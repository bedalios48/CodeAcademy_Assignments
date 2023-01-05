const toDoForm = document.querySelector('#add-to-do-form');
toDoForm.hidden = true;
const toDoTable = document.getElementById('to-do-list');
toDoTable.hidden = true;
const hideListButton = document.getElementById('hideList');
hideListButton.hidden = true;
const message = document.getElementById('list-message');

const nameSurname = document.getElementById('nameSurname');
let currentUser = JSON.parse(localStorage.userInfo);
nameSurname.innerHTML = `${currentUser.name} ${currentUser.surname}`;

let addToDoButton = document.getElementById("add-to-do-button");
const loadForm = () => {
    toDoForm.hidden = false;
    addToDoButton.hidden = true;
}

const cancelSubmitButton = document.getElementById("cancel-button");
const unloadForm = () => {
    toDoForm.hidden = true;
    addToDoButton.hidden = false;
}
cancelSubmitButton.onclick = unloadForm;

sendData = async () => {
    const errorMessages = document.querySelectorAll(".error-message");
    for (m of errorMessages) {
        m.parentElement.removeChild(m);
    }
    let data = new FormData(toDoForm);
    const obj = {};
    data.forEach((value, key) => (obj[key] = value));
    obj['userId'] = currentUser.id;
    const settings = {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(obj)
    };
    try {
        const fetchResponse = await fetch('https://testapi.io/api/bedalios/resource/toDo', settings);
        if (fetchResponse.ok) return fetchResponse.ok;
        const fetchData = await fetchResponse.json();
        if (fetchData.errors.type !== undefined) {
            const typeLabel = document.getElementById('type-label');
            typeLabel.innerHTML += `<span class="error-message">\xa0\xa0\xa0\xa0\xa0${fetchData.errors.type}</span>`;
        }
        if (fetchData.errors.content !== undefined) {
            const contentLabel = document.getElementById('content-label');
            contentLabel.innerHTML += `<span class="error-message">\xa0\xa0\xa0\xa0\xa0${fetchData.errors.content}</span>`;
        }
        if (fetchData.errors.endDate !== undefined) {
            const endDateLabel = document.getElementById('endDate-label');
            endDateLabel.innerHTML += `<span class="error-message">\xa0\xa0\xa0\xa0\xa0${fetchData.errors.endDate}</span>`;
        }
        return fetchResponse.ok;
    } catch (e) {
        console.log(e);
        return false;
    }
}

let submitEvent = async (e) => {
    e.preventDefault();
    let result = await sendData();
    if (result) {
        toDoForm.hidden = true;
        addToDoButton.hidden = false;
    }
}

addToDoButton.onclick = loadForm;

let submitFormButton = document.getElementById("submit-button");
submitFormButton.onclick = submitEvent;

const showList = async () => {
    const settings = {
        method: 'GET',
        headers: {
            Accept: 'application/json'
        }
    };
    try {
        const fetchResponse = await fetch('https://testapi.io/api/bedalios/resource/toDo', settings);
        const fetchData = await fetchResponse.json();
        let toDoItems = '';
        fetchData.data.forEach(d => {
            if (d.userId === currentUser.id) {
                toDoItems += `<tbody><tr class="toDo" toDoId=${d.id}>
                <th class="input" name="type">${d.type}</th>
                <th class="input" name="content">${d.content}</th>
                <th class="input" name="endDate">${d.endDate}</th>
                <th class="editButton" toDoId=${d.id}></th>
                <th class="deleteButton" toDoId=${d.id}></th>
                <th class="submitButton" toDoId=${d.id}></th>
                <th class="cancelButton" toDoId=${d.id}></th>
                </tr></tbody>`;
            }
        });
        if (toDoItems.length === 0) return false;

        message.innerHTML = '';
        toDoTable.innerHTML = '<thead><tr><th>Type</th><th>Content</th><th>End Date</th></tr></thead>';
        toDoTable.innerHTML += toDoItems;
        const editButtons = toDoTable.getElementsByClassName('editButton');
        for (btn of editButtons) {
            let button = document.createElement('button');
            button.type = 'button';
            button.innerHTML = 'Edit';
            let toDoId = btn.getAttribute('toDoId');
            button.addEventListener('click', () => editButton(toDoId));
            btn.appendChild(button);
        }
        const deleteButtons = toDoTable.getElementsByClassName('deleteButton');
        for (btn of deleteButtons) {
            let button = document.createElement('button');
            button.type = 'button';
            button.innerHTML = 'Delete';
            let toDoId = btn.getAttribute('toDoId');
            button.addEventListener('click', () => deleteButton(toDoId));
            btn.appendChild(button);
        }
        const submitButtons = toDoTable.getElementsByClassName('submitButton');
        for (btn of submitButtons) {
            let button = document.createElement('button');
            button.type = 'button';
            button.innerHTML = 'Submit';
            let toDoId = btn.getAttribute('toDoId');
            button.addEventListener('click', async () => {
                const result = await submitButton(toDoId);
                if (result) {
                    completeSubmit(toDoId);
                }
            });
            btn.appendChild(button);
            btn.hidden = true;
        }
        const cancelButtons = toDoTable.getElementsByClassName('cancelButton');
        for (btn of cancelButtons) {
            let button = document.createElement('button');
            button.type = 'button';
            button.innerHTML = 'Cancel';
            let toDoId = btn.getAttribute('toDoId');
            button.addEventListener('click', () => cancelButton(toDoId));
            btn.appendChild(button);
            btn.hidden = true;
        }
        toDoTable.hidden = false;
        return fetchResponse.ok;;
    } catch (e) {
        console.log(e);
        return false;
    }
}

const showListButton = document.getElementById("showList");
showListButton.onclick = async () => {
    const itemsExist = await showList();
    if (itemsExist) {
        hideListButton.hidden = false;
        showListButton.hidden = true;
    }
    else {
        message.innerHTML = 'List is empty!'
    }
}

hideListButton.onclick = () => {
    toDoTable.hidden = true;
    hideListButton.hidden = true;
    showListButton.hidden = false;
}

const logOutButton = document.getElementById("log-out-button");
logOutButton.onclick = () => {
    localStorage.removeItem("userInfo");
    window.location = '../index.html';
}

const editButton = (toDoId) => {
    let toDoElement = Array.from(document.getElementsByClassName('toDo')).filter(r => r.getAttribute('toDoId') === toDoId)[0];
    toDoElement.getElementsByClassName("editButton")[0].hidden = true;
    toDoElement.getElementsByClassName("deleteButton")[0].hidden = true;
    toDoElement.getElementsByClassName("submitButton")[0].hidden = false;
    toDoElement.getElementsByClassName("cancelButton")[0].hidden = false;
    let inputs = toDoElement.getElementsByClassName("input");
    const sessionObj = {};
    for (input of inputs) {
        let currentVal = input.innerHTML;
        let inp = document.createElement('input');
        inp.className = 'edit-do-do';
        inp.type = 'text';
        inp.value = currentVal;
        input.innerHTML = '';
        input.appendChild(inp);
        sessionObj[input.getAttribute('name')] = currentVal;
    }
    sessionStorage.setItem(`orig_to_do_${toDoId}`, JSON.stringify(sessionObj));
};

const cancelButton = (toDoId) => {
    let toDoElement = Array.from(document.getElementsByClassName('toDo')).filter(r => r.getAttribute('toDoId') === toDoId)[0];
    const typeElement = toDoElement.querySelector('[name="type"]');
    const contentElement = toDoElement.querySelector('[name="content"]');
    const endDateElement = toDoElement.querySelector('[name="endDate"]');
    const origToDo = JSON.parse(sessionStorage.getItem(`orig_to_do_${toDoId}`));
    typeElement.innerHTML = origToDo.type;;
    contentElement.innerHTML = origToDo.content;
    endDateElement.innerHTML = origToDo.endDate;;
    toDoElement.getElementsByClassName("editButton")[0].hidden = false;
    toDoElement.getElementsByClassName("deleteButton")[0].hidden = false;
    toDoElement.getElementsByClassName("submitButton")[0].hidden = true;
    toDoElement.getElementsByClassName("cancelButton")[0].hidden = true;
};

const completeSubmit = (toDoId) => {
    let toDoElement = Array.from(document.getElementsByClassName('toDo')).filter(r => r.getAttribute('toDoId') === toDoId)[0];
    const typeElement = toDoElement.querySelector('[name="type"]');
    const contentElement = toDoElement.querySelector('[name="content"]');
    const endDateElement = toDoElement.querySelector('[name="endDate"]');
    const typeValue = typeElement.firstChild.value;
    const contentValue = contentElement.firstChild.value;
    const endDateValue = endDateElement.firstChild.value;
    typeElement.innerHTML = typeValue;
    contentElement.innerHTML = contentValue;
    endDateElement.innerHTML = endDateValue;
    toDoElement.getElementsByClassName("editButton")[0].hidden = false;
    toDoElement.getElementsByClassName("deleteButton")[0].hidden = false;
    toDoElement.getElementsByClassName("submitButton")[0].hidden = true;
    toDoElement.getElementsByClassName("cancelButton")[0].hidden = true;
}

const deleteButton = async (toDoId) => {
    let toDoElement = Array.from(document.getElementsByClassName('toDo')).filter(r => r.getAttribute('toDoId') === toDoId)[0];
    const settings = {
        method: 'DELETE',
        headers: {
            Accept: 'application/json'
        }
    };
    try {
        const fetchResponse = await fetch('https://testapi.io/api/bedalios/resource/toDo/' + toDoId, settings);
        if (fetchResponse.ok) {
            toDoElement.parentElement.removeChild(toDoElement);
        }
    } catch (e) {
        console.log(e);
    }
};
const submitButton = async (toDoId) => {
    let toDoElement = Array.from(document.getElementsByClassName('toDo')).filter(r => r.getAttribute('toDoId') === toDoId)[0];
    const typeElement = toDoElement.querySelector('[name="type"]');
    const contentElement = toDoElement.querySelector('[name="content"]');
    const endDateElement = toDoElement.querySelector('[name="endDate"]');
    let obj = {
        'type': typeElement.firstChild.value,
        'content': contentElement.firstChild.value,
        'endDate': endDateElement.firstChild.value,
        'userId': currentUser.id
    };
    const settings = {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(obj)
    };
    try {
        const fetchResponse = await fetch('https://testapi.io/api/bedalios/resource/toDo/' + toDoId, settings);
        return fetchResponse.ok;
    } catch (e) {
        console.log(e);
        return false;
    }
};
