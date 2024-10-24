import { expect } from "chai";
import { lookupChar } from "../functions/02_charLookup.js";

describe('CharLookup funktion tests', () => {
    it('should return undefined when pass invalid value',function () {
        expect(lookupChar(123,0)).to.be.undefined;
        expect(lookupChar([], 0)).to.be.undefined;
        expect(lookupChar({}, 0)).to.be.undefined;
        expect(lookupChar(null, 0)).to.be.undefined;
        expect(lookupChar('str', [])).to.be.undefined;
        expect(lookupChar('str', {})).to.be.undefined;
        expect(lookupChar('str', null)).to.be.undefined;
        expect(lookupChar('str', 'str')).to.be.undefined;
        expect(lookupChar('str', 1.5)).to.be.undefined;
    });
    it('should return incorrect index when pass incorrect index', () => {
        expect(lookupChar('string', 6)).to.equal('Incorrect index');
        expect(lookupChar('string', 7)).to.equal('Incorrect index');
        expect(lookupChar('string', -1)).to.equal('Incorrect index');
    });
    it('should return the character at the specified index', () => {
        expect(lookupChar('one', 0)).to.equal('o');
        expect(lookupChar('one', 1)).to.equal('n');
        expect(lookupChar('one', 2)).to.equal('e');
    })
})