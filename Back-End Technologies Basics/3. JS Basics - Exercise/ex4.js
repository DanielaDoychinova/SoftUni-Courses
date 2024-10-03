function solve(input) {
    let start = parseInt(input[0]);
    let end = parseInt(input[1]);
    let numbers = "";
    let sum = 0;

    for (let i = start; i <= end; i++) {
        numbers += i + " ";  
        sum += i;           
    }

    console.log(numbers.trim());
    console.log(`Sum: ${sum}`);
}


