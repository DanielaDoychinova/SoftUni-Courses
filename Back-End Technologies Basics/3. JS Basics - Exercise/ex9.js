function solve(input) {
    let sum = 0;
    let inputString = input.toString();
    let isTrue = true;

    for (let i = 0; i < inputString.length; i++) {

        let current = parseInt(inputString[i])
        sum +=  current;
        if (i > 0 && inputString[i] !== inputString[i - 1]) {
            isTrue = false;
            
        } else{
            isTrue = true
            
        }
    }

    console.log(isTrue);
    
    console.log(sum);
    
}