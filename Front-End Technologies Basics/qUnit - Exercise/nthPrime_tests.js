const { nthPrime } = require('./test_functions');

QUnit.module("nthPrime functional tests", () => {
    QUnit.test("1 as a param", function(assert){
        assert.equal(nthPrime(1), 2, "1 as a param")
    })
    QUnit.test("5 as a param", function(assert){
        assert.equal(nthPrime(5), 11, "5 as a param")
    })
    QUnit.test("11 as a param", function(assert){
        assert.equal(nthPrime(11), 31, "11 as a param")
    })
})