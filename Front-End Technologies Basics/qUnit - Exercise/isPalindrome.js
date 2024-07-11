const { isPolindrome } = require("./test_functions");

QUnit.module('sum function tests', () => {
    QUnit.test("Adding two possitive numbers", function(assert) {
        assert.ok(isPolindrome('racecar'), true, "'racecar' as a param")
    })
    QUnit.test("'A man, a plan, a canal, Panama!' as a param", function(assert) {
        assert.ok(isPolindrome('A man, a plan, a canal, Panama!'), true, "'A man, a plan, a canal, Panama!' as a param")
    })
    QUnit.test("'hello' as a param", function(assert) {
        assert.notOk(isPolindrome('hello'), false, "'hello' as a param")
    })
    QUnit.test("'' as a param", function(assert) {
        assert.notOk(isPolindrome(''), false, "'' as a param")
    })

})
