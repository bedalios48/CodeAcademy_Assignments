import { callEndpoint } from "../../js/functions.js";
const showList = document.querySelector('#showList');

let showFamily = async (personId) => {
    const familyTable = document.getElementById('family-members');
    familyTable.innerHTML += '<thead><tr><th>Name</th><th>Relation</th></tr></thead>';
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
            button.addEventListener('click', () => selectPerson(personId));
            btn.appendChild(button);
        }
}

const selectPerson = async (personId) => {
    const person = await callEndpoint(`https://localhost:7008/api/user/person?personId=${personId}`, 'GET');
    if(person !== null)
    localStorage.setItem("person", JSON.stringify(person));
    else
    localStorage.setItem("person", null);

    window.location.reload();
}

const person = JSON.parse(localStorage.getItem("person"));

const personTable = document.getElementById('person-info');
personTable.innerHTML += '<thead><tr><th>Name</th><th>Surname</th><th>Date of birth</th><th>Birth place</th></tr></thead>';
personTable.innerHTML += `<tbody><tr></tr>
<th>${person.name}</th>
<th>${person.surname}</th>
<th>${person.dateOfBirth}</th>
<th>${person.birthPlace}</th></tbody>`;

showList.addEventListener('click', (e) => {
    e.preventDefault();
    showFamily(person.personId);
})