const {test, describe, beforeEach, afterEach, beforeAll, afterAll, expect} = require('@playwright/test');
const {chromium} = require('playwright');

const host = 'http://localhost:3000';

let browser;
let context;
let page;

let user = {
    email: "",
    password: "123456",
    confirmPass: "123456",
};

let game = {
    title: 'Random title',
    category: 'Random category',
    maxLevel: '77',
    imageUrl: './images/ZombieLang.png',
    summary: 'Random summary'
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

    describe('Authentication', () => {
        test("register makes correct API call", async () => {
            await page.goto(host);
            await page.click('text=Register');

            await page.waitForSelector('form');

            let random = Math.floor(Math.random() * 1000);
            user.email = `abv_${random}@abv.bg`;
            
            
            await page.locator("#email").fill(user.email);
            await page.locator("#register-password").fill(user.password);
            await page.locator("#confirm-password").fill(user.confirmPass);
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes('/users/register') && response.status() === 200),
                page.click('[type="submit"]')
            ]);
            
            await expect(response.ok()).toBeTruthy();
            let userData = await response.json();

            expect(userData.email).toBe(user.email);
            expect(userData.password).toEqual(user.password);
        });

        test('register does not work with empty fields', async () => {
            await page.goto(host);

            await page.click('text=Register');
            await page.click('[type="submit"]');
            expect(page.url()).toBe(host + '/register');
        });

        test('register does not work with invalid email', async () => {
            await page.goto(host);
            await page.click('text=Register');
            await page.waitForSelector('form');

            await page.locator("#email").fill('invalidEmail');
            await page.locator("#register-password").fill(user.password);
            await page.locator("#confirm-password").fill(user.confirmPass);
            await page.click('[type="submit"]');

            //await page.on("dialog", dialog => dialog.accept());

            expect(page.url()).toBe(host + '/register');
        });

        test('register does not work with one empty field', async () => {
            await page.goto(host);
            await page.click('text=Register');
            await page.waitForSelector('form');

            await page.locator("#register-password").fill(user.password);
            await page.locator("#confirm-password").fill(user.confirmPass);
            await page.click('[type="submit"]');

            //await page.on("dialog", dialog => dialog.accept());

            expect(page.url()).toBe(host + '/register');
        });

        test('register does not work with not matching passwords', async () => {
            await page.goto(host);
            await page.click('text=Register');
            await page.waitForSelector('form');

            await page.locator("#email").fill(user.email);
            await page.locator("#register-password").fill(user.password);
            await page.locator("#confirm-password").fill('123457');
            await page.click('[type="submit"]');

            //await page.on("dialog", dialog => dialog.accept());

            expect(page.url()).toBe(host + '/register');
        });

        test('login with valid credentials', async () => {
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');

            await page.locator('#email').fill(user.email);
            await page.locator('#login-password').fill(user.password); 
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes('/users/login') && response.status() === 200),
                page.click('[type="submit"]')
            ]);

            let userData = await response.json();

            expect(response.ok()).toBeTruthy();
            expect(userData.email).toBe(user.email);
            expect(userData.password).toBe(user.password);
        });

        test('login does not work with empty fields', async () => {
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');

            await page.click('[type="submit"]');
            
            expect(page.url()).toBe(host + '/login');
        });

        test("login does not work with incorrect email", async () => {
            await page.goto(host);
            await page.click('text=Login');

            await page.locator("#email").fill('unregistered@abv.bg');
            await page.locator("#login-password").fill(user.password);
            await page.click('[type="submit"]');

            expect(page.url()).toBe(host + '/login');
        });

        test("login does not work without an email", async () => {
            await page.goto(host);
            await page.click('text=Login');

            await page.locator("#login-password").fill(user.password);
            await page.click('[type="submit"]');

            expect(page.url()).toBe(host + '/login');
        });

        test("login does not work without a password", async () => {
            await page.goto(host);
            await page.click('text=Login');

            await page.locator('#email').fill(user.email);
            await page.click('[type="submit"]');

            expect(page.url()).toBe(host + '/login');
        });

        test('logout makes correct API call', async () => {
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#login-password').fill(user.password);
            await page.click('[type="submit"]');

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/users/logout") && response.status() === 204),
                page.locator('nav >> text=Logout').click()
            ]);

            expect(response.ok).toBeTruthy();
            await page.waitForSelector('nav >> text=Login');

            expect(page.url()).toBe(host + '/');

        });   
    });

    describe('navigation bar', () => {
        test('logged user should see correct nav buttons', async () => {
            await page.goto(host);

            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#login-password').fill(user.password); 
            await page.click('[type="submit"]');

            await expect(page.locator('nav >> text=All games')).toBeVisible();
            await expect(page.locator('nav >> text=Create Game')).toBeVisible();
            await expect(page.locator('nav >> text=Logout')).toBeVisible();
            await expect(page.locator('nav >> text=Login')).toBeHidden();
            await expect(page.locator('nav >> text=Register')).toBeHidden();   
        });

        test('guest user should see correct nav buttons', async () => {
            await page.goto(host);

            await expect(page.locator('nav >> text=All games')).toBeVisible();
            await expect(page.locator('nav >> text=Create Game')).toBeHidden();
            await expect(page.locator('nav >> text=Logout')).toBeHidden();
            await expect(page.locator('nav >> text=Login')).toBeVisible();
            await expect(page.locator('nav >> text=Register')).toBeVisible();   
        }); 
    });

    describe('games functionalities', () => {
        beforeEach(async () => {
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#login-password').fill(user.password);
            await page.click('[type="submit"]');
        });

        test('create does not work with empty fields', async () => {
            await page.click('text=Create Game');
            await page.waitForSelector('form');

            await page.click('[type="submit"]');

            await expect(page.url()).toBe(host + '/create');
        });

        test('create does not work with one empty field', async () => {

            await page.click('text=Create Game');
            await page.waitForSelector('form');

            await page.fill('[name="category"]', "Random category");
            await page.fill('[name="maxLevel"]', "12");
            await page.fill('[name="imageUrl"]', "https://jpeg.org/images/jpeg-home.jpg");
            await page.fill('[name="summary"]', "Random summary...");
            await page.click('[type="submit"]');

            //await page.on("dialog", dialog => dialog.accept());

            expect(page.url()).toBe(host + '/create');
        });

        test('create makes correct API call for logged user', async () => {
            await page.click('text=Create Game');
            await page.waitForSelector('form');

            await page.locator('#title').fill(game.title);
            await page.locator('#category').fill(game.category);
            await page.locator('#maxLevel').fill(game.maxLevel);
            await page.locator('#imageUrl').fill(game.imageUrl);
            await page.locator('#summary').fill(game.summary);

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes('/data/games') && response.status() === 200),
                await page.click('[type="submit"]')
            ]);

            let gameData = await response.json();

            await expect(response.ok).toBeTruthy();
            expect(gameData.title).toEqual(game.title);
            expect(gameData.category).toEqual(game.category);
            expect(gameData.maxLevel).toEqual(game.maxLevel);
            expect(gameData.summary).toEqual(game.summary);
        });

        test('edit/delete buttons are visible for owner', async () => {
            await page.goto(host + '/catalog');

            await page.click(`.allGames .allGames-info:has-text("Random title") .details-button`);
            
            await expect(page.locator('text=Delete')).toBeVisible();
            await expect(page.locator('text=Edit')).toBeVisible();
        });

        test('edit/delete buttons are not visible for non-owner', async () => {
            await page.goto(host + '/catalog');

            await page.click(`.allGames .allGames-info:has-text("MineCraft") .details-button`);
            
            await expect(page.locator('text=Delete')).toBeHidden();
            await expect(page.locator('text=Edit')).toBeHidden();
        });

        test('edit makes correct API call', async () => {
            await page.goto(host + '/catalog');
            await page.click(`.allGames .allGames-info:has-text("Random title") .details-button`);
            await page.click('text=Edit');

            await page.locator('[name="title"]').fill('Edited title');
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes('/data/games') && response.status() === 200),
                await page.click('[type="submit"]')
            ]);

            let gameData = await response.json();

            await expect(response.ok).toBeTruthy();
            expect(gameData.title).toEqual('Edited title');
            expect(gameData.category).toEqual(game.category);
            expect(gameData.maxLevel).toEqual(game.maxLevel);
            expect(gameData.summary).toEqual(game.summary);
        });

        test('delete makes correct API call', async () => {
            await page.goto(host + '/catalog');
            await page.click(`.allGames .allGames-info:has-text("Edited title") .details-button`);
            
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes('/data/games') && response.status() === 200),
                await page.click('text=Delete')
            ]);

            await expect(response.ok).toBeTruthy();
        });
    });

    describe('home page', () => {
        test('home page has correct data', async () => {
            await page.goto(host);

            await expect(page.locator('.welcome-message h2')).toHaveText('ALL new games are');
            await expect(page.locator('.welcome-message h3')).toHaveText('Only in GamesPlay');
            await expect(page.locator('#home-page h1')).toHaveText('Latest Games');

            const games = await page.locator('#home-page .game').all()
            expect(games.length).toBeGreaterThanOrEqual(3);
        });
    });
});

