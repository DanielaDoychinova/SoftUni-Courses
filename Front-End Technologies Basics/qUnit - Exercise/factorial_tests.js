const { assert } = require('qunit');
const { factorial } = require('./test_functions.js');

QUnit.module("Factorial functional tests", () =>{
    QUnit.test("5 as a parameter", (assert) =>{
        assert.equal(factorial(5), 120, "5 as a parameter")
    })
    QUnit.test("0 as a parameter", (assert) =>{
        assert.equal(factorial(0), 1, "0 as a parameter")
    })
    QUnit.test("-1 as a parameter", (assert) =>{
        assert.equal(factorial(-1), 1, "-1 as a parameter")
    })
})