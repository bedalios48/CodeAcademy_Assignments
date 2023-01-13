import { callEndpoint } from "../../js/functions.js";
import { parseJwt } from "../../js/functions.js";
const showList = document.querySelector('#showList');

const userPerson = async () => {
    const token = localStorage.getItem('token');
    const tokenData = parseJwt(token);
    const userId = tokenData.unique_name;
    return userId;
}

let showFamily = async (userId) => {
    const familyTable = document.getElementById('family-members');
    familyTable.innerHTML += '<thead><tr><th>Name</th><th>Relation</th></tr></thead>';
    const familyData = await callEndpoint(`https://localhost:7008/api/user/${userId}/relatives`, 'GET');
    console.log(familyData);
    familyData.forEach(element => {
        familyTable.innerHTML += `<tbody><tr></tr>
        <th>${element.nameSurname}</th>
        <th>${element.relation}</th></tbody>`;
    });
}

const userId = await userPerson();
const userData = await callEndpoint(`https://localhost:7008/api/user/${userId}/person`, 'GET');
console.log(userData);

const personTable = document.getElementById('person-info');
personTable.innerHTML += '<thead><tr><th>Name</th><th>Surname</th><th>Date of birth</th><th>Birth place</th></tr></thead>';
personTable.innerHTML += `<tbody><tr></tr>
<th>${userData.name}</th>
<th>${userData.surname}</th>
<th>${userData.dateOfBirth}</th>
<th>${userData.birthPlace}</th></tbody>`;

showList.addEventListener('click', (e) => {
    e.preventDefault();
    showFamily(userId);
})