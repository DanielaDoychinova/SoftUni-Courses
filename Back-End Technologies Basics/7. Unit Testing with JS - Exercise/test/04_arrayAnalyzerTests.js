import { expect } from "chai";
import { analyzeArray } from "../functions/04_arrayAnalyzer.js";

describe('ArrayAnalyzer function tests', () => {
    it('should return undefined when pass an empty array', () => {
        expect(analyzeArray([])).to.be.undefined;

    });
    it('should return undefined when pass an array with non-numbers', () => {
        expect(analyzeArray(['3', '2'])).to.be.undefined;
        expect(analyzeArray(['3'])).to.be.undefined;
        expect(analyzeArray(['3', 2])).to.be.undefined;
        expect(analyzeArray([true, 3])).to.be.undefined;
        expect(analyzeArray([null, 3])).to.be.undefined;
    });
    it('should return undefined when pass non-array input', () => {
        expect(analyzeArray('str')).to.be.undefined;
        expect(analyzeArray(5)).to.be.undefined;
        expect(analyzeArray({})).to.be.undefined;
        expect(analyzeArray(null)).to.be.undefined;
    });
    it('should return correct object when pass a single element array', () => {
        expect(analyzeArray([0])).to.deep.equal({ min: 0, max: 0, length: 1 });

    });
    it('shuld return correct object when pass correct input', () => {
        expect(analyzeArray([5, 1, 4, 10])).to.deep.equal({ min: 1, max: 10, length: 4 });
        expect(analyzeArray([-5, 1, 0, 10])).to.deep.equal({ min: -5, max: 10, length: 4 });
        expect(analyzeArray([-5, -1, -4, -10])).to.deep.equal({ min: -10, max: -1, length: 4 });
        expect(analyzeArray([1.5, -11.6, 0.58, 18.99])).to.deep.equal({ min: -11.6, max: 18.99, length: 4 });
    })
    it('shuld return correct object when pass an array with equal elements', () => {
        expect(analyzeArray([1, 1, 1, 1])).to.deep.equal({ min: 1, max: 1, length: 4 });
    })
})