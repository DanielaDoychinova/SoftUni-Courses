function solve(input) {
    let num = parseFloat(input[0]);

    for (let i = 1; i < input.length; i++) {
        let operation = input[i];

        switch (operation) {
            case 'chop':
                num /= 2;
                break;
            case 'dice':
                num = Math.sqrt(num);
                break;
            case 'spice':
                num += 1;
                break;
            case 'bake':
                num *= 3;
                break;
            case 'fillet':
                num *= 0.80;
            default:
                break;
        }

        console.log(num);
        
        
    }
}