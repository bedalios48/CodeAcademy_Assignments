import { parseJwt, callEndpoint, callPost } from "../../js/functions.js";

// properties
const token = localStorage.getItem("token");
const tokenData = parseJwt(token);
const userId =  tokenData.unique_name;

const user = JSON.parse(localStorage.getItem("user"));

const form = document.getElementById("personForm");
const marriageForm = document.getElementById("marriageForm");

const message = document.getElementById("error-message");

const table = document.getElementById("peopleTable");

const person = JSON.parse(localStorage.getItem("person"));

const addChildButton = document.getElementById("addChild");
const addParentButton = document.getElementById("addParent");
const addSpouseButton = document.getElementById("addSpouse");

const addMarriageButton = document.getElementById("addMarriageSubmit");

const showListButton = document.getElementById("showList");

const hideListButton = document.getElementById("hideList");

let isUserEmpty = false;
let addChild = false;
let addSpouse = false;


// event listeners
const submitButton = document.getElementById("createPersonSubmit");
submitButton.addEventListener("click", async (e) => {
    e.preventDefault();
    const resultPersonId = await createPerson();
    if(addSpouse && resultPersonId !== 0)
    {
        await addMarriage(resultPersonId);
    }
});

const logoutButton = document.getElementById("logOutButton");
logoutButton.addEventListener("click", () => {
    localStorage.removeItem("user");
    localStorage.removeItem("person");
    localStorage.removeItem("token");
    window.location = '../index.html';
})

addChildButton.addEventListener("click", () => {
    addChild = true;
    form.hidden = false;
    const info = document.getElementById("personFormInfo");
    info.innerHTML = "Please fill in this form to create child";
})

addParentButton.addEventListener("click", () => {
    addChild = false;
    form.hidden = false;
    const info = document.getElementById("personFormInfo");
    info.innerHTML = "Please fill in this form to create parent";
})

addSpouseButton.addEventListener("click", () => {
    addSpouse = true;
    form.hidden = false;
    const info = document.getElementById("personFormInfo");
    info.innerHTML = "Please fill in this form to add spouse";
})

showListButton.addEventListener("click", (e) => {
    e.preventDefault();
    showListButton.hidden = true;
    hideListButton.hidden = false;
    showFamilyList();
})

hideListButton.addEventListener("click", (e) => {
    e.preventDefault();
    showListButton.hidden = false;
    hideListButton.hidden = true;
    table.hidden = true;
})

// functions
const createPerson = async () => {
    const obj = createFormObject(form);
    const result = await submitForm(obj);
    if(result.status === 409)
    {
        await offerExistingPerson(result, obj);
        createAllButtons();
        return 0;
    }
    if(result.status === 400)
    {
        message.innerHTML = result.data;
        return 0;
    }
    if(result.status === 201)
    {
        if(isUserEmpty)
        {
            await linkPerson(result.data.personId);
            return;
        }
        if(addSpouse)
        {
            return result.data.personId;
        }
        await addRelation(result.data.personId);
        return result.data.personId;
    }
}

const createFormObject = (f) => {
    let data = new FormData(f);
    const obj = {};
    data.forEach((value, key) => (obj[key] = value));
    return obj;
}

const submitForm = async (obj) => {
    const response = await callPost(`https://localhost:7008/api/user/${userId}/createPerson`, obj);
    const fetchData = await response.json();
    return {
        "status": response.status,
        "data": fetchData
    }
}

const offerExistingPerson = async (result, obj) => {
    message.innerHTML = result.data;
    message.innerHTML += "<br/>Do you want to use this person?";
    form.hidden = true;
    table.hidden = false;
    let endpoint = `https://localhost:7008/api/user/findPeople?` + 
    `Name=${obj.name}&Surname=${obj.surname}`;
    if(obj.dateOfBirth !== "")
    endpoint+= `&DateOfBirth=${obj.dateOfBirth}`;
    if(obj.birthPlace !== "")
    endpoint += `&BirthPlace=${obj.birthPlace}`;
    table.innerHTML = "<thead><tr><th>Name</th><th>Surname</th><th>Date of birth</th><th>Birth place</th></tr></thead>";
    const foundPeople = await callEndpoint(endpoint, 'GET');
    for(let el of foundPeople.data)
    {
        let dateOfBirth = "";
        if(el.dateOfBirth !== null)
        dateOfBirth = el.dateOfBirth.substring(0, 10);
        table.innerHTML += `<tbody>
        <th>${el.name}</th>
        <th>${el.surname}</th>
        <th>${dateOfBirth}</th>
        <th>${el.birthPlace}</th>
        <th class="linkPersonButton" personId=${el.personId}></th>
        <th class="cancelButton"></th></tbody>`
    }
}

const createAllButtons = () => {
    if(isUserEmpty) {
        createButtons("linkPersonButton", "Use", "personId", linkPerson);
        createButtons("cancelButton", "Cancel", null, hideExistingPerson);
        return;
    }
    if(addSpouse) {
        createButtons("linkPersonButton", "Use", "personId", addMarriage);
        createButtons("cancelButton", "Cancel", null, hideExistingPerson);
        return;
    }
    createButtons("linkPersonButton", "Use", "personId", addRelation);
    createButtons("cancelButton", "Cancel", null, hideExistingPerson);
}

const createButtons = (buttonClass, buttonText, eventParameter, evt) => {
    const buttons = document.getElementsByClassName(buttonClass);
    for (let btn of buttons) {
        let button = document.createElement('button');
        button.type = 'button';
        button.innerHTML = buttonText;
        const parameter = btn.getAttribute(eventParameter);
        button.addEventListener('click', async () => await evt(parameter));
        btn.appendChild(button);
    }
}

const linkPerson = async (personId) => {
    const settings = {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`
        }
    };
    let fetchResponse;
    try {
        fetchResponse = await fetch(`https://localhost:7008/api/user/${userId}/linkUser?personId=${personId}`, settings);
    } catch (e) {
        console.log(e);
    }
    if(fetchResponse.status === 200)
    {
        isUserEmpty = false;
        await setPerson(personId, true)
    }
    const error = await fetchResponse.json();
    message.innerHTML = error;
    console.log(error);
}

const addRelation = async (personId) => {
    let parentId;
    let childId;
    if(addChild)
    {
        parentId = person.personId;
        childId = personId;
    }
    else
    {
        parentId = personId;
        childId = person.personId;
    }
    const obj = {
        'parentId': parentId,
        'childId': childId
    }
    const response = await callEndpoint(`https://localhost:7008/api/user/${userId}/createRelation`, 'POST', obj);
    if(response.status === 201)
    {
        table.hidden = true;
        form.hidden = true;
        window.location.reload();
        return;
    }
    message.innerHTML = response.data;
}

const hideExistingPerson = () => {
    form.hidden = false;
    table.hidden = true;
    message.innerHTML = "";
}

const setPerson = async (personId, setUser = false) => {
    const response = await callEndpoint(`https://localhost:7008/api/user/person?personId=${personId}`, 'GET');
    if(response.status === 200)
    {
        localStorage.setItem("person", JSON.stringify(response.data));
        if(setUser)
        localStorage.setItem("user", JSON.stringify(response.data));
        window.location.reload();
        return true;
    }
    message.innerHTML = response;
    return false;
}

const showPersoninfo = () => {
    const personTable = document.getElementById('personInfo');
    let dateOfBirth = "";
    if(person.dateOfBirth !== null)
    dateOfBirth = person.dateOfBirth.substring(0, 10);
    personTable.innerHTML += '<thead><tr><th>Name</th><th>Surname</th><th>Date of birth</th><th>Birth place</th></tr></thead>';
    personTable.innerHTML += `<tbody>
    <th>${person.name}</th>
    <th>${person.surname}</th>
    <th>${dateOfBirth}</th>
    <th>${person.birthPlace}</th></tbody>`;
}

const showFamilyList = async () => {
    await showFamily(person.personId);
    createButtons("selectPersonButton", "Select", "personId", setPerson);
}

const showFamily = async (personId) => {
    const familyData = await callEndpoint(`https://localhost:7008/api/user/relatives?personId=${personId}`, 'GET');
    if(familyData.status === 200)
    {
        table.hidden = false;
        table.innerHTML = '<thead><tr><th>Name</th><th>Relation</th></tr></thead>';
        familyData.data.forEach(element => {
            table.innerHTML += `<tbody><tr></tr>
            <th>${element.nameSurname}</th>
            <th>${element.relation}</th>
            <th class="selectPersonButton" personId=${element.personId}></th></tbody>`;
        });
    }
}

const addMarriage = async (spouseId) => {
    form.hidden = false;
    table.hidden = true;
    marriageForm.hidden = false;
    form.hidden = true;
    addMarriageButton.addEventListener('click', async () => await submitMarriage(spouseId));
}

const submitMarriage = async (spouseId) => {
    const obj = createFormObject(marriageForm);
    if (document.getElementById("areDivorced").checked) {
        obj.areDivorced = true;
      }
      else {
        obj.areDivorced = false;
      }
    obj.personId = person.personId;
    obj.spouseId = spouseId;
    const response = await callPost(`https://localhost:7008/api/user/${userId}/addMarriage`, obj);
    const fetchData = await response.json();
    if(response.status === 400)
    {
        message.innerHTML = fetchData;
    }
    if(response.status === 201)
    {
        addSpouse = false;
        window.location.reload();
    }
    console.log(fetchData);
}

// logic
if(user === null)
{
    isUserEmpty = true;
    form.hidden = false;
    const info = document.getElementById("personFormInfo");
    info.innerHTML = "Please fill in this form to create your profile";
}

if(user !== null && person !== null)
{
    await showPersoninfo();
    addChildButton.hidden = false;
    addParentButton.hidden = false;
    addSpouseButton.hidden = false;
    showListButton.hidden = false;
}