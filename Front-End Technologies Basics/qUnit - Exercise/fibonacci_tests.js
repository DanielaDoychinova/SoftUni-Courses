const { fibonacci } = require('./test_functions');

QUnit.module('Fibonacci func tests', () => {
    QUnit.test("zero as a param", function(assert){
        assert.deepEqual(fibonacci(0), [], "zero as a param")
    })
    QUnit.test("one as a param", function(assert){
        assert.deepEqual(fibonacci(1), [0, 1], "one as a param")
    })
    QUnit.test("5 as a param", function(assert){
        assert.deepEqual(fibonacci(5), [0, 1, 1, 2, 3], "5 as a param")
    })
    QUnit.test("10 as a param", function(assert){
        assert.deepEqual(fibonacci(10), [0, 1, 1, 2, 3, 5, 8, 13, 21, 34], "10 as a param")
    })
})