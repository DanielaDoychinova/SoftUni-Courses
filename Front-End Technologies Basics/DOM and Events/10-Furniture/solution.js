function solve() {
  const [input, output] = document.getElementsByTagName('textarea');
  const [generateButton, buyButton] = document.getElementsByTagName('button');
  const tableBody = document.getElementsByTagName('tbody')[0];
  
  generateButton.addEventListener("click", generateRow);
  buyButton.addEventListener("click", buyItems);
  
  function generateRow(){
    let items = JSON.parse(input.value);
    
    for (let i = 0; i < items.length; i++) {
      let tableRow = document.createElement('tr');
      
      //add tableData for the image column
      
      let imageTableData = document.createElement('td');
      let image = document.createElement('img');
      image.src = items[i].img;
      imageTableData.appendChild(image);
      tableRow.appendChild(imageTableData);
      
      //add tableData for the name column
      
      let nameTableData = document.createElement('td');
      let name = document.createElement('p');
      name.textContent = items[i].name;
      nameTableData.appendChild(name);
      tableRow.appendChild(nameTableData);
      
      //add tableData for the price column
      
      let priceTableData = document.createElement('td');
      let price = document.createElement('p');
      price.textContent = items[i].price;
      priceTableData.appendChild(price);
      tableRow.appendChild(priceTableData);
      
      //add tableData for the decFactor column
      
      let decFactorTableData = document.createElement('td');
      let decFactor = document.createElement('p');
      decFactor.textContent = items[i].decFactor;
      decFactorTableData.appendChild(decFactor);
      tableRow.appendChild(decFactorTableData);
      
      //add tableData for the mark column
      
      let markTableData = document.createElement('td');
      let mark = document.createElement('input');
      mark.type = 'checkbox';
      markTableData.appendChild(mark);
      tableRow.appendChild(markTableData);
      
      tableBody.appendChild(tableRow);
    }
  }
  
  function buyItems() {
    let furniture = [];
    let price = 0;
    let averageDecFactor = 0;
    let checkItemsCount = 0;
    let tableRows = document.getElementsByTagName('tr');

    for (let i = 1; i < tableRows.length; i++) {
      let isMarkChecked = tableRows[i].lastChild.lastChild;

      if(isMarkChecked) {
        furniture.push(tableRows[i].children[1].children[0].textContent);
        price += Number(tableRows[i].children[2].children[0].textContent);
        averageDecFactor += Number(tableRows[i].children[3].firstChild.textContent);
        checkItemsCount += 1;
      }
      
    }

    const result =
     `Bought furniture: ${furniture.join(', ')}
      Total price = ${price}
      Average decoration factor = ${averageDecFactor/checkItemsCount}`


    output.textContent = result;
  }
}