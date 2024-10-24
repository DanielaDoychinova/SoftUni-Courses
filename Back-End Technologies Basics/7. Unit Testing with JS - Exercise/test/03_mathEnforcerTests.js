import { expect } from "chai";
import { mathEnforcer } from "../functions/03_mathEnforcer.js";

describe('MathEnforcer function tests', () => {
    describe('addFive', () => {
        it('should return undefined when input is not number', () => {
            expect(mathEnforcer.addFive('5')).to.be.undefined;
            expect(mathEnforcer.addFive([])).to.be.undefined;
            expect(mathEnforcer.addFive({})).to.be.undefined;
            expect(mathEnforcer.addFive(null)).to.be.undefined;
            expect(mathEnforcer.addFive(undefined)).to.be.undefined;
        });
        it('should return input + 5 when pass number', () => {
            expect(mathEnforcer.addFive(5)).to.equal(10);
            expect(mathEnforcer.addFive(-5)).to.equal(0);
            expect(mathEnforcer.addFive(-10)).to.equal(-5);
            expect(mathEnforcer.addFive(-10.5)).to.equal(-5.5);
            expect(mathEnforcer.addFive(10.5)).to.equal(15.5);
        });
    });
    describe('subtractTen', () => {
        it('should return undefined when input is not number', () => {
            expect(mathEnforcer.subtractTen('5')).to.be.undefined;
            expect(mathEnforcer.subtractTen([])).to.be.undefined;
            expect(mathEnforcer.subtractTen({})).to.be.undefined;
            expect(mathEnforcer.subtractTen(null)).to.be.undefined;
            expect(mathEnforcer.subtractTen(undefined)).to.be.undefined;
        });
        it('should return input - 10 when pass number', () => {
            expect(mathEnforcer.subtractTen(15)).to.equal(5);
            expect(mathEnforcer.subtractTen(10)).to.equal(0);
            expect(mathEnforcer.subtractTen(-10)).to.equal(-20);
            expect(mathEnforcer.subtractTen(-10.6)).to.equal(-20.6);
            expect(mathEnforcer.subtractTen(30.6)).to.equal(20.6);
            expect(mathEnforcer.subtractTen(10.6)).to.be.closeTo(0.6, 0.1);
        });
    });
    describe('sum', () => {
        it('should return undefined when input is not number', () => {
            expect(mathEnforcer.sum('5', 0)).to.be.undefined;
            expect(mathEnforcer.sum([], 0)).to.be.undefined;
            expect(mathEnforcer.sum({}, 0)).to.be.undefined;
            expect(mathEnforcer.sum(null, 0)).to.be.undefined;
            expect(mathEnforcer.sum(undefined, 0)).to.be.undefined;
            expect(mathEnforcer.sum('5', '0')).to.be.undefined;
            expect(mathEnforcer.sum(5, '0')).to.be.undefined;
            expect(mathEnforcer.sum(5, [])).to.be.undefined;
            expect(mathEnforcer.sum(5, {})).to.be.undefined;
            expect(mathEnforcer.sum(5, null)).to.be.undefined;
            expect(mathEnforcer.sum(5, undefined)).to.be.undefined;
        });
        it('should return sum of inputs when pass number', () => {
            expect(mathEnforcer.sum(15, 5)).to.equal(20);
            expect(mathEnforcer.sum(10, -10)).to.equal(0);
            expect(mathEnforcer.sum(-10, -5)).to.equal(-15);
            expect(mathEnforcer.sum(10, -5)).to.equal(5);
            expect(mathEnforcer.sum(10.7, -5.3)).to.be.closeTo(5.4, 0.1);
            expect(mathEnforcer.sum(10.7, 5.3)).to.equal(16);
        });
    });
})