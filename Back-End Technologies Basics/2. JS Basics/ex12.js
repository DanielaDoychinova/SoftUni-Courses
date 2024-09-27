function solve(n, array) {
    let result = [];

    for (let i = 0; i < n; i++) {
        result.push(array[i]);
    }

    result.reverse();

    console.log(result.join(' '));
}