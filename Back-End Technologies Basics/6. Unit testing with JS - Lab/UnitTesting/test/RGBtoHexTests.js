import { expect } from "chai";
import { rgbToHexColor } from "../RGBtoHex.js";
import { describe } from "mocha";

describe('rgbToHexColor function tests', function () {

    it('should return correct HEX for valid RGB', () => {
        const result = rgbToHexColor(255, 142, 144);

        expect(result).to.equal('#FF8E90')
    });

    it('should return correct HEX for lower boundary', () => {
        const result = rgbToHexColor(0, 0, 0);

        expect(result).to.equal('#000000')
    })

    it('should return correct HEX for upper', () => {
        const result = rgbToHexColor(255, 255, 255);

        expect(result).to.equal('#FFFFFF')
    })

    it('should return undefined HEX with negative number', () => {
        const result = rgbToHexColor(-11, 0, 0);

        expect(result).to.be.undefined
    })

    it('should return undefined for bigger than 255 number', () => {
        const result = rgbToHexColor(0, 256, 0);

        expect(result).to.be.undefined
    })

    it('should return undefined for strings', () => {
        const result = rgbToHexColor('0', '25.5', '0');

        expect(result).to.be.undefined
    })
})