function StoreProvision(currentStock, orederedProducts) {
    let store = {};
    
    for (let i = 0; i < currentStock.length; i += 2) {
        
        let productName = currentStock[i];
        let productQuantity = Number(currentStock[i + 1]);
        
        store[productName] = productQuantity
    }
    
    for (let i = 0; i < orederedProducts.length; i += 2) {
        let productName = orederedProducts[i];
        let productQuantity = Number(orederedProducts[i + 1]);
        
        if (store.hasOwnProperty(productName)) {
            store[productName] += productQuantity;
            
            
        } else {
            store[productName] = productQuantity
        }
        
    }

    for (let product in store) {
        console.log(`${product} -> ${store[product]}`);
        
    }
}