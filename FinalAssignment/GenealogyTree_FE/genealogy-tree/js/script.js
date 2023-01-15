import { callEndpoint } from "../../js/functions.js";
import { parseJwt } from "../../js/functions.js";
import { callPost } from "../../js/functions.js";
const showList = document.querySelector('#showList');
const hideList = document.querySelector('#hideList');
hideList.hidden = true;
const addChild = document.querySelector('#addChild');
const createProfileButton = document.getElementById('createProfile');
createProfileButton.hidden = true;
const addParent = document.querySelector('#addParent');
const addRelativeForm = document.querySelector('#addRelativeForm');
addRelativeForm.hidden = true;
const token = localStorage.getItem("token");
const tokenData = parseJwt(token);
const userId = tokenData.unique_name;
const person = JSON.parse(localStorage.getItem("person"));
const user = JSON.parse(localStorage.getItem("user"));

if(user === null)
{
    addChild.hidden = true;
    addParent.hidden = true;
    createProfileButton.hidden = false;
    createProfileButton.addEventListener('click', () => createProfile());
}
else
{
    const header = document.getElementById('yourFamilyTree');
    header.innerHTML = `${user.name} ${user.surname}'s Family Tree`;
}

const createProfile = async () => {
    addRelativeForm.hidden = false;
    const createRelativeButton = document.getElementById("createRelativeSubmit");
    createRelativeButton.hidden = true;
    const createProfileButton = document.getElementById("createProfileSubmit");
    createProfileButton.onclick = submitCreateProfile;
}

const linkUser = async (personId) => {
    const settings = {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    };
    let fetchResponse;
    try {
        fetchResponse = await fetch(`https://localhost:7008/api/user/${userId}/linkUser?personId=${personId}`, settings);
        console.log('response.status: ', fetchResponse.status);
    } catch (e) {
        console.log(e);
    }
    if (fetchResponse.ok)
    {
        addChild.hidden = true;
    addParent.hidden = true;
    createProfileButton.hidden = false;
    await selectPerson(personId, true);
    }
}

const hideFamily = () => {
    const familyTable = document.getElementById('family-members');
    familyTable.hidden = true;
    showList.hidden = false;
    hideList.hidden = true;
}

let showFamily = async (personId) => {
    const familyTable = document.getElementById('family-members');
    familyTable.hidden = false;
    familyTable.innerHTML = '<thead><tr><th>Name</th><th>Relation</th></tr></thead>';
    const familyData = await callEndpoint(`https://localhost:7008/api/user/relatives?personId=${personId}`, 'GET');
    console.log(familyData);
    familyData.forEach(element => {
        familyTable.innerHTML += `<tbody><tr></tr>
        <th>${element.nameSurname}</th>
        <th>${element.relation}</th>
        <th class="selectPersonButton" personId=${element.personId}></th></tbody>`;
    });

    const selectPersonButtons = familyTable.getElementsByClassName('selectPersonButton');
        for (let btn of selectPersonButtons) {
            let button = document.createElement('button');
            button.type = 'button';
            button.innerHTML = 'Select';
            let personId = btn.getAttribute('personId');
            console.log(personId);
            button.addEventListener('click', () => selectPerson(personId));
            btn.appendChild(button);
        }
        showList.hidden = true;
        hideList.hidden = false;
}

const selectPerson = async (personId, setUser) => {
    const person = await callEndpoint(`https://localhost:7008/api/user/person?personId=${personId}`, 'GET');
    if(person !== null)
    {
        localStorage.setItem("person", JSON.stringify(person));
        if(setUser)
        localStorage.setItem("user", JSON.stringify(person));
    }
    else
    localStorage.setItem("person", null);

    window.location.reload();
}

if(person !== null)
{
    const personTable = document.getElementById('person-info');
    personTable.innerHTML += '<thead><tr><th>Name</th><th>Surname</th><th>Date of birth</th><th>Birth place</th></tr></thead>';
    personTable.innerHTML += `<tbody>
    <th>${person.name}</th>
    <th>${person.surname}</th>
    <th>${person.dateOfBirth}</th>
    <th>${person.birthPlace}</th></tbody>`;
}

let currentRelation = '';

let showAddRelative = (relation) => {
    const header = document.getElementById('header');
    header.innerHTML = `Add ${relation}`;
    addRelativeForm.hidden = false;
    const createRelativeButton = document.getElementById("createRelativeSubmit");
    createRelativeButton.onclick = submitEvent;
    const createProfileButton = document.getElementById("createProfileSubmit");
    createProfileButton.hidden = true;
    currentRelation = relation;
}

let addRelative = async () => {
    let data = new FormData(addRelativeForm);
    const personObj = {};
    data.forEach((value, key) => (personObj[key] = value));
    const createResponse = await createNewPerson(personObj);
    if(createResponse.status === 201)
    {
        await createRelation(createResponse.data.personId);
        addRelativeForm.hidden = true;
    }
    if(createResponse.status === 400)
    {
        const message = document.getElementById('message');
        addRelativeForm.hidden = true;
        message.innerHTML = 'Person already exists in the database. Do you want to add them?'
        const existingPeopleTable = document.getElementById('existing-people');
        existingPeopleTable.innerHTML += '<thead><th>Name</th><th>Surname</th><th>Date of birth</th>' +
        '<th>Birth place</th></thead>';
        const foundPeople = await callEndpoint(`https://localhost:7008/api/user/findPeople?
        Name=${personObj.name}&Surname=${personObj.surname}&DateOfBirth=${personObj.dateOfBirth}&
        BirthPlace=${personObj.birthPlace}`, 'GET')
        
        foundPeople.forEach(element => {
            existingPeopleTable.innerHTML += `<tbody>
            <th>${element.name}</th>
            <th>${element.surname}</th>
            <th>${element.dateOfBirth}</th>
            <th>${element.birthPlace}</th>
            <th class="addRelationButton" personId=${element.personId}></th></tbody>`;
        });
    }
}

const createAddRelationButton = () => {
    const existingPeopleTable = document.getElementById('existing-people');
    const addRelationButtons = existingPeopleTable.getElementsByClassName('addRelationButton');
    for (let btn of addRelationButtons) {
        let button = document.createElement('button');
        button.type = 'button';
        button.innerHTML = 'Add ' + currentRelation;
        let personId = btn.getAttribute('personId');
        button.addEventListener('click', () => createRelation(personId));
        btn.appendChild(button);
    }
}

const createAddProfileButton = () => {
    const existingPeopleTable = document.getElementById('existing-people');
    const addRelationButtons = existingPeopleTable.getElementsByClassName('addRelationButton');
    for (let btn of addRelationButtons) {
        let button = document.createElement('button');
        button.type = 'button';
        button.innerHTML = 'Select';
        let personId = btn.getAttribute('personId');
        button.addEventListener('click', () => linkUser(personId));
        btn.appendChild(button);
    }
}

const createNewPerson = async (personObj) => {
    const response = await callPost(`https://localhost:7008/api/user/${userId}/createPerson`, personObj);
    console.log(response);
    const fetchData = await response.json();
    console.log(fetchData);
    return {
        "status": response.status,
        "data": fetchData
    }
}

const createRelation = async (personId) => {
    let parentId = 0;
    let childId = 0;
    if(currentRelation === 'child')
    {
        parentId = person.personId;
        childId = personId;
    }
    else
    {
        parentId = personId;
        childId = person.personId;
    }
    const relationObj = {
        'parentId': parentId,
        'childId': childId
    }
    console.log(relationObj);
    const relationResponse = await callEndpoint(`https://localhost:7008/api/user/${userId}/createRelation`, 'POST', relationObj);
    console.log(relationResponse);
    if (relationResponse !== null)
    await showFamily(person.personId);
}

let submitEvent = async (e) => {
    e.preventDefault();
    await addRelative();
    createAddRelationButton();
}

let submitCreateProfile = async (e) => {
    e.preventDefault();
    await addRelative();
    createAddProfileButton();
}

showList.addEventListener('click', (e) => {
    e.preventDefault();
    showFamily(person.personId);
})

hideList.addEventListener('click', (e) => {
    e.preventDefault();
    hideFamily();
})

addChild.addEventListener('click', (e) => {
    e.preventDefault();
    showAddRelative('child');
})

addParent.addEventListener('click', (e) => {
    e.preventDefault();
    showAddRelative('parent');
})

const logOutButton = document.getElementById("log-out-button");
logOutButton.onclick = () => {
    localStorage.removeItem("user");
    localStorage.removeItem("person");
    localStorage.removeItem("token");
    window.location = '../index.html';
}