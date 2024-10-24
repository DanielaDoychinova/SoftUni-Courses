import { expect } from "chai";
import { foodDelivery } from "../functions/06_foodDelivery.js";

describe('foodDelivery object tests', () => {
    describe('getCategory function tests', () => {
        it('should retur correct message for vegan', () => {
            expect(foodDelivery.getCategory('Vegan')).to.equal('Dishes that contain no animal products.');
        })
        it('should retur correct message for vegetarian', () => {
            expect(foodDelivery.getCategory('Vegetarian')).to.equal('Dishes that contain no meat or fish.');
        })
        it('should retur correct message for gluten-free', () => {
            expect(foodDelivery.getCategory('Gluten-Free')).to.equal('Dishes that contain no gluten.');
        })
        it('should retur correct message for all', () => {
            expect(foodDelivery.getCategory('All')).to.equal('All available dishes.');
        })
        it('should throw error for invalid input', () => {
            expect(() => foodDelivery.getCategory('other')).to.throw('Invalid Category!');
            expect(() => foodDelivery.getCategory(3)).to.throw('Invalid Category!');
            expect(() => foodDelivery.getCategory(null)).to.throw('Invalid Category!');
        })
    })
    describe('addMenuItem function tests', () => {
        it('should add items with valid price and return correct array lenght', () => {
            const menuItems = [
                {name: 'salad', price: 8},
                {name: 'soup', price: 5},
                {name: 'steak', price: 20}
            ]
            expect(foodDelivery.addMenuItem(menuItems, 10)).to.equal('There are 2 available menu items matching your criteria!');

        })
        it('should throw error when pass invaliid params', () => {
            const menuItems = [
                {name: 'salad', price: 8},
                {name: 'soup', price: 5},
                {name: 'steak', price: 20}
            ]

            expect(() => foodDelivery.addMenuItem('str', 10)).to.throw('Invalid Information!');
            expect(() => foodDelivery.addMenuItem(menuItems, '10')).to.throw('Invalid Information!'); 
        })
        it('should throw error when pass price lower than 5 and manu items lower than 1', () => {
            const menuItems = [
                {name: 'salad', price: 8},
                {name: 'soup', price: 5},
                {name: 'steak', price: 20}
            ]

            expect(() => foodDelivery.addMenuItem([], 10)).to.throw('Invalid Information!');
            expect(() => foodDelivery.addMenuItem(menuItems, 4)).to.throw('Invalid Information!'); 
        })
    })
    describe('calculateOrderCost function tests', () => {
        it('should calculate correct price without discount', () => {
            const shipping = ['standard'];
            const addOns = ['sauce', 'beverage'];

            expect(foodDelivery.calculateOrderCost(shipping, addOns, false)).to.equal('You spend $7.50 for shipping and addons!')
        })
        it('should calculate correct price with discount', () => {
            const shipping = ['express'];
            const addOns = ['sauce', 'beverage'];

            expect(foodDelivery.calculateOrderCost(shipping, addOns, true)).to.equal('You spend $8.07 for shipping and addons with a 15% discount!')
        })
        it('should throw error for invalid parameters', () => {
            expect(() => foodDelivery.calculateOrderCost('str', [], true)).to.throw('Invalid Information!')
            expect(() => foodDelivery.calculateOrderCost([], 'str', true)).to.throw('Invalid Information!')
            expect(() => foodDelivery.calculateOrderCost('str', [], false)).to.throw('Invalid Information!')
        })
    })
    
})