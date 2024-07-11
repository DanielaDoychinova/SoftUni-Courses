window.addEventListener('load', solution);

function solution() {
  let employeeElement = document.getElementById('employee');
  let categoryElement = document.getElementById('category');
  let urgencyElement = document.getElementById('urgency');
  let teamElement = document.getElementById('team');
  let descriptionElement = document.getElementById('description');
  let addBtn = document.getElementById('add-btn');
  let previewList = document.querySelector('.preview-list');
  let pendingList = document.querySelector('.pending-list');
  let resolvedList = document.querySelector('.resolved-list');
  
  addBtn.addEventListener('click', onAdd);
  
  function onAdd(e) {
    e.preventDefault();
    if(
      employeeElement.value == '' ||
      categoryElement.value == '' ||
      urgencyElement.value == '' ||
      teamElement.value == '' ||
      descriptionElement.value == ''
    ) {
      return;
    }
    
    let liElementInfo = document.createElement('li');
    liElementInfo.setAttribute('class', 'problem-content');
    let articleElementInfo = document.createElement('article');
    let articleElementBtn = document.createElement('article');
    
    let editBtn = document.createElement('button');
    editBtn.setAttribute('class', 'edit-btn');
    editBtn.textContent = 'Edit';
    let continueBtn = document.createElement('button');
    continueBtn.setAttribute('class', 'continue-btn');
    continueBtn.textContent = 'Continue';
    
    let employee = document.createElement('p');
    employee.textContent = `From: ${employeeElement.value}`;
    
    let category = document.createElement('p');
    category.textContent = `Category: ${categoryElement.value}`;
    
    let urgency = document.createElement('p');
    urgency.textContent = `Urgency: ${urgencyElement.value}`;
    
    let team = document.createElement('p');
    team.textContent = `Assigned to: ${teamElement.value}`;
    
    let description = document.createElement('p');
    description.textContent = `Description: ${descriptionElement.value}`;
    
    articleElementInfo.appendChild(employee);
    articleElementInfo.appendChild(category);
    articleElementInfo.appendChild(urgency);
    articleElementInfo.appendChild(team);
    articleElementInfo.appendChild(description);
    
    articleElementBtn.appendChild(editBtn);
    articleElementBtn.appendChild(continueBtn);
    
    liElementInfo.appendChild(articleElementInfo);
    liElementInfo.appendChild(articleElementBtn);
    
    previewList.appendChild(liElementInfo);
    
    let editedEmployee = employeeElement.value;
    let editedCategory = categoryElement.value;
    let editedUrgency = urgencyElement.value;
    let editedTeam = teamElement.value;
    let editedDescription = descriptionElement.value;
    
    employeeElement.value = '';
    categoryElement.value = '';
    urgencyElement.value = '';
    teamElement.value = '';
    descriptionElement.value = '';
    
    addBtn.disabled = true;
    
    editBtn.addEventListener('click', onEdit);
    
    function onEdit() {
      employeeElement.value = editedEmployee;
      categoryElement.value = editedCategory;
      urgencyElement.value = editedUrgency;
      teamElement.value = editedTeam;
      descriptionElement.value = editedDescription;
      
      liElementInfo.remove();
      addBtn.disabled = false;
    }
    
    continueBtn.addEventListener('click', onContinue);
    
    function onContinue() {
      let liElementContinue = document.createElement('li');
      liElementContinue.setAttribute('class', 'problem-content');
      
      let articleElementContinue = document.createElement('article');
      articleElementContinue = articleElementInfo;
      
      let articleElementResolve = document.createElement('article');
      let resolveBtn = document.createElement('button');
      resolveBtn.setAttribute('class', 'resolve-btn');
      resolveBtn.textContent = 'Resolve';
      
      articleElementResolve.appendChild(resolveBtn);
      
      liElementContinue.appendChild(articleElementContinue);
      liElementContinue.appendChild(articleElementResolve);
      
      liElementInfo.remove();
      
      pendingList.appendChild(liElementContinue);
      
      resolveBtn.addEventListener('click', onResolve);
      
      function onResolve() {
        
        let liElementResolve = document.createElement('li');
        liElementContinue.setAttribute('class', 'problem-content');
        
        let articleElementFinal = document.createElement('article');
        articleElementFinal = articleElementContinue;
        
        let articleElementFinalBtn = document.createElement('article');
        
        let clearBtn = document.createElement('button');
        clearBtn.setAttribute('class', 'clear-btn');
        clearBtn.textContent = 'Clear';
        
        articleElementFinalBtn.appendChild(clearBtn);
        
        liElementResolve.appendChild(articleElementFinal);
        liElementResolve.appendChild(articleElementFinalBtn);
        
        resolvedList.appendChild(liElementResolve);
        
        liElementContinue.remove();
        
        clearBtn.addEventListener('click', onClear);
        
        function onClear() {
          window.location.reload()
        }
      }
    }
  }
}




