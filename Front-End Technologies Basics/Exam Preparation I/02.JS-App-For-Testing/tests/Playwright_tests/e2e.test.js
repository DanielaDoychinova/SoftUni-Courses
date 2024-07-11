const { test, describe, beforeEach, afterEach, beforeAll, afterAll, expect } = require('@playwright/test');
const { chromium } = require('playwright');

const host = 'http://localhost:3000'; // Application host (NOT service host - that can be anything)

let browser;
let context;
let page;

let user = {
    username : "",
    email : "",
    password : "123456",
    confirmPass : "123456",
};

let meme =  {
    title: '',
    description: '',
    imageUrl: 'https://jpeg.ogr/images/jpeg-home.jpg'
}


describe("e2e tests", () => {
    beforeAll(async () => {
        browser = await chromium.launch();
    });
    
    afterAll(async () => {
        await browser.close();
    });
    
    beforeEach(async () => {
        context = await browser.newContext();
        page = await context.newPage();
    });
    
    afterEach(async () => {
        await page.close();
        await context.close();
    });
    
    
    describe("authentication", () => {
        test('registration', async () => {
            await page.goto(host);
            
            let random = Math.floor(Math.random() * 10000);
            user.username = `randomuser_${random}`
            user.email = `abv${random}@abv.bg`
            
            await page.locator('a[href="/register"]').first().click();
            await page.waitForSelector('form');
            
            await page.locator('#username').fill(user.username);
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.locator('#repeatPass').fill(user.confirmPass);
            
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes('/users/register') && response.status() === 200),
                page.click('[type="submit"]')
            ]);
            
            expect(response.ok).toBeTruthy();
            
            let userData = await response.json();
            
            expect(userData.email).toBe(user.email);
            expect(userData.password).toBe(user.password);
        });
        
        test('login', async () => {
            await page.goto(host);
            await page.locator('a[href="/login"]').first().click();
            await page.waitForSelector('form');
            
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes('/users/login') && response.status() === 200),
                page.click('[type="submit"]')
            ]);
            
            expect(response.ok).toBeTruthy();
            
            let userData = await response.json();
            
            expect(userData.email).toBe(user.email);
            expect(userData.password).toBe(user.password);
            
        });
        
        test('logout', async () => {
            await page.goto(host);
            await page.locator('a[href="/login"]').first().click();
            await page.waitForSelector('form');
            
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]')
            
            
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes('/users/logout') && response.status() === 204),
                await page.click('a[href="/logout"]')
            ]);
            
            await page.locator('a[href="/login"]');
            
            expect(page.url()).toBe(host + '/');
        });
    });
    
    describe("navbar", () => {
        test('guest user navbar', async () => {
            await page.goto(host);
            
            await expect(page.locator('a[href="/login"]').first()).toBeVisible();
            await expect(page.locator('a[href="/register"]').first()).toBeVisible();
            await expect(page.locator('a[href="/"]')).toBeVisible();
            await expect(page.locator('a[href="/catalog"]')).toBeVisible();
            await expect(page.locator('a[href="/logout"]')).toBeHidden();
            await expect(page.locator('a[href="/myprofile"]')).toBeHidden();
            await expect(page.locator('a[href="/create"]')).toBeHidden();
            
        });
        
        test('logged user navbar', async () => {
            await page.goto(host);
            await page.locator('a[href="/login"]').first().click();
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
            
            await expect(page.locator('a[href="/login"]').first()).toBeHidden();
            await expect(page.locator('a[href="/register"]').first()).toBeHidden();
            await expect(page.locator('a[href="/"]')).toBeHidden();
            await expect(page.locator('a[href="/catalog"]')).toBeVisible();
            await expect(page.locator('a[href="/logout"]')).toBeVisible();
            await expect(page.locator('a[href="/myprofile"]')).toBeVisible();
            await expect(page.locator('a[href="/create"]')).toBeVisible();
        });
        
        describe("CRUD", () => {
            beforeEach( async () => {
                await page.goto(host);
                await page.locator('a[href="/login"]').first().click();
                await page.waitForSelector('form');
                await page.locator('#email').fill(user.email);
                await page.locator('#password').fill(user.password);
                await page.click('[type="submit"]');
                
            });
            
            test('create meme', async () => {
                await page.click('a[href="/create"]');
                await page.waitForSelector('form');
                
                let random = Math.floor(Math.random() * 10000);
                meme.title = `Random title ${random}`;
                meme.description = `Random description ${random}`,
                
                await page.locator('#title').fill(meme.title);
                await page.locator('#description').fill(meme.description);
                await page.locator('#imageUrl').fill(meme.imageUrl);
                
                let [response] = await Promise.all([
                    page.waitForResponse(response => response.url().includes("/data/memes") && response.status() === 200),
                    page.click('[type="submit"]')
                ]);
                
                let memeData = await response.json();
                console.log(memeData);
                
                expect(response.ok).toBeTruthy();
                
                expect(memeData.title).toBe(meme.title);
                expect(memeData.description).toBe(meme.description);
                expect(memeData.imageUrl).toBe(meme.imageUrl);
            });
            
            test('edit meme', async () => {
                await page.click('a[href="/myprofile"]');
                
                await page.locator('//a[text()="Details"]').first().click();
                await page.locator('text=Edit').click();
                await page.waitForSelector('form');
                await page.locator('#description').fill('edited description');
                
                let [response] = await Promise.all([
                    page.waitForResponse(response => response.url().includes("/data/memes") && response.status() === 200),
                    page.click('[type="submit"]')
                ]);
                
                let memeData = await response.json();
                console.log(memeData);
                
                expect(response.ok).toBeTruthy();
                
                expect(memeData.title).toBe(meme.title);
                expect(memeData.description).toBe('edited description');
                expect(memeData.imageUrl).toBe(meme.imageUrl);
            });
            
            test('delete meme', async () => {
                await page.click('a[href="/myprofile"]');
                
                await page.locator('//a[text()="Details"]').first().click();
                
                
                let [response] = await Promise.all([
                    page.waitForResponse(response => response.url().includes("/data/memes") && response.status() === 200),
                    page.locator('text=Delete').click()
                ]);
                
                expect(response.ok()).toBeTruthy();
            });
        });
    });
});