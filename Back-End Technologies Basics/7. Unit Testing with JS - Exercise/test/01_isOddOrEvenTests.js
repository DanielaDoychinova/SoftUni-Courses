import { expect } from "chai";
import { isOddOrEven } from "../functions/01_isOddOrEven.js";

describe('IsOddOrEven function tests', () => {
    it('should return undefined when input is not string', () => {
        expect(isOddOrEven(123)).to.be.undefined;
        expect(isOddOrEven({})).to.be.undefined;
        expect(isOddOrEven(null)).to.be.undefined;
        expect(isOddOrEven(undefined)).to.be.undefined;
        expect(isOddOrEven([])).to.be.undefined;
    });
    it('should return even if the string lenght is even', () => {
        expect(isOddOrEven('four')).to.equal('even');
        expect(isOddOrEven('fourfive')).to.equal('even');
    });
    it('should return odd when the string is odd', () => {
        expect(isOddOrEven('one')).to.equal('odd');
        expect(isOddOrEven('oneee')).to.equal('odd');
    });

    it('should return correct value when pass different input', function(){
        expect(isOddOrEven('')).to.equal('even');
        expect(isOddOrEven('odd')).to.equal('odd');
        expect(isOddOrEven('even')).to.equal('even');
        expect(isOddOrEven(null)).to.be.undefined;
    })
})