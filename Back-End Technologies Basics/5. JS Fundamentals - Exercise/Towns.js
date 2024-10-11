function towns(array) {
    array.forEach(townInfo => {
        let [town, latitude, longitude] = townInfo.split(' | ');

        latitude = Number(latitude).toFixed(2);
        longitude = Number(longitude).toFixed(2);

        let townObj = {
            town: town,
            latitude: latitude,
            longitude: longitude
        }

        console.log(townObj);
        
    });
}