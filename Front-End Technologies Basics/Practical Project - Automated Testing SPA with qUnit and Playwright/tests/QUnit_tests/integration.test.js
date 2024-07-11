const baseUrl = 'http://localhost:3030/'

QUnit.config.reorder = false;

let user = {
    email: "",
    password: "123456"
}

let token = "";
let userId = "";

let game = {
    title: "",
    category: "",
    maxLevel: "77",
    imageUrl: "./images/ZombieLang.png",
    summary: ""
    
}

let lastCreatedGameId = "";

let gameIdForComments = "";

QUnit.module("User functionalities", () => {
    QUnit.test("registration", async (assert) => {
        //arrange
        let path = "users/register";
        let random = Math.floor(Math.random() * 10000);
        let email = `abv${random}@abv.bg`;
        user.email = email;
        
        //act
        let response = await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        
        let json = await response.json();
        
        console.log(json);
        
        //assert
        assert.ok(response.ok, "User registered successfully")
        
        assert.ok(json.hasOwnProperty('email'), 'Email exist');
        assert.equal(json['email'], user.email, 'Correct expected email');
        assert.strictEqual(typeof json.email, 'string', 'Email has correct property type');
        
        assert.ok(json.hasOwnProperty('password'), 'Password exist');
        assert.equal(json['password'], user.password, 'Correct expected password');
        assert.strictEqual(typeof json.password, 'string', 'Password has correct property type');
        
        assert.ok(json.hasOwnProperty('accessToken'), 'AccesToken exists');
        assert.strictEqual(typeof json.accessToken, 'string', 'AccessToken has correct property type');
        
        assert.ok(json.hasOwnProperty('_id'), '_id exists');
        assert.strictEqual(typeof json._id, 'string', 'ID has correct property type');
        
        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('game-user', JSON.stringify(user))
    });
    QUnit.test('login', async (assert) => {
        //arrange
        let path = 'users/login';
        
        //act
        let response = await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(user)
            
        })
        
        let json = await response.json();
        console.log(json);
        
        
        //assert
        assert.ok(response.ok, 'User logged-in successfully')
        
        
        assert.ok(json.hasOwnProperty('email'), 'Email exist');
        assert.equal(json['email'], user.email, 'Correct expected email');
        assert.strictEqual(typeof json.email, 'string', 'Email has correct property type');
        
        assert.ok(json.hasOwnProperty('password'), 'Password exist');
        assert.equal(json['password'], user.password, 'Correct expected password');
        assert.strictEqual(typeof json.password, 'string', 'Password has correct property type');
        
        assert.ok(json.hasOwnProperty('accessToken'), 'AccesToken exists');
        assert.strictEqual(typeof json.accessToken, 'string', 'AccessToken has correct property type');
        
        assert.ok(json.hasOwnProperty('_id'), '_id exists');
        assert.strictEqual(typeof json._id, 'string', 'ID has correct property type');
        
        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('game-user', JSON.stringify(user))
    });
});

QUnit.module('Games functionalities', () => {
    QUnit.test('Get all games', async (assert) => {
        //arrange
        let path = 'data/games';
        let queryParams = '?sortBy=_createdOn%20desc';
        
        let response = await fetch(baseUrl + path + queryParams);
        let json = await response.json();
        
        assert.ok(response.ok, 'Successfull response');
        assert.ok(Array.isArray(json), "Response is array");
        
        json.forEach(jsonProperty => {
            assert.ok(jsonProperty.hasOwnProperty('category'), 'Property category exists');
            assert.strictEqual(typeof jsonProperty.category, 'string', 'Property category has correct type');
            
            assert.ok(jsonProperty.hasOwnProperty('imageUrl'), 'Property imageUrl exists');
            assert.strictEqual(typeof jsonProperty.imageUrl, 'string', 'Property imageUrl has correct type');
            
            assert.ok(jsonProperty.hasOwnProperty('_ownerId'), 'Property _ownerId exists');
            assert.strictEqual(typeof jsonProperty._ownerId, 'string', 'Property _ownerId has correct type');
            
            assert.ok(jsonProperty.hasOwnProperty('title'), 'Property title exists');
            assert.strictEqual(typeof jsonProperty.title, 'string', 'Property title has correct type');
            
            assert.ok(jsonProperty.hasOwnProperty('maxLevel'), 'Property maxLevel exists');
            assert.strictEqual(typeof jsonProperty.maxLevel, 'string', 'Property maxLevel has correct type');
            
            assert.ok(jsonProperty.hasOwnProperty('summary'), 'Property summary exists');
            assert.strictEqual(typeof jsonProperty.summary, 'string', 'Property summary has correct type');
            
            assert.ok(jsonProperty.hasOwnProperty('_createdOn'), 'Property _createdOn exists');
            assert.strictEqual(typeof jsonProperty._createdOn, 'number', 'Property _createdOn has correct type');
            
            assert.ok(jsonProperty.hasOwnProperty('_id'), 'Property _id exists');
            assert.strictEqual(typeof jsonProperty._id, 'string', 'Property _id has correct type');
        });
        
    });
    
    QUnit.test('Create Game', async (assert) => {
        let path = 'data/games';
        let random = Math.floor(Math.random() * 10000);
        
        game.title = `Random game title ${random}`;
        game.category = `random game category ${random}`;
        game.summary = `Random game summary ${random}`;
        
        let response = await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(game)
        });
        let json = await response.json();
        lastCreatedGameId = json._id;
        
        assert.ok(response.ok, "Successfull response");
        
        assert.ok(json.hasOwnProperty('category'), 'Property category exists');
        assert.strictEqual(typeof json.category, 'string', 'The property category has correct type');
        assert.strictEqual(json.category, game.category, 'Property category has expected value');
        
        assert.ok(json.hasOwnProperty('imageUrl'), 'Property imageUrl exists');
        assert.strictEqual(typeof json.imageUrl, 'string', 'The property imageUrl has correct type');
        assert.strictEqual(json.imageUrl, game.imageUrl, 'Property imageUrl has expected value');
        
        assert.ok(json.hasOwnProperty('title'), 'Property title exists');
        assert.strictEqual(typeof json.title, 'string', 'The property title has correct type');
        assert.strictEqual(json.title, game.title, 'Property title has expected value');
        
        assert.ok(json.hasOwnProperty('maxLevel'), 'Property maxLevel exists');
        assert.strictEqual(typeof json.maxLevel, 'string', 'The property category has correct type');
        assert.strictEqual(json.maxLevel, game.maxLevel, 'Property maxLevel has expected value');
        
        assert.ok(json.hasOwnProperty('summary'), 'Property summary exists');
        assert.strictEqual(typeof json.summary, 'string', 'The property summary has correct type');
        assert.strictEqual(json.summary, game.summary, 'Property summary has expected value');
        
        assert.ok(json.hasOwnProperty('_createdOn'), 'Property _createdOn exists');
        assert.strictEqual(typeof json._createdOn, 'number', 'The property _createdOn has correct type');
        
        assert.ok(json.hasOwnProperty('_id'), 'Property _id exists');
        assert.strictEqual(typeof json._id, 'string', 'The property _id has correct type');
        
        assert.ok(json.hasOwnProperty('_ownerId'), 'Property _ownerId exists');
        assert.strictEqual(typeof json._ownerId, 'string', 'The property _ownerId has correct type');
    });
    
    QUnit.test('Get by Id', async (assert) => {
        let path = 'data/games';
        
        let response = await fetch(baseUrl + path + `/${lastCreatedGameId}`);
        let json = await response.json();
        
        assert.ok(response.ok, "Successfull response");
        
        assert.ok(json.hasOwnProperty('category'), 'Property category exists');
        assert.strictEqual(typeof json.category, 'string', 'The property category has correct type');
        assert.strictEqual(json.category, game.category, 'Property category has expected value');
        
        assert.ok(json.hasOwnProperty('imageUrl'), 'Property imageUrl exists');
        assert.strictEqual(typeof json.imageUrl, 'string', 'The property imageUrl has correct type');
        assert.strictEqual(json.imageUrl, game.imageUrl, 'Property imageUrl has expected value');
        
        assert.ok(json.hasOwnProperty('title'), 'Property title exists');
        assert.strictEqual(typeof json.title, 'string', 'The property title has correct type');
        assert.strictEqual(json.title, game.title, 'Property title has expected value');
        
        assert.ok(json.hasOwnProperty('maxLevel'), 'Property maxLevel exists');
        assert.strictEqual(typeof json.maxLevel, 'string', 'The property category has correct type');
        assert.strictEqual(json.maxLevel, game.maxLevel, 'Property maxLevel has expected value');
        
        assert.ok(json.hasOwnProperty('summary'), 'Property summary exists');
        assert.strictEqual(typeof json.summary, 'string', 'The property summary has correct type');
        assert.strictEqual(json.summary, game.summary, 'Property summary has expected value');
        
        assert.ok(json.hasOwnProperty('_createdOn'), 'Property _createdOn exists');
        assert.strictEqual(typeof json._createdOn, 'number', 'The property _createdOn has correct type');
        
        assert.ok(json.hasOwnProperty('_id'), 'Property _id exists');
        assert.strictEqual(typeof json._id, 'string', 'The property _id has correct type');
        
        assert.ok(json.hasOwnProperty('_ownerId'), 'Property _ownerId exists');
        assert.strictEqual(typeof json._ownerId, 'string', 'The property _ownerId has correct type');
        
        
    });
    
    QUnit.test('Edit game', async (assert) => {
        let path = 'data/games';
        let random = Math.floor(Math.random() * 10000);
        
        game.title = `Edited game title ${random}`;
        game.category = `Edited game category ${random}`;
        game.summary = `Edited game summary ${random}`;
        
        let response = await fetch( baseUrl + path + `/${lastCreatedGameId}`, {
            method: 'PUT',
            headers: {
                'content-type': 'application/jason',
                'X-Authorization': token
            },
            body: JSON.stringify(game)
            
        });
        
        assert.ok(response.ok, 'Game edited successfully');
        
        let json = await response.json();
        
        
        assert.ok(json.hasOwnProperty('category'), 'Property category exists');
        assert.strictEqual(typeof json.category, 'string', 'The property category has correct type');
        assert.strictEqual(json.category, game.category, 'Property category has expected value');
        
        assert.ok(json.hasOwnProperty('imageUrl'), 'Property imageUrl exists');
        assert.strictEqual(typeof json.imageUrl, 'string', 'The property imageUrl has correct type');
        assert.strictEqual(json.imageUrl, game.imageUrl, 'Property imageUrl has expected value');
        
        assert.ok(json.hasOwnProperty('title'), 'Property title exists');
        assert.strictEqual(typeof json.title, 'string', 'The property title has correct type');
        assert.strictEqual(json.title, game.title, 'Property title has expected value');
        
        assert.ok(json.hasOwnProperty('maxLevel'), 'Property maxLevel exists');
        assert.strictEqual(typeof json.maxLevel, 'string', 'The property category has correct type');
        assert.strictEqual(json.maxLevel, game.maxLevel, 'Property maxLevel has expected value');
        
        assert.ok(json.hasOwnProperty('summary'), 'Property summary exists');
        assert.strictEqual(typeof json.summary, 'string', 'The property summary has correct type');
        assert.strictEqual(json.summary, game.summary, 'Property summary has expected value');
        
        assert.ok(json.hasOwnProperty('_createdOn'), 'Property _createdOn exists');
        assert.strictEqual(typeof json._createdOn, 'number', 'The property _createdOn has correct type');
        
        assert.ok(json.hasOwnProperty('_id'), 'Property _id exists');
        assert.strictEqual(typeof json._id, 'string', 'The property _id has correct type');
        
        assert.ok(json.hasOwnProperty('_ownerId'), 'Property _ownerId exists');
        assert.strictEqual(typeof json._ownerId, 'string', 'The property _ownerId has correct type');
        
    });
    
    QUnit.test('Delete game', async (assert) => {
        let path = 'data/games';
        
        let response = await fetch(baseUrl + path + `/${lastCreatedGameId}`, {
            method: 'DELETE',
            headers: {
                'content-type': 'applicattion.json',
                'X-Authorization': token
            }
            
        });
        
        assert.ok(response.ok, 'Game deleted successfully')
    });
    
});

QUnit.module('Comments fuctionalities', () => {
    QUnit.test('Nawly created game - no comments (empty)', async (assert) => {
        let path = 'data/comments';
        
        let random = Math.floor(Math.random() * 10000);
        
        game.title = `Random game title ${random}`;
        game.category = `random game category ${random}`;
        game.summary = `Random game summary ${random}`;
        
        let gameId = (await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(game)
        })
        .then(response => response.json()))._id;
        
        gameIdForComments = gameId;
        
        let queryParams = `?where=gameId%3D%22${gameIdForComments}%22`;
        
        let response = await fetch(baseUrl + path + queryParams);
        let json = await response.json();
        
        assert.ok(response.ok, 'Success on response');
        assert.ok(Array.isArray(json), "Response is array");
        assert.ok(json.length == 0, 'Array is empty');
    });
    
    QUnit.test('Create new comment', async (assert) => {
        let path = 'data/comments';
        let random = Math.floor(Math.random() * 10000);
        let comment = {
            gameId: gameIdForComments,
            comment: `New comment ${random}` 
        };
        
        let response = await fetch(baseUrl + path, {
            method: 'POST',
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(comment)
        });
        
        let json = await response.json();
        
        assert.ok(response.ok, 'Comment added successfully ');
        
        
        assert.ok(json.hasOwnProperty('_ownerId'), 'Property _ownerId exists');
        assert.strictEqual(typeof json._ownerId, 'string', 'The property _ownerId has correct type');
        
        assert.ok(json.hasOwnProperty('gameId'), 'Property gameId exists');
        assert.strictEqual(typeof json.gameId, 'string', 'The property gameId has correct type');
        assert.strictEqual(json.gameId, comment.gameId, 'Property gameId has expected value');
        
        assert.ok(json.hasOwnProperty('comment'), 'Property comment exists');
        assert.strictEqual(typeof json.comment, 'string', 'The property comment has correct type');
        assert.strictEqual(json.comment, comment.comment, 'Property comment has expected value');
        
        assert.ok(json.hasOwnProperty('_createdOn'), 'Property _createdOn exists');
        assert.strictEqual(typeof json._createdOn, 'number', 'The property _createdOn has correct type');
        
        assert.ok(json.hasOwnProperty('_id'), 'Property _id exists');
        assert.strictEqual(typeof json._id, 'string', 'The property _id has correct type');
    });
    
    QUnit.test('Get comments by game ID', async (assert) => {
        let path = 'data/comments';
        let queryParams = `?where=gameId%3D%22${gameIdForComments}%22`
        
        let response = await fetch(baseUrl + path + queryParams);
        let json = await response.json();
        
        assert.ok(response.ok, 'Request success');
        assert.ok(Array.isArray(json), 'The response is array');
        
        json.forEach(comment => {
            assert.ok(comment.hasOwnProperty('_ownerId'), 'Property _ownerId exists');
            assert.strictEqual(typeof comment._ownerId, 'string', 'The property _ownerId has correct type');
            
            assert.ok(comment.hasOwnProperty('gameId'), 'Property gameId exists');
            assert.strictEqual(typeof comment.gameId, 'string', 'The property gameId has correct type');
            
            assert.ok(comment.hasOwnProperty('comment'), 'Property comment exists');
            assert.strictEqual(typeof comment.comment, 'string', 'The property comment has correct type');
            
            assert.ok(comment.hasOwnProperty('_createdOn'), 'Property _createdOn exists');
            assert.strictEqual(typeof comment._createdOn, 'number', 'The property _createdOn has correct type');
            
            assert.ok(comment.hasOwnProperty('_id'), 'Property _id exists');
            assert.strictEqual(typeof comment._id, 'string', 'The property _id has correct type');
        });
    })
    
})






