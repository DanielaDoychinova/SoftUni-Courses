window.addEventListener("load", solve);

function solve() {
  let snowmanNameElement = document.getElementById('snowman-name');
  let snowmanHeightElement = document.getElementById('snowman-height');
  let snowmanLocationElement = document.getElementById('location');
  let snowmanCreatorElement = document.getElementById('creator-name');
  let snowmanAttributeElement = document.getElementById('special-attribute');
  let addButton = document.querySelector('.add-btn');
  let snowListElement = document.querySelector('.snow-list');
  let showSnowmanElement = document.querySelector('.snow-list');
  let main = document.getElementById('hero');
  let bodyElement = document.querySelector('.body');
  let backImg = document.getElementById('back-img');
  let snowmanPreview = document.querySelector('.snowman-preview');
  
  addButton.addEventListener('click', onAdd);
  
  function onAdd(e) {
    e.preventDefault();
    if (
      snowmanNameElement.value == '' ||
      snowmanHeightElement.value == '' ||
      snowmanLocationElement.value == '' ||
      snowmanCreatorElement.value == '' ||
      snowmanAttributeElement.value == '' 
    ) {
      return;
    }
    
    let articleElementInfo = document.createElement('article');
    let liElementInfo = document.createElement('li');
    liElementInfo.setAttribute('class', 'snowman-info');
    let btnContainer = document.createElement('div');
    btnContainer.setAttribute('class', 'btn-container');
    
    let snowmanName = document.createElement('p');
    snowmanName.textContent = `Name: ${snowmanNameElement.value}`;
    
    let snowmanHeight = document.createElement('p');
    snowmanHeight.textContent = `Height: ${snowmanHeightElement.value}`;
    
    let snowmanLocation = document.createElement('p');
    snowmanLocation.textContent = `Location: ${snowmanLocationElement.value}`;
    
    let snowmanCreator = document.createElement('p');
    snowmanCreator.textContent = `Ceator: ${snowmanCreatorElement.value}`;
    
    let snowmanAttribute = document.createElement('p');
    snowmanAttribute.textContent = `Attribute: ${snowmanAttributeElement.value}`;
    
    let editBtn = document.createElement('button');
    editBtn.setAttribute('class', 'edit-btn');
    editBtn.textContent = "Edit";
    
    let nextBtn = document.createElement('button');
    nextBtn.setAttribute('class', 'next-btn');
    nextBtn.textContent = 'Next';
    
    articleElementInfo.appendChild(snowmanName);
    articleElementInfo.appendChild(snowmanHeight);
    articleElementInfo.appendChild(snowmanLocation);
    articleElementInfo.appendChild(snowmanCreator);
    articleElementInfo.appendChild(snowmanAttribute);
    
    btnContainer.appendChild(editBtn);
    btnContainer.appendChild(nextBtn);
    
    liElementInfo.appendChild(articleElementInfo);
    liElementInfo.appendChild(btnContainer);
    
    snowmanPreview.appendChild(liElementInfo);
    
    let editedSnowmanName = snowmanNameElement.value;
    let editedHeight = snowmanHeightElement.value;
    let editedLocation = snowmanLocationElement.value;
    let editedCreator = snowmanCreatorElement.value;
    let editedAttribute = snowmanAttributeElement.value; 
    
    
    snowmanNameElement.value = "";
    snowmanHeightElement.value = "";
    snowmanLocationElement.value = "";
    snowmanCreatorElement.value = "";
    snowmanAttributeElement.value = "";
    
    addButton.disabled = true;

    editBtn.addEventListener('click', onEdit);

    function onEdit() {
      snowmanNameElement.value = editedSnowmanName;
      snowmanHeightElement.value = editedHeight;
      snowmanLocationElement.value = editedLocation;
      snowmanCreatorElement.value  = editedCreator;
      snowmanAttributeElement.value = editedAttribute;

      liElementInfo.remove();
      addButton.disabled = false;
    }

    nextBtn.addEventListener('click', onNext);

    function onNext() {
      let liElementNext = document.createElement('li');
      liElementNext.setAttribute('class', 'snowman-content');

      let articleElementNext = document.createElement('article');
      articleElementNext = articleElementInfo;

      let sendBtn = document.createElement('button');
      sendBtn.setAttribute('class', 'send-btn');
      sendBtn.textContent = 'Send';

      articleElementNext.appendChild(sendBtn);

      liElementNext.appendChild(articleElementNext);

      snowListElement.appendChild(liElementNext);

      liElementInfo.remove();

      sendBtn.addEventListener('click', onSend);

      function onSend() {
        main.remove();

        let backBtn = document.createElement('button');
        backBtn.setAttribute('class', 'back-btn');
        backBtn.textContent = 'Back';

        bodyElement.appendChild(backBtn);

        backImg.style.display = 'block';

        backBtn.addEventListener('click', onBack);

        function onBack() {
          window.location.reload();
        }
      }
    }
  }
}
