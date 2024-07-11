const {fake_delay} = require("./async_test_functions.js");

QUnit.module("fakeDelay tests", () => {
    QUnit.test("1st", async function(assert){
        const start = Date.now();
        await fake_delay(2000);
        const end = Date.now();
        const difference = end - start;
        assert.ok(difference >= 2000, "delay 2s")
    })
})