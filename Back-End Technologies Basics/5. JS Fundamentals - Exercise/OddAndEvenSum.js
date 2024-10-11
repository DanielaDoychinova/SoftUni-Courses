function oddAndEvenSum(num) {
    let oddSum = 0;
    let evenSum = 0;
    let nums = num.toString().split('').map(Number);
    
    nums.forEach(element => {
        if (element % 2 === 0) {
            evenSum += parseInt(element)
        } else{
            oddSum += parseInt(element)
        }
        
    });

    console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`);
    
}