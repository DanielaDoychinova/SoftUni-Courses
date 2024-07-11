const BASE_URL = 'http://localhost:3000';

const TEST_URL = {
    TEST_HOME_URL: BASE_URL + '/',
    TEST_LOGIN_URL: BASE_URL + '/login',
    TEST_REGISER_URL: BASE_URL + '/register',
    TEST_CATALOG_URL: BASE_URL + '/catalog',
    TEST_CREATE_URL: BASE_URL + '/create',
    TEST_DETAILS_URL: BASE_URL + '/details',
    TEST_PROFILE_URL: BASE_URL + '/profile'
}

const TEST_USER = {
    EMAIL: 'peter@abv.bg',
    PASSWORD: '123456'
}

const ALERT = {
    ALERT_EMPTY_FIELD: 'All fields are requred!',
    ALERT_CONF_PASS: 'Passwords don\'t match!'
}

const TEST_BOOK = {
    TITLE: 'Test Books Title',
    DESCRIPTION: 'Test Book Description',
    IMAGE: 'https://example.com/book-image.jpg',
    TEST_BOOK_OPTIONS: {
        FICTION: 'Fiction',
        ROMANCE: 'Romance',
        MISTERY: 'Mistery',
        CLASSIC: 'Clasic',
        OTHER: 'Other'
    }
}

const TEST_USER_REG = {
    EMAIL: 'test@test.test',
    PASSWORD: '123456'

}

const TEST_USERR = {
    EMAIL: 'john@abv.bg',
    PASSWORD: '123456'
}

export { 
    BASE_URL,
    TEST_URL,
    TEST_USER,
    ALERT,
    TEST_BOOK,
    TEST_USER_REG, 
    TEST_USERR
}