const{pascalTriangle} = require("./test_functions");

QUnit.module("pascalTriangle functional tests", function(){
    QUnit.test("0 as a param", function(assert){
        assert.deepEqual(pascalTriangle(0), [], "0 as a param")
    })
    QUnit.test("1 as a param", function(assert){
        assert.deepEqual(pascalTriangle(1), [[1]], "1 as a param")
    })
    QUnit.test("5 as a param", function(assert){
        assert.deepEqual(pascalTriangle(5), [[1], [1, 1], [1, 2, 1], [1, 3, 3, 1], [1, 4, 6, 4, 1]], "5 as a param" )
})
// QUnit.test("8 as a param", function(assert){
//     assert.deepEqual(pascalTriangle(8), , "8 as a param")

// })
})

// const { pascalTriangle } = require('./test_functions.js');

// QUnit.module("pascalTriangle functional tests", () => {
//     QUnit.test("0 as a param", function(assert) {
//         assert.deepEqual(pascalTriangle(0), [], "0 as a param");
//     });

//     QUnit.test("1 as a param", function(assert) {
//         assert.deepEqual(pascalTriangle(1), [[1]], "1 as a param");
//     });

//     QUnit.test("5 as a param", function(assert) {
//         assert.deepEqual(pascalTriangle(5), [
//             [1],
//             [1, 1],
//             [1, 1, 1],
//             [1, 1, 1, 1],
//             [1, 1, 1, 1, 1]
//         ], "5 as a param");
//     });

//     QUnit.test("8 as a param", function(assert) {
//         assert.deepEqual(pascalTriangle(8), [
//             [1],
//             [1, 1],
//             [1, 1, 1],
//             [1, 1, 1, 1],
//             [1, 1, 1, 1, 1],
//             [1, 1, 1, 1, 1, 1],
//             [1, 1, 1, 1, 1, 1, 1],
//             [1, 1, 1, 1, 1, 1, 1, 1]
//         ], "8 as a param");
//     });
// });