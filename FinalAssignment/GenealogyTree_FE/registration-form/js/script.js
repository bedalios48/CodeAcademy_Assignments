import { callPost, login } from "../../js/functions.js";
const registration = document.querySelector('#registration');
const register = document.querySelector('#register');
const message = document.getElementById('message');

const registerUser = async () => {
    const errorMessages = document.querySelectorAll(".error-message");
    for (m of errorMessages) {
        m.parentElement.removeChild(m);
    }
    message.innerHTML = '';
    let data = new FormData(registration);
    const obj = {};
    data.forEach((value, key) => (obj[key] = value));
    obj["role"] = 'external';
    const response = await callPost('https://localhost:7008/api/User/register', obj);
    if(response.status === 400) {
        message.innerHTML = `User ${obj.userName} already exists.
        <a class="sign-in-link">Sign in</a> or use another credentials`;
       const messageLink = message.getElementsByClassName("sign-in-link")[0];
       addLoginLink(messageLink);
       return;
    }
    if(response.status !== 201) {
        const fetchData = await response.json();
        console.log(fetchData);
        if (fetchData.errors.userName !== undefined) {
            const usernameLabel = document.getElementById('username-label');
            usernameLabel.innerHTML += `<span class="error-message">\xa0\xa0\xa0\xa0\xa0${fetchData.errors.userName}</span>`;
        }
        if (fetchData.errors.password !== undefined) {
            const passwordLabel = document.getElementById('password-label');
            passwordLabel.innerHTML += `<span class="error-message">\xa0\xa0\xa0\xa0\xa0${fetchData.errors.password}</span>`;
        }
        return;
    }
    delete obj.role;
    login(obj);
};

register.addEventListener('click', (e) => {
    e.preventDefault();
    registerUser();
})

const addLoginLink = (element) => element.onclick = () => window.location = '../login-form/index.html';

const signInLinks = document.getElementsByClassName("sign-in-link");
for (link of signInLinks) addLoginLink(link);
