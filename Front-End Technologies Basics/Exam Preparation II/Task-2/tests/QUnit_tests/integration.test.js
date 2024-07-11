const baseUrl = 'http://localhost:3030';

QUnit.config.reorder = false;

let user = {
    email: '',
    password: '123456',
};

let eevent = {
    author: '',
    date: '24.06.2024',
    title: '',
    description: '',
    imageUrl: '/images/2.png'
};

let userId = '';

let token = '';


let lastCreatedEventId = '';

QUnit.module('user functionalities', () => {
    QUnit.test('user registration', async (assert) => {
        let path = '/users/register';
        let random = Math.floor(Math.random() * 10000);
        let randomEmail = `abv${random}@abv.bg`;
        
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
        
        assert.ok(jsonResponse.hasOwnProperty('email'), '"Email" property exists');
        assert.equal(jsonResponse['email'], user.email, '"Email" property has expected value');
        assert.strictEqual(typeof jsonResponse.email, 'string', '"Email" has expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('password'), '"Password" property exists');
        assert.equal(jsonResponse['password'], user.password, '"Password" property has expected value');
        assert.strictEqual(typeof jsonResponse.password, 'string', '"Password" has expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('_id'), '_id exists');
        assert.strictEqual(typeof jsonResponse._id, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('accessToken'), 'accessToken exists');
        assert.strictEqual(typeof jsonResponse.accessToken, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('_createdOn'), '_createdOn exists');
        assert.strictEqual(typeof jsonResponse._createdOn, 'number', 'Expected property type');
        
        token = jsonResponse.accessToken;
        userId = jsonResponse._id;
        sessionStorage.setItem('theater-user', JSON.stringify(user));
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
        
        assert.ok(jsonResponse.hasOwnProperty('email'), '"Email" property exists');
        assert.equal(jsonResponse['email'], user.email, '"Email" property has expected value');
        assert.strictEqual(typeof jsonResponse.email, 'string', '"Email" has expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('password'), '"Password" property exists');
        assert.equal(jsonResponse['password'], user.password, '"Password" property has expected value');
        assert.strictEqual(typeof jsonResponse.password, 'string', '"Password" has expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('_id'), '_id exists');
        assert.strictEqual(typeof jsonResponse._id, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('accessToken'), 'accessToken exists');
        assert.strictEqual(typeof jsonResponse.accessToken, 'string', 'Expected property type');
        
        assert.ok(jsonResponse.hasOwnProperty('_createdOn'), '_createdOn exists');
        assert.strictEqual(typeof jsonResponse._createdOn, 'number', 'Expected property type');
        
        token = jsonResponse.accessToken;
        userId = jsonResponse._id;
        sessionStorage.setItem('theater-user', JSON.stringify(user));
    });
});

QUnit.module('Event functionalities', () => {
    QUnit.test('get all events', async (assert) => {
        let path = '/data/theaters/';
        let queryParams = '?sortBy=_createdOn%20desc&distinct=title';
        
        let response = await fetch(baseUrl + path + queryParams);
        
        assert.ok(response.ok, 'Successful response');
        
        let jsonResponse = await response.json();
        console.log(jsonResponse);
        
        assert.ok(Array.isArray(jsonResponse), 'Response is array');
        
        jsonResponse.forEach(element => {
            assert.ok(element.hasOwnProperty('author'), 'Property "author" exists');
            assert.strictEqual(typeof element.author, 'string', 'Property "author" has correct type');

            assert.ok(element.hasOwnProperty('date'), 'Property "date" exists');
            assert.strictEqual(typeof element.date, 'string', 'Property "date" has correct type');

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

    QUnit.test('create an event', async (assert) => {
        let path = '/data/theaters';
        let random = Math.floor(Math.random() * 10000);
        eevent.author = `author${random}`;
        eevent.title = `title${random}`;
        eevent.description = `description${random}`;

        let response = await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(eevent)
        });

        assert.ok(response.ok, 'Successful response');

        let jsonResponse = await response.json();
        console.log(jsonResponse);

            assert.ok(jsonResponse.hasOwnProperty('author'), 'Property "author" exists');
            assert.strictEqual(typeof jsonResponse.author, 'string', 'Property "author" has correct type');
            assert.equal(jsonResponse['author'], eevent.author, '"Author" property has expected value');

            assert.ok(jsonResponse.hasOwnProperty('date'), 'Property "date" exists');
            assert.strictEqual(typeof jsonResponse.date, 'string', 'Property "date" has correct type');
            assert.equal(jsonResponse['date'], eevent.date, '"Date" property has expected value');

            assert.ok(jsonResponse.hasOwnProperty('description'), 'Property "description" exists');
            assert.strictEqual(typeof jsonResponse.description, 'string', 'Property "description" has correct type');
            assert.equal(jsonResponse['description'], eevent.description, '"Description" property has expected value');
            
            assert.ok(jsonResponse.hasOwnProperty('imageUrl'), 'Property "imageUrl" exists');
            assert.strictEqual(typeof jsonResponse.imageUrl, 'string', 'Property "imageUrl" has correct type');
            assert.equal(jsonResponse['imageUrl'], eevent.imageUrl, '"imageUrl" property has expected value');
            
            assert.ok(jsonResponse.hasOwnProperty('title'), 'Property "title" exists');
            assert.strictEqual(typeof jsonResponse.title, 'string', 'Property "title" has correct type');
            assert.equal(jsonResponse['title'], eevent.title, '"Title" property has expected value');
            
            assert.ok(jsonResponse.hasOwnProperty('_createdOn'), 'Property "_createdOn" exists');
            assert.strictEqual(typeof jsonResponse._createdOn, 'number', 'Property "_createdOn" has correct type');
            
            assert.ok(jsonResponse.hasOwnProperty('_id'), 'Property "_id" exists');
            assert.strictEqual(typeof jsonResponse._id, 'string', 'Property "_id" has correct type');
            
            assert.ok(jsonResponse.hasOwnProperty('_ownerId'), 'Property "_ownerId" exists');
            assert.strictEqual(typeof jsonResponse._ownerId, 'string', 'Property "_ownerId" has correct type');

        lastCreatedEventId = jsonResponse._id;
    });

    QUnit.test('edit an event', async (assert) => {
        let path = '/data/theaters/';
        eevent.description = 'Edited description';

        let response = await fetch(baseUrl + path + lastCreatedEventId, {
            method: 'PUT',
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(eevent)
        });

        assert.ok(response.ok, 'Successful response');

        let jsonResponse = await response.json();
        console.log(jsonResponse);

        assert.ok(jsonResponse.hasOwnProperty('author'), 'Property "author" exists');
        assert.strictEqual(typeof jsonResponse.author, 'string', 'Property "author" has correct type');
        assert.equal(jsonResponse['author'], eevent.author, '"Author" property has expected value');

        assert.ok(jsonResponse.hasOwnProperty('date'), 'Property "date" exists');
        assert.strictEqual(typeof jsonResponse.date, 'string', 'Property "date" has correct type');
        assert.equal(jsonResponse['date'], eevent.date, '"Date" property has expected value');

        assert.ok(jsonResponse.hasOwnProperty('description'), 'Property "description" exists');
        assert.strictEqual(typeof jsonResponse.description, 'string', 'Property "description" has correct type');
        assert.equal(jsonResponse['description'], eevent.description, '"Description" property has expected value');
        
        assert.ok(jsonResponse.hasOwnProperty('imageUrl'), 'Property "imageUrl" exists');
        assert.strictEqual(typeof jsonResponse.imageUrl, 'string', 'Property "imageUrl" has correct type');
        assert.equal(jsonResponse['imageUrl'], eevent.imageUrl, '"imageUrl" property has expected value');
        
        assert.ok(jsonResponse.hasOwnProperty('title'), 'Property "title" exists');
        assert.strictEqual(typeof jsonResponse.title, 'string', 'Property "title" has correct type');
        assert.equal(jsonResponse['title'], eevent.title, '"Title" property has expected value');
        
        assert.ok(jsonResponse.hasOwnProperty('_createdOn'), 'Property "_createdOn" exists');
        assert.strictEqual(typeof jsonResponse._createdOn, 'number', 'Property "_createdOn" has correct type');
        
        assert.ok(jsonResponse.hasOwnProperty('_id'), 'Property "_id" exists');
        assert.strictEqual(typeof jsonResponse._id, 'string', 'Property "_id" has correct type');
        
        assert.ok(jsonResponse.hasOwnProperty('_ownerId'), 'Property "_ownerId" exists');
        assert.strictEqual(typeof jsonResponse._ownerId, 'string', 'Property "_ownerId" has correct type');
    });

    QUnit.test('delete an event', async (assert) => {
        let path = '/data/theaters/';

        let response = await fetch(baseUrl + path + lastCreatedEventId, {
            method: 'DELETE',
            headers: {
                'X-Authorization': token
            }
        });

        assert.ok(response.ok, 'Successful response')
    });
});