const { isEven } = require("./test_functions");

QUnit.module("isEven functional tests", () => {
    QUnit.test("Even number as a parameter", function(assert){
        assert.equal(isEven(2), true, "Even number as a parameter")
    })
    QUnit.test("Odd number as a parameter", function(assert){
        assert.equal(isEven(5), false, "Odd number as a parameter")
    })
    QUnit.test("Zero as a parameter", function(assert){
        assert.equal(isEven(0), true, "Zero as a parameter")
    })
    QUnit.test("Negative even as a parameter", function(assert){
        assert.equal(isEven(-2), true, "Negative even as a parameter")
    })
    QUnit.test("Negative odd as a parameter", function(assert){
        assert.equal(isEven(-5), false, "Negative odd as a parameter")
    })
})