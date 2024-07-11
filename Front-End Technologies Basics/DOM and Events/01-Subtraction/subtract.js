function subtract() {
    let numElement1 = Number(document.getElementById('firstNumber').value);
    let numElement2 = Number(document.getElementById('secondNumber').value);

    let result = numElement1 - numElement2;
    let resultElement = document.getElementById('result');

    resultElement.textContent = result
}

