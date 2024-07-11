const { isPerfectSquare } = require('./test_functions');

QUnit.module("isPerfectSquare functional ests", () => {
    QUnit.test("1", function(assert){
        assert.ok(isPerfectSquare(1), true, "1")
    })
    QUnit.test("4", function(assert){
        assert.ok(isPerfectSquare(4), true, "4")
    })
    QUnit.test("9", function(assert){
        assert.ok(isPerfectSquare(9), true, "9")
    })
    QUnit.test("16", function(assert){
        assert.ok(isPerfectSquare(16), true, "16")
    })
    QUnit.test("2", function(assert){
        assert.notOk(isPerfectSquare(2), false, "2")
    })
    QUnit.test("15", function(assert){
        assert.notOk(isPerfectSquare(15), false, "15")
    })
})