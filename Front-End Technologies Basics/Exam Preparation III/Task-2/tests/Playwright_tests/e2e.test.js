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

let book = {
    title: '',
    description: '',
    imageUrl: '/images/book.png',
    type: {
        fiction: 'Fiction',
        romance: 'Romance',
        mistery: 'Mistery',
        classic: 'Classic',
        other: 'Other'
    }
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
        test("Registration with valid data", async () => {
            await page.goto(host);
            await page.click("text=Register");
            await page.waitForSelector('form');
            let random = Math.floor(Math.random()*10000);
            user.email = `abv${random}@abv.bg`;

            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.locator('#repeat-pass').fill(user.confirmPass);
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/users/register") && response.status() == 200),
                page.click('[type="submit"]')
            ])
            let userData = await response.json();
            console.log(userData);

            //assert
            expect(response.ok()).toBeTruthy();
            expect(userData.email).toBe(user.email);
            expect(userData.password).toBe(user.password);
        });

        test("Login with valid credentials", async()=>{
            //arrange
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');

            //act
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/users/login") && response.status() == 200),
                page.click('[type="submit"]')
            ])
            let userData = await response.json();
            console.log(userData);

            expect(response.ok()).toBeTruthy();
            expect(userData.email).toBe(user.email);
            expect(userData.password).toBe(user.password);
        });

        test('Logout from application', async () => {
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/users/logout") && response.status() == 204 ),
                page.click('nav >> text=Logout')
            ])

            expect(response.ok()).toBeTruthy();
            await page.waitForSelector('nav >> text=Login')
            expect(page.url()).toBe(host + '/');
        });

    })

    describe("navbar", () => {
        test("Navigation for logged-in user", async()=>{
            await page.goto(host);

            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');

            await expect(page.locator('nav >> text=Dashboard')).toBeVisible();
            await expect(page.locator('nav >> text=My Books')).toBeVisible();
            await expect(page.locator('nav >> text=Add Book')).toBeVisible();
            await expect(page.locator('nav >> text=Logout')).toBeVisible();
            await expect(page.locator('nav >> text=Login')).toBeHidden();
            await expect(page.locator('nav >> text=Register')).toBeHidden();
        });

        test("Navigation for guest user", async()=>{
            await page.goto(host);

            await expect(page.locator('nav >> text=Dashboard')).toBeVisible();
            await expect(page.locator('nav >> text=My Books')).toBeHidden();
            await expect(page.locator('nav >> text=Add Book')).toBeHidden();
            await expect(page.locator('nav >> text=Logout')).toBeHidden();
            await expect(page.locator('nav >> text=Login')).toBeVisible();
            await expect(page.locator('nav >> text=Register')).toBeVisible();
        });
    });

    describe("CRUD", () => {
        beforeEach(async () =>{
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
        });

        test('Create a book', async()=>{
            await page.click('nav >> text=Add Book');
            await page.waitForSelector("form");

            let random = Math.floor(Math.random() * 10000);
            book.title = `Title${random}`;
            book.description = `Description${random}`;

            await page.locator('#title').fill(book.title);
            await page.locator('#description').fill(book.description);
            await page.locator('#image').fill(book.imageUrl);
            await page.locator('#type').selectOption(book.type.classic);

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/books") && response.status() == 200 ),
                page.click('[type="submit"]')
            ]);
            let bookData = await response.json();
            console.log(bookData);

            expect(response.ok()).toBeTruthy();
            expect(bookData.title).toBe(book.title);
            expect(bookData.description).toBe(book.description);
            expect(bookData.type).toBe(book.type.classic);
            expect(book.imageUrl).toBe(book.imageUrl);
        });

        test('Edit the book', async () => {
            await page.click('nav >> text=My Books');
            await page.locator('text=Details').first().click();

            await page.locator('text=Edit').click();
            await page.waitForSelector('form');
            await page.locator('#description').fill('edited description');
            await page.locator('#type').selectOption(book.type.classic);

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/books") && response.status() === 200),
                page.click('[type="submit"]')
            ]);
    
            let bookData = await response.json();
            console.log(bookData);

            expect(response.ok()).toBeTruthy();
            expect(bookData.title).toBe(book.title);
            expect(bookData.description).toBe('edited description');
            expect(bookData.type).toBe(book.type.classic);
            expect(bookData.imageUrl).toBe(book.imageUrl);
        });
    
        test("Delete a book", async()=>{
            await page.click("nav >> text=My Books");
            await page.locator("text=Details").first().click();

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/books") && response.status() == 200 ),
                page.on('dialog', dialog => dialog.accept()),
                page.click('text=delete')
            ]);

            expect(response.ok()).toBeTruthy();
        })
    });
    });
