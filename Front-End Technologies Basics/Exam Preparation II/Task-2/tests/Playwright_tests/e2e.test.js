const { test, describe, beforeEach, afterEach, beforeAll, afterAll, expect } = require('@playwright/test');
const { chromium } = require('playwright');

const host = 'http://localhost:3000'; // Application host (NOT service host - that can be anything)

let browser;
let context;
let page;

let user = {
    email : "",
    password : "123456",
    confirmPass : "123456",
};

let event = {
    author: '',
    date: '24.06.2024',
    title: '',
    description: '',
    imageUrl: '/images/2.png'
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
            
            await page.locator('a[href="/register"]').click();
            await page.waitForSelector('form');
            
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.locator('#repeatPassword').fill(user.confirmPass);
            
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
            await page.locator('a[href="/login"]').click();
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
            await page.locator('a[href="/login"]').click();
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
        test('logged user navbar', async () => {
            await page.goto(host);
            await page.locator('a[href="/login"]').click();
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
            
            await expect(page.locator('a[href="/login"]').first()).toBeHidden();
            await expect(page.locator('a[href="/register"]').first()).toBeHidden();
            await expect(page.locator('a[href="/"]')).toBeVisible();
            await expect(page.locator('a[href="/logout"]')).toBeVisible();
            await expect(page.locator('a[href="/profile"]')).toBeVisible();
            await expect(page.locator('a[href="/create"]')).toBeVisible();
        });
        
        test('guest user navbar', async () => {
            await page.goto(host);
            
            await expect(page.locator('a[href="/login"]').first()).toBeVisible();
            await expect(page.locator('a[href="/register"]').first()).toBeVisible();
            await expect(page.locator('a[href="/"]')).toBeVisible();
            await expect(page.locator('a[href="/logout"]')).toBeHidden();
            await expect(page.locator('a[href="/profile"]')).toBeHidden();
            await expect(page.locator('a[href="/create"]')).toBeHidden();
        });
    });
    
    describe("CRUD", () => {
        beforeEach( async () => {
            await page.goto(host);
            await page.locator('a[href="/login"]').click();
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
        });
        
        test('create an event', async () => {
            await page.click('a[href="/create"]');
            await page.waitForSelector('form');
            
            let random = Math.floor(Math.random() * 10000);
            event.title = `Title ${random}`;
            event.description = `Description ${random}`,
            event.author = `Author ${random}`
            
            await page.locator('#title').fill(event.title);
            await page.locator('#date').fill(event.date);
            await page.locator('#description').fill(event.description);
            await page.locator('#author').fill(event.author);
            await page.locator('#imageUrl').fill(event.imageUrl);
            
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/theaters") && response.status() === 200),
                page.click('[type="submit"]')
            ]);
            
            let eventData = await response.json();
            console.log(eventData);
            
            expect(response.ok).toBeTruthy();
            
            expect(eventData.author).toBe(event.author);
            expect(eventData.date).toBe(event.date);
            expect(eventData.title).toBe(event.title);
            expect(eventData.description).toBe(event.description);
            expect(eventData.imageUrl).toBe(event.imageUrl);
        });
        
        test('edit the event', async () => {
            await page.click('a[href="/profile"]');
            
            await page.locator('//a[text()="Details"]').first().click();
            await page.locator('text=Edit').click();
            await page.waitForSelector('form');
            await page.locator('#description').fill('edited description');
            
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/theaters") && response.status() === 200),
                page.click('[type="submit"]')
            ]);
            
            let eventData = await response.json();
            console.log(eventData);
            
            expect(response.ok).toBeTruthy();
            
            expect(eventData.author).toBe(event.author);
            expect(eventData.date).toBe(event.date);
            expect(eventData.title).toBe(event.title);
            expect(eventData.description).toBe('edited description');
            expect(eventData.imageUrl).toBe(event.imageUrl);
        });
        
        
        
        test('delete the event', async () => {
            // Click on the 'Profile' button
            await page.locator('text=Profile').click();
            await page.locator("text=Details").first().click();
            
            // Wait for the response and click on the 'Delete' button
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/theaters") && response.status() === 200),
                page.on('dialog', dialog => dialog.accept()),
                page.click('text=delete')
            ]);
            
            // Assert that the response is okay
            expect(response.ok()).toBeTruthy();
            
        });

        test("delete makes correct API call", async()=>{
            //arrange
            await page.click("nav >> text=Profile");
            await page.locator("text=Details").first().click();

            //act
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/theaters") && response.status() == 200 ),
                page.on('dialog', dialog => dialog.accept()),
                page.click('text=delete')
            ]);

            //assert
            expect(response.ok()).toBeTruthy();
        })
    })
})