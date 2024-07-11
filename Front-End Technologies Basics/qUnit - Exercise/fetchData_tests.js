const { assert } = require("qunit");
const { fetchData } = require("./async_test_functions.js");

QUnit.module("Fetch Data Function Tests for Bulgarian Post Code", () => {
    QUnit.test("Fetch Data Function Tests for Bulgarian Post Code", async function(assert) { 
        const data = await fetchData('https://www.zippopotam.us/bg/8000')
        
        //    console.log(data); 
        
        assert.ok(data.hasOwnProperty("post code"));
        assert.equal(data["post code"], 8000);
        assert.ok(data.hasOwnProperty("country"));
        assert.equal(data["country"], "Bulgaria");
        assert.ok(data.hasOwnProperty("country abbreviation"));
        assert.ok(data["country abbreviation"], "BG");

        assert.ok(Array.isArray(data.places));
        assert.equal(data.places.length, 1);

        const place = data.places[0];
        assert.ok(place.hasOwnProperty("place name"));
        assert.equal(place["place name"], 'Бургас / Burgas');
        assert.ok(place.hasOwnProperty("longitude"));
        assert.equal(place["longitude"], '27.4667');
        assert.ok(place.hasOwnProperty("state"));
        assert.equal(place["state"], 'Бургас / Burgas');
        assert.ok(place.hasOwnProperty("state abbreviation"));
        assert.equal(place["state abbreviation"], "BGS");
        assert.ok(place.hasOwnProperty("latitude"));
        assert.equal(place["latitude"], '42.5');
        
    }); 

    QUnit.test("Fetch Data Function Tests for unexisting Post Code", async () => {
        const data = await fetchData('https://www.zippopotam.us/bg/8000999');

        assert.notOk(data);
        assert.true(data === undefined);
    })

    QUnit.test("Fetch Data Function Tests for unexisting URL", async () => {
        const data = await fetchData('https://wwww.zippopotam.us/bg/8000');

        assert.equal(data, "fetch failed");
    })
    
})







// // Check main object properties
// assert.ok(data.hasOwnProperty('post code'), "Data contains 'post code'");
// assert.equal(data['post code'], '8000', "'post code' is 8000'" );
// assert.ok(data.hasOwnProperty('country'), "Data contains 'country'");
// assert.equal(data['country'], 'Bulgaria', "'country' is 'Bulgaria'");
// assert.ok(data.hasOwnProperty('country abbreviation'), "Data contains 'country abbreviation'"); 
// assert.equal(data['country abbreviation'], 'BG', "country abbreviation' is 'BG'");
// // Check places array
// assert.ok(Array.isArray (data.places), "'places' is an array");
// assert.equal(data.places.length, 1, "'places' array has one element");
// });
// const place
// =
// data.places[0];
// assert.ok(place.hasOwnProperty('place name'), "Place contains 'place name'");
// assert.equal(place['place name'], 'бyprac / Burgas', "'place name' is 'бyprac / Burgas'");
// assert.ok(place.hasOwnProperty('longitude'), "Place contains 'longitude'");
// assert.equal(place['longitude'], '27.4667', "'longitude' is '27.4667'");
// assert.ok(place.hasOwnProperty('state'), "Place contains 'state'");
// assert.equal(place['state'], 'byprac / Burgas', "'state' is 'byprac / Burgas'");
// assert.ok(place.hasOwnProperty('state abbreviation'), "Place contains 'state abbreviation'");
// assert.equal(place['state abbreviation'], 'BGS', "state abbreviation' is 'BGS'");
// assert.ok(place.hasOwnProperty("latitude"), "Place contains 'latitude'");
// assert.equal(place['latitude'], '42.5', "latitude' is '42.5'");