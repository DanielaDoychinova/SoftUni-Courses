import { expect } from "chai";
import { artGallery } from "../functions/05_artGallery.js";

describe('ArtGallery object tests', () => {
    describe('addArtwork function tests', () => {
        it('should throw error for invalid title or artist', () => {
            expect(() => artGallery.addArtwork(123, '30 x 40', 'Van Gogh')).to.throw('Invalid Information!');
            expect(() => artGallery.addArtwork('title', '30 x 40', 123)).to.throw('Invalid Information!');
        });
        it('should throw error for invalid dimension when pass invalid dimension', () => {
            expect(() => artGallery.addArtwork('title', '3040', 'Van Artist')).to.throw('Invalid Dimensions!');
            expect(() => artGallery.addArtwork('title', '30by40', 'Van Artist')).to.throw('Invalid Dimensions!');
            
        });
        it('should throw error for invalid artist when pass invalid artist', () => {
            expect(() => artGallery.addArtwork('title', '30 x 40', 'Leonardo')).to.throw('This artist is not allowed in the gallery!');
        });

        it('should eturn correct message when pass correct data', () => {

            
            expect(artGallery.addArtwork("title", '30 x 40', 'Picasso')).to.equal("Artwork added successfully: 'title' by Picasso with dimensions 30 x 40.");
        });
    })

    describe('calculateCost function tests', () => {
        it('should throw error when pass invalid parameters', () => {
            expect(() => artGallery.calculateCosts('100', 100, true)).to.throw('Invalid Information!');
            expect(() => artGallery.calculateCosts(100, "100", true)).to.throw('Invalid Information!');
            expect(() => artGallery.calculateCosts(100, 100, 'yes')).to.throw('Invalid Information!');
            expect(() => artGallery.calculateCosts(-100, 100, true)).to.throw('Invalid Information!');
            expect(() => artGallery.calculateCosts(100, -100, true)).to.throw('Invalid Information!');
        });
        it('should calculate total cost without discount', () => {
            expect(artGallery.calculateCosts(100, 200, false)).to.equal('Exhibition and insurance costs are 300$.'); 
        })
        it('should calculate total cost with discount', () => {
            expect(artGallery.calculateCosts(100, 200, true)).to.equal('Exhibition and insurance costs are 270$, reduced by 10% with the help of a donation from your sponsor.');
        })
    })
    describe('organizeExhibits function tests', () =>{
        it('should throw an error when pass invalid input', () => {
            expect(() => artGallery.organizeExhibits('10', 2)).to.throw('Invalid Information!');
            expect(() => artGallery.organizeExhibits(10, '2')).to.throw('Invalid Information!');
            expect(() => artGallery.organizeExhibits(-10, 2)).to.throw('Invalid Information!');
            expect(() => artGallery.organizeExhibits(10, -2)).to.throw('Invalid Information!');
        })
        it('should return correct message when artworks is less than 5', () => {
            expect(artGallery.organizeExhibits(10, 3)).to.equal('There are only 3 artworks in each display space, you can add more artworks.');
        })
        it('should return correct message when artworks is bigger than 5', () => {
            expect(artGallery.organizeExhibits(20, 4)).to.equal('You have 4 display spaces with 5 artworks in each space.');
        })
    })
})