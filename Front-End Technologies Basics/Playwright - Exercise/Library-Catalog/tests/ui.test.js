const { test, expect } = require('@playwright/test');
import { ALL_BOOKS_LIST, CREATE_FORM, DELETE_BUTTON, DETAILS_BUTTONS, DETAILS_DESCRIPTION, DETAILS_TITLE, DETAILS_TYPE, EDIT_BUTTON, LIKE_BUTTON, LOGGED_NAVBAR, LOGIN_FORM, NAVBAR, REGISTER_FORM } from '../utils/locators';
import { ALERT, BASE_URL, TEST_BOOK, TEST_URL, TEST_USER, TEST_USERR, TEST_USER_REG } from '../utils/constants';


//Navbar

test('Verify "All Books" link is visible ex2', async ({page}) => {
    await page.goto(BASE_URL);

    await expect(page.locator(NAVBAR.NAV_NAVBAR)).toBeVisible();

    await expect(page.locator(NAVBAR.ALL_BOOKS_LINK)).toBeVisible();

});

test('Verify "LOGIN" link is visible ex2', async ({page}) => {
    await page.goto(BASE_URL);

    await expect(page.locator(NAVBAR.NAV_NAVBAR)).toBeVisible();

    await expect(page.locator(NAVBAR.LOGIN_BUTTON)).toBeVisible();

});

test('Verify "REGISTER" link is visible ex2', async ({page}) => {
    await page.goto(BASE_URL);

    await expect(page.locator(NAVBAR.NAV_NAVBAR)).toBeVisible();

    await expect(page.locator(NAVBAR.REGISTER_BUTTON)).toBeVisible();

});

// Navbar for logged

test('All Books Link visible after login', async ({page}) => {
    await page.goto(BASE_URL);

    await expect(page.locator(NAVBAR.LOGIN_BUTTON)).toBeVisible();

    await page.locator(NAVBAR.LOGIN_BUTTON).click();

    await expect(page.locator(LOGIN_FORM.LOGIN_FORM)).toBeVisible();

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.SUBMIT).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);

    await expect(page.locator(NAVBAR.ALL_BOOKS_LINK)).toBeVisible();
});

test('Verify "My Books" link is visible', async ({page}) => {
    await page.goto(BASE_URL);
    
    await expect(page.locator(NAVBAR.LOGIN_BUTTON)).toBeVisible();
    await page.locator(NAVBAR.LOGIN_BUTTON).click();
    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.SUBMIT).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);

    await expect(page.locator(LOGGED_NAVBAR.MY_BOOKS_LINK)).toBeVisible();
});

test('Verify "Add Book" link is visible', async ({page}) => {
    await page.goto(BASE_URL);
    
    await expect(page.locator(NAVBAR.LOGIN_BUTTON)).toBeVisible();
    await page.locator(NAVBAR.LOGIN_BUTTON).click();
    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.SUBMIT).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);

    await expect(page.locator(LOGGED_NAVBAR.ADD_BOOK_LINK)).toBeVisible();
});

test('User email is visible', async({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await expect(page.locator(LOGIN_FORM.LOGIN_FORM)).toBeVisible();

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.SUBMIT).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);

    await expect(page.locator(LOGGED_NAVBAR.USER_EMAIL)).toBeVisible();
    await expect(page.locator(LOGGED_NAVBAR.MY_BOOKS_LINK)).toBeVisible();
    await expect(page.locator(LOGGED_NAVBAR.ADD_BOOK_LINK)).toBeVisible();


});




//Login Page

test('Valid login', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.SUBMIT).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);

});

test('empty input fields', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.SUBMIT).click();
    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    })

    await page.waitForURL(TEST_URL.TEST_LOGIN_URL);
    expect(page.url()).toBe(TEST_URL.TEST_LOGIN_URL);
}); 

test('empty email field', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.SUBMIT).click();
    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    })

    await page.waitForURL(TEST_URL.TEST_LOGIN_URL);
    expect(page.url()).toBe(TEST_URL.TEST_LOGIN_URL);
}); 

test('Submit the form with empty password field', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    });

    await page.waitForURL(TEST_URL.TEST_LOGIN_URL);
    expect(page.url()).toBe(TEST_URL.TEST_LOGIN_URL);
});



//Register Page

test('Submit the form with valid values', async ({page}) => {
    await page.goto(TEST_URL.TEST_REGISER_URL);

    await page.locator(REGISTER_FORM.EMAIL).fill(TEST_USER_REG.EMAIL);
    await page.locator(REGISTER_FORM.PASSWORD).fill(TEST_USER_REG.PASSWORD);
    await page.locator(REGISTER_FORM.REPEAT_PASS).fill(TEST_USER_REG.PASSWORD);
    await page.locator(REGISTER_FORM.SUBMIT).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);
})

test('Submit the form with empty values', async ({page}) => {
    await page.goto(TEST_URL.TEST_REGISER_URL);

    await page.locator(REGISTER_FORM.SUBMIT).click();

    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    });

    await page.waitForURL(TEST_URL.TEST_REGISER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISER_URL);
}); 

test('Submit the form with empty email', async ({page}) => {
    await page.goto(TEST_URL.TEST_REGISER_URL);

    await page.locator(REGISTER_FORM.PASSWORD).fill(TEST_USER_REG.PASSWORD);
    await page.locator(REGISTER_FORM.REPEAT_PASS).fill(TEST_USER_REG.PASSWORD);
    await page.locator(REGISTER_FORM.SUBMIT).click();

    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    });

    await page.waitForURL(TEST_URL.TEST_REGISER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISER_URL);
}); 

test('Submit the form with empty password', async ({page}) => {
    await page.goto(TEST_URL.TEST_REGISER_URL);

    await page.locator(REGISTER_FORM.EMAIL).fill(TEST_USER_REG.EMAIL);
    await page.locator(REGISTER_FORM.REPEAT_PASS).fill(TEST_USER_REG.PASSWORD);
    await page.locator(REGISTER_FORM.SUBMIT).click();

    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    });

    await page.waitForURL(TEST_URL.TEST_REGISER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISER_URL);
});

test('Submit the form with empty repeat password', async ({page}) => {
    await page.goto(TEST_URL.TEST_REGISER_URL);

    await page.locator(REGISTER_FORM.EMAIL).fill(TEST_USER_REG.EMAIL);
    await page.locator(REGISTER_FORM.PASSWORD).fill(TEST_USER_REG.PASSWORD);
    await page.locator(REGISTER_FORM.SUBMIT).click();

    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    });

    await page.waitForURL(TEST_URL.TEST_REGISER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISER_URL);
});

test('Submit the form with different passwords', async ({page}) => {
    await page.goto(TEST_URL.TEST_REGISER_URL);

    await page.locator(REGISTER_FORM.EMAIL).fill(TEST_USER_REG.EMAIL);
    await page.locator(REGISTER_FORM.PASSWORD).fill(TEST_USER_REG.PASSWORD);
    await page.locator(REGISTER_FORM.REPEAT_PASS).fill('654321')
    await page.locator(REGISTER_FORM.SUBMIT).click();

    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_CONF_PASS);
        await dialog.accept();
    });

    await page.waitForURL(TEST_URL.TEST_REGISER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISER_URL);
});



//Add Book Page

test('Submit the form with correct data', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
        page.locator(LOGIN_FORM.SUBMIT).click(),
        page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    await page.locator(LOGGED_NAVBAR.ADD_BOOK_LINK).click();

    await page.locator(CREATE_FORM.TITLE).fill(TEST_BOOK.TITLE);
    await page.locator(CREATE_FORM.DESCRIPTION).fill(TEST_BOOK.DESCRIPTION);
    await page.locator(CREATE_FORM.IMAGE).fill(TEST_BOOK.IMAGE);
    await page.locator(CREATE_FORM.TYPE_OPTION).selectOption(TEST_BOOK.TEST_BOOK_OPTIONS.FICTION);
    await page.locator(CREATE_FORM.ADD_BOOK_BUTTON).click();
    
    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);
});

test('Submit the form with empty title', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
        page.locator(LOGIN_FORM.SUBMIT).click(),
        page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    await page.locator(LOGGED_NAVBAR.ADD_BOOK_LINK).click();

    await page.locator(CREATE_FORM.DESCRIPTION).fill(TEST_BOOK.DESCRIPTION);
    await page.locator(CREATE_FORM.IMAGE).fill(TEST_BOOK.IMAGE);
    await page.locator(CREATE_FORM.TYPE_OPTION).selectOption(TEST_BOOK.TEST_BOOK_OPTIONS.FICTION);
    await page.locator(CREATE_FORM.ADD_BOOK_BUTTON).click();

    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    });
    
    await page.waitForURL(TEST_URL.TEST_CREATE_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CREATE_URL);
});

test('Submit the form with empty description', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
        page.locator(LOGIN_FORM.SUBMIT).click(),
        page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    await page.locator(LOGGED_NAVBAR.ADD_BOOK_LINK).click();

    await page.locator(CREATE_FORM.TITLE).fill(TEST_BOOK.TITLE);
    await page.locator(CREATE_FORM.IMAGE).fill(TEST_BOOK.IMAGE);
    await page.locator(CREATE_FORM.TYPE_OPTION).selectOption(TEST_BOOK.TEST_BOOK_OPTIONS.FICTION);
    await page.locator(CREATE_FORM.ADD_BOOK_BUTTON).click();

    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    });
    
    await page.waitForURL(TEST_URL.TEST_CREATE_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CREATE_URL);
});

test('Submit the form with empty image', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
        page.locator(LOGIN_FORM.SUBMIT).click(),
        page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    await page.locator(LOGGED_NAVBAR.ADD_BOOK_LINK).click();

    await page.locator(CREATE_FORM.TITLE).fill(TEST_BOOK.TITLE);
    await page.locator(CREATE_FORM.DESCRIPTION).fill(TEST_BOOK.DESCRIPTION);
    await page.locator(CREATE_FORM.TYPE_OPTION).selectOption(TEST_BOOK.TEST_BOOK_OPTIONS.FICTION);
    await page.locator(CREATE_FORM.ADD_BOOK_BUTTON).click();

    page.on('dialog', async dialog => {
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_EMPTY_FIELD);
        await dialog.accept();
    });
    
    await page.waitForURL(TEST_URL.TEST_CREATE_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CREATE_URL);
});



//All Books Page

test('Login and verify all books are displayed', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
        page.waitForURL(TEST_URL.TEST_CATALOG_URL),
        page.locator(LOGIN_FORM.SUBMIT).click()
    ]);

    const booksCount = await page.locator('//li[@class="otherBooks"]').count();
    expect(booksCount).toBeGreaterThan(0);
});



//Details Page

test('Verify that logged-in user sees details button and button works correctly', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USERR.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USERR.PASSWORD);

    await Promise.all([
        page.locator(LOGIN_FORM.SUBMIT).click(),
        page.waitForURL(TEST_URL.TEST_CATALOG_URL)

    ]);

    await page.locator(DETAILS_BUTTONS).first().click();
    await expect(page.locator(DETAILS_DESCRIPTION)).toBeVisible();
      
});

test('Verify guest user sees details button and button works correctly', async ({page}) => {
    await page.goto(TEST_URL.TEST_CATALOG_URL);

    await page.locator(DETAILS_BUTTONS).first().click();
    await expect(page.locator(DETAILS_DESCRIPTION)).toBeVisible();
});

test('Verify Edit and Delete buttons are visible for creator', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USERR.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USERR.PASSWORD);

    await Promise.all([
        page.locator(LOGIN_FORM.SUBMIT).click(),
        page.waitForURL(TEST_URL.TEST_CATALOG_URL)

    ]);

    await page.locator(LOGGED_NAVBAR.MY_BOOKS_LINK).click();
    await expect(page.url()).toBe(TEST_URL.TEST_PROFILE_URL);

    await page.locator(DETAILS_BUTTONS).first().click();

    await expect(page.locator(DELETE_BUTTON)).toBeVisible();
    await expect(page.locator(EDIT_BUTTON)).toBeVisible();
      
});

test('Verify Edit and Delete buttons are not visible for non-creator', async ({page}) => {
    await page.goto(TEST_URL.TEST_CATALOG_URL);

    await page.locator(DETAILS_BUTTONS).first().click();

    await expect(page.locator(DELETE_BUTTON)).not.toBeVisible();
    await expect(page.locator(EDIT_BUTTON)).not.toBeVisible();
      
});

test('Verify Like button is not visible for creator', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USERR.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USERR.PASSWORD);

    await Promise.all([
        page.locator(LOGIN_FORM.SUBMIT).click(),
        page.waitForURL(TEST_URL.TEST_CATALOG_URL)

    ]);

    await page.locator(LOGGED_NAVBAR.MY_BOOKS_LINK).click();
    await expect(page.url()).toBe(TEST_URL.TEST_PROFILE_URL);

    await page.locator(DETAILS_BUTTONS).first().click();

    await expect(page.locator(LIKE_BUTTON)).not.toBeVisible();
      
});

test('Verify Like button is visible for non-creator', async ({page}) => {
    await page.goto(TEST_URL.TEST_CATALOG_URL);

    await page.locator(DETAILS_BUTTONS).first().click();

    await expect(page.locator(LIKE_BUTTON)).not.toBeVisible();
      
});
 



//Logout Functionality

test('Verify Logout button is visible', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.SUBMIT).click();

    await expect(page.locator(LOGGED_NAVBAR.LOGOUT_BUTTON)).toBeVisible();
});

test('Verify Logout button redirects correctly', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.SUBMIT).click();

    await page.locator(LOGGED_NAVBAR.LOGOUT_BUTTON).click();
    
    await expect(page.locator(LOGGED_NAVBAR.USER_EMAIL)).not.toBeVisible();
});



