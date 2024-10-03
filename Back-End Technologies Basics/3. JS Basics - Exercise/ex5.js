function solve(input) {
    let num = parseInt(input);
    let result = 0;
    let times;

    for (let i = 1; i <= 10; i++){
        times = i;
        result = num * i;
        console.log(`${num} X ${times} = ${result}`);
    }
}