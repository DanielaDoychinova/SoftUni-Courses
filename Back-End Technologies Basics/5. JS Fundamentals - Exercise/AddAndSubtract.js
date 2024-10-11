function addAndSubtract(n1, n2, n3) {
    function sum(n1, n2) {
        return n1 + n2
    } 
    function subtract(resultSum, n3) {
        return resultSum - n3
    }

    let resultSum = sum(n1, n2);
    let resultSubstract = subtract(resultSum, n3)

    console.log(resultSubstract);
    
}