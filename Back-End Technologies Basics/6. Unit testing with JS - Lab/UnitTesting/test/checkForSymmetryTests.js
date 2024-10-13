import { expect } from "chai";
import { isSymmetric } from "../checkForSymmetry.js";
import { describe, it } from "mocha";

describe('Tests for checkForSymmentry function', function(){
    it('should return true for symmetric array', () => {
        const result = isSymmetric([1, 2, 3, 2, 1]);

        expect(result).to.be.true;
    });

    it('should return false for non-symmetric array', () => {
        const result = isSymmetric([1, 2, 3, 5, 1]);

        expect(result).to.be.false;
    });

    it('should return true for empty array', () => {
        const result = isSymmetric([]);

        expect(result).to.be.true;
    });

    it('should return false for non-array', () => {
        const result = isSymmetric('this is not array');

        expect(result).to.be.false;
    });

    it('should return false for non number elements', () => {
        const result = isSymmetric(['1', 1]);

        expect(result).to.be.false;
    });

    it('should return true for single element array', () => {
        const result = isSymmetric([1]);

        expect(result).to.be.true;
    });
})