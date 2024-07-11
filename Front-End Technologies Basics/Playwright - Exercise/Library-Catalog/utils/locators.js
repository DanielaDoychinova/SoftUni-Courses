const NAVBAR = {
    NAV_NAVBAR: 'nav.navbar',
    ALL_BOOKS_LINK: 'a[href="/catalog"]',
    LOGIN_BUTTON: 'a[href="/login"]',
    REGISTER_BUTTON: 'a[href="/register"]'
}

const LOGIN_FORM = {
    LOGIN_FORM: '#login-form',
    EMAIL: 'input[id="email"]',
    PASSWORD: 'input[id="password"]',
    SUBMIT: '#login-form input[type="submit"]'

}

const LOGGED_NAVBAR = {
    USER_EMAIL: '//span[text()="Welcome, peter@abv.bg"]',
    MY_BOOKS_LINK: 'a[href="/profile"]',
    ADD_BOOK_LINK: 'a[href="/create"]',
    LOGOUT_BUTTON: 'a[id="logoutBtn"]'
}

const CREATE_FORM = {
    CREATE_FORM: '#create-form',
    TITLE: 'input[id="title"]',
    DESCRIPTION: 'textarea[id="description"]',
    IMAGE: 'input[id="image"]',
    TYPE_OPTION: '#type',
    ADD_BOOK_BUTTON: '#create-form input[type="submit"]'
}

const ALL_BOOKS_LIST = '//li[@class="otherBooks"]';

const DETAILS_BUTTONS = '//a[text()="Details"]';

const DETAILS_DESCRIPTION = '//h3[text()="Description:"]';

const EDIT_BUTTON = '//div[@class="actions"]/a[1]';

const DELETE_BUTTON = '//div[@class="actions"]/a[2]';

const LIKE_BUTTON = '//a[text()="Like"]';



const REGISTER_FORM = {
    EMAIL: 'input[id="email"]',
    PASSWORD: 'input[id="password"]',
    REPEAT_PASS: 'input[id="repeat-pass"]',
    SUBMIT: 'input[type="submit"]'
}



export {
    NAVBAR,
    LOGIN_FORM,
    LOGGED_NAVBAR,
    CREATE_FORM,
    ALL_BOOKS_LIST,
    DETAILS_BUTTONS,
    DETAILS_DESCRIPTION,
    REGISTER_FORM,
    EDIT_BUTTON,
    DELETE_BUTTON,
    LIKE_BUTTON
}