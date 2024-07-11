const baseUrl = "http://localhost:3030/";

let user = {
    email: "",
    password: "123456"
};

let token = '';
let userId = '';

let book = {
    title: '',
    description: '',
    imageUrl: '/images/book.png',
    type: 'Other'
};

let lastCreatedBookId = '';


QUnit.config.reorder = false;

QUnit.module("User functionalities", () => {
    QUnit.test("Registration", async (assert) => {
        //arrange
        let path = 'users/register';

        let random = Math.floor(Math.random() * 10000);
        let email = `abv${random}@abv.bg`;
        user.email = email;

        //act
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        let json = await response.json();
        console.log(json);

        //assert
        assert.ok(response.ok);

        assert.ok(json.hasOwnProperty('email'), '"Email" property exists');
        assert.equal(json['email'], user.email, '"Email" property has expected value');
        assert.strictEqual(typeof json.email, 'string', '"Email" propeerty has correct type');

        assert.ok(json.hasOwnProperty('password'), '"Password" property exists');
        assert.equal(json['password'], user.password, '"Password" property has expected value');
        assert.strictEqual(typeof json.password, 'string', '"Password" property has correct type');

        assert.ok(json.hasOwnProperty('_createdOn'), '"_createdOn" property exists');
        assert.strictEqual(typeof json._createdOn, 'number', '"_createdOn" propery has correct type');

        assert.ok(json.hasOwnProperty('_id'), '"_id" property exists');
        assert.strictEqual(typeof json._id, 'string', '"_id" property has correct type');

        assert.ok(json.hasOwnProperty('accessToken'), '"accessToken" propery exists');
        assert.strictEqual(typeof json.accessToken, 'string', '"accessToken" property has correct type');

        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('library-user', JSON.stringify(user));
    });

    QUnit.test("Login", async (assert) => {
        //arrange
        let path = 'users/login';

        //act
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        let json = await response.json();
        console.log(json);

        //assert
        assert.ok(response.ok);

        assert.ok(json.hasOwnProperty('email'), '"Email" property exists');
        assert.equal(json['email'], user.email, '"Email" property has expected value');
        assert.strictEqual(typeof json.email, 'string', '"Email" propeerty has correct type');

        assert.ok(json.hasOwnProperty('password'), '"Password" property exists');
        assert.equal(json['password'], user.password, '"Password" property has expected value');
        assert.strictEqual(typeof json.password, 'string', '"Password" property has correct type');

        assert.ok(json.hasOwnProperty('_createdOn'), '"_createdOn" property exists');
        assert.strictEqual(typeof json._createdOn, 'number', '"_createdOn" propery has correct type');

        assert.ok(json.hasOwnProperty('_id'), '"_id" property exists');
        assert.strictEqual(typeof json._id, 'string', '"_id" property has correct type');

        assert.ok(json.hasOwnProperty('accessToken'), '"accessToken" propery exists');
        assert.strictEqual(typeof json.accessToken, 'string', '"accessToken" property has correct type');

        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('library-user', JSON.stringify(user));
    });
});

QUnit.module("Book functionalities", () => {
    QUnit.test("Get all books", async (assert) => {
        let path = 'data/books';
        let queryParams = '?sortBy=_createdOn%20desc';

        let response = await fetch(baseUrl + path + queryParams);
        let json = await response.json();
        console.log(json);

        assert.ok(response.ok, "Response is ok");
        assert.ok(Array.isArray(json), "Response is array");

        json.forEach(jsonData => {
            assert.ok(jsonData.hasOwnProperty('description'), '"Description" property exists');
            assert.strictEqual(typeof jsonData.description, 'string', '"Description" property has correct type');

            assert.ok(jsonData.hasOwnProperty('imageUrl'), '"imageUrl" propery exists');
            assert.strictEqual(typeof jsonData.imageUrl, 'string', '"imageUrl" property has correct type');

            assert.ok(jsonData.hasOwnProperty('title'), '"Title" property exists');
            assert.strictEqual(typeof jsonData.title, 'string', '"Title" property has correct type');

            assert.ok(jsonData.hasOwnProperty('type'), '"Type" property exists');
            assert.strictEqual(typeof jsonData.type, 'string', '"Type" property has correct type');

            assert.ok(jsonData.hasOwnProperty('_createdOn'), '"_createdOn" property exists');
            assert.strictEqual(typeof jsonData._createdOn, 'number', '"_createdOn" property has correct type');

            assert.ok(jsonData.hasOwnProperty('_id'), '"_id" property exists');
            assert.strictEqual(typeof jsonData._id, 'string', '"_id" property has correct type');

            assert.ok(jsonData.hasOwnProperty('_ownerId'), '"_ownerId" property exists');
            assert.strictEqual(typeof jsonData._ownerId, 'string', '"_ownerId" property has correct type');
        });
    })

    QUnit.test("Create a book", async (assert) => {
        let path = "data/books";
        let random = Math.floor(Math.random() * 10000);
        book.title = `Random title ${random}`;
        book.description = `Random descr ${random}`;

        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(book)
        })
        let jsonData = await response.json();
        console.log(jsonData);

        assert.ok(response.ok, "Response is ok");

            assert.ok(jsonData.hasOwnProperty('description'), '"Description" property exists');
            assert.strictEqual(typeof jsonData.description, 'string', '"Description" property has correct type');
            assert.strictEqual(jsonData.description, book.description, '"Description" property has expected value')

            assert.ok(jsonData.hasOwnProperty('imageUrl'), '"imageUrl" propery exists');
            assert.strictEqual(typeof jsonData.imageUrl, 'string', '"imageUrl" property has correct type');
            assert.strictEqual(jsonData.imageUrl, book.imageUrl, '"imageUrl" property has expected value')

            assert.ok(jsonData.hasOwnProperty('title'), '"Title" property exists');
            assert.strictEqual(typeof jsonData.title, 'string', '"Title" property has correct type');
            assert.strictEqual(jsonData.title, book.title, '"Title" property has expected value')

            assert.ok(jsonData.hasOwnProperty('type'), '"Type" property exists');
            assert.strictEqual(typeof jsonData.type, 'string', '"Type" property has correct type');
            assert.strictEqual(jsonData.type, book.type, '"Type" property has expected value')

            assert.ok(jsonData.hasOwnProperty('_createdOn'), '"_createdOn" property exists');
            assert.strictEqual(typeof jsonData._createdOn, 'number', '"_createdOn" property has correct type');

            assert.ok(jsonData.hasOwnProperty('_id'), '"_id" property exists');
            assert.strictEqual(typeof jsonData._id, 'string', '"_id" property has correct type');

            assert.ok(jsonData.hasOwnProperty('_ownerId'), '"_ownerId" property exists');
            assert.strictEqual(typeof jsonData._ownerId, 'string', '"_ownerId" property has correct type');

        lastCreatedBookId = jsonData._id;
    })

    QUnit.test("Edit the book", async (assert) => {
        let path = 'data/books';
        let random = Math.floor(Math.random() * 10000);
        book.title = 'Edited title';

        let response = await fetch(baseUrl + path + `/${lastCreatedBookId}`, {
            method: "PUT",
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(book)
        })
        let jsonData = await response.json();

        assert.ok(response.ok, "Response is ok");

        assert.ok(jsonData.hasOwnProperty('description'), '"Description" property exists');
            assert.strictEqual(typeof jsonData.description, 'string', '"Description" property has correct type');
            assert.strictEqual(jsonData.description, book.description, '"Description" property has expected value')

            assert.ok(jsonData.hasOwnProperty('imageUrl'), '"imageUrl" propery exists');
            assert.strictEqual(typeof jsonData.imageUrl, 'string', '"imageUrl" property has correct type');
            assert.strictEqual(jsonData.imageUrl, book.imageUrl, '"imageUrl" property has expected value')

            assert.ok(jsonData.hasOwnProperty('title'), '"Title" property exists');
            assert.strictEqual(typeof jsonData.title, 'string', '"Title" property has correct type');
            assert.strictEqual(jsonData.title, book.title, '"Title" property has expected value')

            assert.ok(jsonData.hasOwnProperty('type'), '"Type" property exists');
            assert.strictEqual(typeof jsonData.type, 'string', '"Type" property has correct type');
            assert.strictEqual(jsonData.type, book.type, '"Type" property has expected value')

            assert.ok(jsonData.hasOwnProperty('_createdOn'), '"_createdOn" property exists');
            assert.strictEqual(typeof jsonData._createdOn, 'number', '"_createdOn" property has correct type');

            assert.ok(jsonData.hasOwnProperty('_id'), '"_id" property exists');
            assert.strictEqual(typeof jsonData._id, 'string', '"_id" property has correct type');

            assert.ok(jsonData.hasOwnProperty('_ownerId'), '"_ownerId" property exists');
            assert.strictEqual(typeof jsonData._ownerId, 'string', '"_ownerId" property has correct type');

        lastCreatedBookId = jsonData._id;
    })

    QUnit.test("Delete event", async (assert) => {
        let path = "data/books";

        let response = await fetch(baseUrl + path + `/${lastCreatedBookId}`, {
            method: "DELETE",
            headers: {
                'X-Authorization' : token
            }
        });

        assert.ok(response.ok)
    });
});