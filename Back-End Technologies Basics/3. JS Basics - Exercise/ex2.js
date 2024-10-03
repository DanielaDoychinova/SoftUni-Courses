function solve(input) {
    let count = parseInt(input[0]);
    let type = input[1];
    let day = input[2];
    let discount = 0;
    let finalPrice;

    if (day == 'Friday') {
        if (type == 'Students') {
            price = count * 8.45
            if (count >= 30) {
                discount = price * 0.15;
            }

            finalPrice = price - discount;
        } else if (type == 'Business') {
            price = count * 10.90
            if (count >= 100) {
                let group = count - 10;
                finalPrice = group * 10.90;
            } else {
                finalPrice = price;
            }
        } else if (type == 'Regular') {
            price = count * 15
            if (count >= 10 && count <= 20) {
                discount = price * 0.05;
            }

            finalPrice = price - discount;
        }
    }

   else if (day == 'Saturday') {
        if (type == 'Students') {
            price = count * 9.80
            if (count >= 30) {
                discount = price * 0.15;
            }

            finalPrice = price - discount;
        } else if (type == 'Business') {
            price = count * 15.60
            if (count >= 100) {
                let group = count - 10;
                finalPrice = group * 15.60;
            } else {
                finalPrice = price;
            }
        } else if (type == 'Regular') {
            price = count * 20
            if (count >= 10 && count <= 20) {
                discount = price * 0.05;
            }

            finalPrice = price - discount;
        }
    }
   else if (day == 'Sunday') {
        if (type == 'Students') {
            price = count * 10.46
            if (count >= 30) {
                discount = price * 0.15;
            }

            finalPrice = price - discount;
        } else if (type == 'Business') {
            price = count * 16
            if (count >= 100) {
                let group = count - 10;
                finalPrice = group * 16;
            } else {
                finalPrice = price;
            }
        } else if (type == 'Regular') {
            price = count * 22.50
            if (count >= 10 && count <= 20) {
                discount = price * 0.05;
            }

            finalPrice = price - discount;
        }
    }



    console.log(`Total price: ${finalPrice.toFixed(2)}`);

}

solve(['30', 'Students', 'Friday'])