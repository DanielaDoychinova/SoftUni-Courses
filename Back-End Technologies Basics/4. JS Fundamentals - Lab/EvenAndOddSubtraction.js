function subtraction(arr) {
    let evenSum = 0;
    let oddSum = 0;
    let difference = 0;


    for (let i = 0; i < arr.length; i++) {
        arr[i] = Number(arr[i]);

        if (arr[i] % 2 == 0) {
            evenSum += arr[i]
        } else if (arr[i] % 2 !== 0) {
            oddSum += arr[i]
        }
        
    }

    difference = evenSum - oddSum;

    console.log(difference);
    


}