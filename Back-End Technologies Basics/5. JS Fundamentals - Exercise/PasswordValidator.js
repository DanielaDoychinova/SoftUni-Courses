function passValidator(pass) {
    let isValid = true;
    if (pass.length < 6 || pass.length > 10) {
        console.log('Password must be between 6 and 10 characters');
     isValid = false;   
    }

    if (!/^[A-Za-z0-9]+$/.test(pass)) {
        console.log('Password must consist only of letters and digits');
        isValid = false;
    }

    let digitsCount = 0;
    for (let char of pass) {
        if (char >= '0' && char <= '9') {
            digitsCount ++
        }
    }

    if (digitsCount < 2 ) {
        console.log('Password must have at least 2 digits');
        isValid = false;
    }
    if (isValid){
        console.log('Password is valid');
        
    } 
}