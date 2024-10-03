function solve(input) {
    let fruit = input[0];
    let grams = parseInt([input[1]]);
    let pricePerKilo = parseFloat(input[2]);

    let weight = grams / 1000;

    let money = weight * pricePerKilo;

    console.log(`I need $${money.toFixed(2)} to buy ${weight.toFixed(2)} kilograms ${fruit}.`);
    
}