window.addEventListener('load', solve);

function solve() {

    let timeElement = document.getElementById('time');
    let dateElement = document.getElementById('date');
    let placeElement = document.getElementById('place');
    let nameElement = document.getElementById('event-name');
    let emailElement = document.getElementById('email');
    let addBtn = document.getElementById('add-btn');
    let upcomingList = document.getElementById('upcoming-list');
    let checkList = document.getElementById('check-list');
    let finishedList = document.getElementById('finished-list');
    let clearBtn = document.getElementById('clear');

    addBtn.addEventListener('click', onAdd);

    function onAdd(e) {
        e.preventDefault();
        if (timeElement.value == '' ||
            dateElement.value == '' ||
            placeElement.value == '' ||
            nameElement.value == '' ||
            emailElement.value == '' 
        ) {
            return;
        }

        let liElementChecked = document.createElement('li');
        liElementChecked.setAttribute('class', 'event-content');

        let articleElementChecked = document.createElement('article');

        let timeAndDateElementChecked = document.createElement('p');
        timeAndDateElementChecked.textContent = `Begins: ${dateElement.value} at: ${timeElement.value}`;

        let placeElementChecked = document.createElement('p');
        placeElementChecked.textContent = `In: ${placeElement.value}`;

        let nameElementChecked = document.createElement('p');
        nameElementChecked.textContent = `Event: ${nameElement.vaalue}`;

        let emailElementChecked = document.createElement('p');
        emailElementChecked.textContent = `Contact: ${emailElement.value}`;

        let editBtn = document.createElement('button')
        editBtn.setAttribute('class', 'edit-btn');
        editBtn.textContent = 'Edit';

        let continueBtn = document.createElement('button')
        continueBtn.setAttribute('class', 'continue-btn');
        continueBtn.textContent = 'Continue';

        articleElementChecked.appendChild(timeAndDateElementChecked);
        articleElementChecked.appendChild(placeElementChecked);
        articleElementChecked.appendChild(nameElementChecked);
        articleElementChecked.appendChild(emailElementChecked);

        liElementChecked.appendChild(articleElementChecked);
        liElementChecked.appendChild(editBtn);
        liElementChecked.appendChild(continueBtn);

        checkList.appendChild(liElementChecked);

        let editedTimeElement = timeElement.value;
        let editedDateElement = dateElement.value;
        let editedPlaceElement = placeElement.value;
        let editedNameElement = nameElement.value;
        let editedEmailElement = emailElement.value;

        timeElement.value = '';
        dateElement.value = '';
        placeElement.value = '';
        nameElement.value = '';
        emailElement.value = '';

        addBtn.disabled = true;

        editBtn.addEventListener('click', onEdit);

        function onEdit() {
            timeElement.value = editedTimeElement;
            dateElement.value = editedDateElement;
            placeElement.value = editedPlaceElement;
            nameElement.value = editedNameElement;
            emailElement.value = editedEmailElement;

            liElementChecked.remove();
            addBtn.disabled = false;
        }
        
        continueBtn.addEventListener('click', onContinue);

        function onContinue() {
            let liElementContinue = document.createElement('li');
        liElementContinue.setAttribute('class', 'event-content');

        let articleElementContinue = document.createElement('article');
        articleElementContinue = articleElementChecked;

        let finishBtn = document.createElement('button');
        finishBtn.setAttribute('class', 'finished-btn');
        finishBtn.textContent = 'Move to Finished';

        liElementContinue.appendChild(articleElementContinue);
        liElementContinue.appendChild(finishBtn);

        upcomingList.appendChild(liElementContinue);

        liElementChecked.remove();
        addBtn.disabled = false;

        finishBtn.addEventListener('click', onFinish);

        function onFinish() {
            let liElementFinish = document.createElement('li');
            liElementFinish.setAttribute('class', 'event-content');
            
            let articleElementFinish = document.createElement('article');
            articleElementFinish = articleElementContinue;

            liElementFinish.appendChild(articleElementFinish);
            
            finishedList.appendChild(liElementFinish);

            liElementContinue.remove();

            clearBtn.addEventListener('click', onClear);

            function onClear() {
                liElementFinish.remove()
            }
        }


        }

        
    }
}


    
    
