const baseUrl = 'http://localhost:3030';

let user = {
    username: '',
    email: '',
    password: '123456',
    gender: 'male'
    
};

let token = '';
let userId = '';

let meme = {
    title: '',
    description: '',
    imageUrl: '/image/2.png'
}

let lastCreatedMemeId = '';

QUnit.config.reorder = false;

QUnit.module('user functionalities', () => {
    QUnit.test('user registration', async (assert) => {
        let path = '/users/register';
        let random = Math.floor(Math.random() * 10000);
        let randomUsername = `Auto_Test_User_${random}`;
        let randomEmail = `abv${random}@abv.bg`;
        
        user.username = randomUsername;
        user.email = randomEmail;
        
        let response = await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json',
                
            },
            body: JSON.stringify(user)
        });
        
        assert.ok(response.ok, 'successful response');
        
        let jsonResponse = await response.json();
        console.log(jsonResponse);
        
        assert.ok(jsonResponse.hasOwnProperty('email'), 'Email exists');
        assert.equal(jsonResponse['email'], user.email, 'Expected email');
        assert.strictEqual(typeof jsonResponse.email, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('username'), 'Username exists');
        assert.equal(jsonResponse['username'], user.username, 'Expected username');
        assert.strictEqual(typeof jsonResponse.username, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('password'), 'password exists');
        assert.equal(jsonResponse['password'], user.password, 'Expected password');
        assert.strictEqual(typeof jsonResponse.password, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('gender'), 'gender exists');
        assert.equal(jsonResponse['gender'], user.gender, 'Expected gender');
        assert.strictEqual(typeof jsonResponse.gender, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('_id'), '_id exists');
        assert.strictEqual(typeof jsonResponse._id, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('accessToken'), 'accessToken exists');
        assert.strictEqual(typeof jsonResponse.accessToken, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('_createdOn'), '_createdOn exists');
        assert.strictEqual(typeof jsonResponse._createdOn, 'number', 'Expected property type');
        
        token = jsonResponse.accessToken;
        userId = jsonResponse._id;
        sessionStorage.setItem('meme-user', JSON.stringify(user));
    });
    
    QUnit.test('user login', async (assert) => {
        let path = '/users/login';
        
        let response = await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify({
                email: user.email,
                password:user.password
            })
        });
        
        assert.ok(response.ok, 'Successful login');
        
        let jsonResponse = await response.json();
        console.log(jsonResponse);
        
        assert.ok(jsonResponse.hasOwnProperty('email'), 'Email exists');
        assert.equal(jsonResponse['email'], user.email, 'Expected email');
        assert.strictEqual(typeof jsonResponse.email, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('username'), 'Username exists');
        assert.equal(jsonResponse['username'], user.username, 'Expected username');
        assert.strictEqual(typeof jsonResponse.username, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('password'), 'password exists');
        assert.equal(jsonResponse['password'], user.password, 'Expected password');
        assert.strictEqual(typeof jsonResponse.password, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('gender'), 'gender exists');
        assert.equal(jsonResponse['gender'], user.gender, 'Expected gender');
        assert.strictEqual(typeof jsonResponse.gender, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('_id'), '_id exists');
        assert.strictEqual(typeof jsonResponse._id, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('accessToken'), 'accessToken exists');
        assert.strictEqual(typeof jsonResponse.accessToken, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('_createdOn'), '_createdOn exists');
        assert.strictEqual(typeof jsonResponse._createdOn, 'number', 'Expected property type');
        
        token = jsonResponse.accessToken;
        userId = jsonResponse._id;
        sessionStorage.setItem('meme-user', JSON.stringify(user));
    });
});

QUnit.module('Meme functionalities', () => {
    QUnit.test('get all memes', async (assert) => {
        let path = '/data/memes/';
        let queryParams = '?sortBy=_createdOn%20desc';
        
        let response = await fetch(baseUrl + path + queryParams);
        
        assert.ok(response.ok, 'Successful response');
        
        let jsonResponse = await response.json();
        console.log(jsonResponse);
        
        assert.ok(Array.isArray(jsonResponse), 'Response is array');
        
        jsonResponse.forEach(element => {
            assert.ok(element.hasOwnProperty('description'), 'Property "description" exists');
            assert.strictEqual(typeof element.description, 'string', 'Property "description" has correct type');
            
            assert.ok(element.hasOwnProperty('imageUrl'), 'Property "imageUrl" exists');
            assert.strictEqual(typeof element.imageUrl, 'string', 'Property "imageUrl" has correct type');
            
            assert.ok(element.hasOwnProperty('title'), 'Property "title" exists');
            assert.strictEqual(typeof element.title, 'string', 'Property "title" has correct type');
            
            assert.ok(element.hasOwnProperty('_createdOn'), 'Property "_createdOn" exists');
            assert.strictEqual(typeof element._createdOn, 'number', 'Property "_createdOn" has correct type');
            
            assert.ok(element.hasOwnProperty('_id'), 'Property "_id" exists');
            assert.strictEqual(typeof element._id, 'string', 'Property "_id" has correct type');
            
            assert.ok(element.hasOwnProperty('_ownerId'), 'Property "_ownerId" exists');
            assert.strictEqual(typeof element._ownerId, 'string', 'Property "_ownerId" has correct type');
        });
    });
    
    QUnit.test('create a meme', async (assert) => {
        let path = '/data/meme';
        let random = Math.floor(Math.random() * 10000);
        meme.title = `Random_Title_${random}`;
        meme.description = `Random description${random}`;

        let response = await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(meme)
        });

        assert.ok(response.ok, 'Successful response');

        let jsonResponse = await response.json();
        console.log(jsonResponse);

        assert.ok(jsonResponse.hasOwnProperty('description'), 'Property exists');
        assert.strictEqual(typeof jsonResponse.description, 'string', 'Property has correct type');
        assert.strictEqual(jsonResponse.description, meme.description, 'Property has correc tvalue');

        assert.ok(jsonResponse.hasOwnProperty('imageUrl'), 'Property exists');
        assert.strictEqual(typeof jsonResponse.imageUrl, 'string', 'Property has correct type');
        assert.strictEqual(jsonResponse.imageUrl, meme.imageUrl, 'Property has correc tvalue');

        assert.ok(jsonResponse.hasOwnProperty('title'), 'Property exists');
        assert.strictEqual(typeof jsonResponse.title, 'string', 'Property has correct type');
        assert.strictEqual(jsonResponse.title, meme.title, 'Property has correc tvalue');

        assert.ok(jsonResponse.hasOwnProperty('_createdOn'), 'Property exists');
        assert.strictEqual(typeof jsonResponse._createdOn, 'number', 'Property has correct type');

        assert.ok(jsonResponse.hasOwnProperty('_id'), 'Property exists');
        assert.strictEqual(typeof jsonResponse._id, 'string', 'Property has correct type');

        assert.ok(jsonResponse.hasOwnProperty('_ownerId'), 'Property exists');
        assert.strictEqual(typeof jsonResponse._ownerId, 'string', 'Property has correct type');

        lastCreatedMemeId = jsonResponse._id;
    });
    
    QUnit.test('edit a meme', async (assert) => {
        let path = '/data/meme/';
        meme.description = 'Edited description';

        let response = await fetch(baseUrl + path + lastCreatedMemeId, {
            method: 'PUT',
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(meme)
        });

        assert.ok(response.ok, 'Successful response');

        let jsonResponse = await response.json();
        console.log(jsonResponse);

        assert.ok(jsonResponse.hasOwnProperty('description'), 'Property exists');
        assert.strictEqual(typeof jsonResponse.description, 'string', 'Property has correct type');
        assert.strictEqual(jsonResponse.description, meme.description, 'Property has correc tvalue');

        assert.ok(jsonResponse.hasOwnProperty('imageUrl'), 'Property exists');
        assert.strictEqual(typeof jsonResponse.imageUrl, 'string', 'Property has correct type');
        assert.strictEqual(jsonResponse.imageUrl, meme.imageUrl, 'Property has correc tvalue');

        assert.ok(jsonResponse.hasOwnProperty('title'), 'Property exists');
        assert.strictEqual(typeof jsonResponse.title, 'string', 'Property has correct type');
        assert.strictEqual(jsonResponse.title, meme.title, 'Property has correc tvalue');

        assert.ok(jsonResponse.hasOwnProperty('_createdOn'), 'Property exists');
        assert.strictEqual(typeof jsonResponse._createdOn, 'number', 'Property has correct type');

        assert.ok(jsonResponse.hasOwnProperty('_id'), 'Property exists');
        assert.strictEqual(typeof jsonResponse._id, 'string', 'Property has correct type');

        assert.ok(jsonResponse.hasOwnProperty('_ownerId'), 'Property exists');
        assert.strictEqual(typeof jsonResponse._ownerId, 'string', 'Property has correct type');

        assert.ok(jsonResponse.hasOwnProperty('_updatedOn'), 'Property exists');
        assert.strictEqual(typeof jsonResponse._updatedOn, 'number', 'Property _updatedOn has correct type');
    });
    
    QUnit.test('delete a meme', async (assert) => {
        let path = '/data/meme/';

        let response = await fetch(baseUrl + path + lastCreatedMemeId, {
            method: 'DELETE',
            headers: {
                'X-Authorization': token
            }
        });

        assert.ok(response.ok, 'Successful response')
    });
});
