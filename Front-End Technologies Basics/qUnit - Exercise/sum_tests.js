const { sum } = require("./test_functions");

QUnit.module('sum function tests', () => {
    QUnit.test("Adding two possitive numbers", function(assert){
        assert.equal(sum(2, 3), 5, "Adding two possitive numbers")
    })
    QUnit.test("Adding possitive and negative numbers", function(assert){
        assert.equal(sum(5, -3), 2, "Adding possitive and negative numbers")
    })
    QUnit.test("Adding two negative numbers", function(assert){
        assert.equal(sum(-5, -3), -8, "Adding two negative numbers")
    })
    QUnit.test("Adding two real numbers", function(assert){
        assert.equal(Math.floor(sum(0.2, 0.1)*10), 3, "Adding two real numbers")
    })
})
