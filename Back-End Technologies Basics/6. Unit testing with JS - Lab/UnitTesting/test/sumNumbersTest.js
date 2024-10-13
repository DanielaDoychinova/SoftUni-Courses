import { expect } from "chai";
import { sum } from "../sumNumbers.js";
import { describe, it } from "mocha";

describe('Sum function tests', function () {
    it('should return the sum of an array of numbers',() => {
        //arrange
        let testData = [1, 2, 3]
        let result;

        //act
        result = sum(testData);

        //assert
        expect(result).to.equal(6);
    });

    it('should return the sum of an array as string',() => {
        //arrange
        let testData = ['1', '2', '3']
        let result;

        //act
        result = sum(testData);

        //assert
        expect(result).to.equal(6);
    });

    it('should return zero when pass array with zero elements',() => {
        //arrange
        let testData = []
        let result;

        //act
        result = sum(testData);

        //assert
        expect(result).to.equal(0);
    });

    it('should return correct sum when pass negative numbers',() => {
        //arrange
        let testData = [-1, -2, -3]
        let result;

        //act
        result = sum(testData);

        //assert
        expect(result).to.equal(-6);
    });

    it('should return correct sum when passed mixed input',() => {
        //arrange
        let testData = [1, '2', -3]
        let result;

        //act
        result = sum(testData);

        //assert
        expect(result).to.equal(0);
    });

    it('should return undefined when passed chars as input',() => {
        //arrange
        let testData = ['a', 'b', 'c']
        let result;

        //act
        result = sum(testData);

        //assert
        expect(result).to.be.NaN;
    });
});