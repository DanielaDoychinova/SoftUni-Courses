function addItem() {
    let itemText = document.getElementById("newItemText");
    let itemValue = document.getElementById("newItemValue");
    let selectItem = document.getElementById('menu');

    let optionElement = document.createElement('option');

    optionElement.textContent = itemText.value;
    optionElement.value = itemValue.value;

    selectItem.appendChild(optionElement);

    itemText = '';
    itemValue = '';
}