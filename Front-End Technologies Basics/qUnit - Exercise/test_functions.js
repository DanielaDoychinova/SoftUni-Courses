
// Sum Function
function sum(a, b) {
    return a + b;
}


// isEven Function
function isEven(num) {
    return num % 2 == 0;
}


//Factorial function
function factorial(num) {
    if (num === 0 || num === 1 || num < 0) {
        return 1;
    }
    
    return num * factorial(num - 1);
    
}

//isPolindrome function
function isPolindrome(str) {
    if (str == '') { return false }
    
    let cleanStr = str.toLowerCase().replace(/[\W_]/g, '');
    let reversedStr = cleanStr.split('').reverse().join('');
    return cleanStr === reversedStr;
}


console.log(isPolindrome("civic"));


// Fibonacci Function
function fibonacci(n) {
    if (n === 0) {
        return[];
    }
    if (n === 1) {
        return [0, 1];
    }
    let sequence = [0, 1];
    for (let i = 2; i < n; i++) {
        sequence.push(sequence[i - 1] + sequence[i - 2]);
    }
    return sequence;
}

// isPrime Function
function nthPrime(n) {
    let count = 0;
    let num = 2;
    while (count < n) {
        if (isPrime(num))
            {
            count++;
        }
        num++;
    }
    return num - 1;
}



function isPrime(num) {
    if (num <= 1) { return false; }
    if (num <= 3) { return true; }
    if (num % 2 === 0 || num % 3 === 0) { return false; }
    
    for (let i = 5; i * i <= num; i += 6) {
        if (num % i === 0 || num % (i + 2) === 0) { return false; }    
    }
    
    return true;
}


//Pascal Triangle Function
function pascalTriangle(rows) 
{
    let triangle = [];
    for (let i = 0; i < rows; i++) {
        triangle[i] = [];
        triangle[i][0] = 1;
        for (let j = 1; j < i; j++) {
            triangle[i][j] = triangle[i-1][i-1] + triangle[i-1][i];
        }  
        triangle[i][i] = 1;
    }
    
    return triangle;
}


//Perfect Prime Function
function isPerfectSquare(num) {
    return Math.sqrt(num) % 1 === 0;
}

module.exports = {
    sum,
    isEven,
    factorial,
    isPolindrome,
    fibonacci,
    nthPrime,
    pascalTriangle,
    isPerfectSquare

}




