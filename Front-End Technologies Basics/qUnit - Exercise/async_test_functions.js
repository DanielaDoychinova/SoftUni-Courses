async function fetchData(url){
    let result = await fetch(url)
    .then(response => {
        if(response.ok){
            return response.json();
        }
    })
    .catch(error => `${error.message}`)
return result;

}

async function fake_delay(miliseconds) {
    return new Promise(resolve => setTimeout(resolve, miliseconds));
}

module.exports = {
    fetchData,
    fake_delay
}