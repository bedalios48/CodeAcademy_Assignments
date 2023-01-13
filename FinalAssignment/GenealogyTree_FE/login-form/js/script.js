import { callEndpoint } from "../../js/functions.js";
const loginForm = document.querySelector('#login');
const submitLogin = document.querySelector('#submit-login');

let sendData = async () => {
    const errorMessages = document.querySelectorAll(".error-message");
    for (let m of errorMessages) {
        m.parentElement.removeChild(m);
    }
    message.innerHTML = '';
    let data = new FormData(loginForm);
    const obj = {};
    data.forEach((value, key) => (obj[key] = value));

    if (obj.username === "" || obj.password === "") {
        if (obj.username === "") {
            const usernameLabel = document.getElementById('username-label');
            usernameLabel.innerHTML += '<span class="error-message">\xa0\xa0\xa0\xa0\xa0Username mandatory!</span>';
        }
        if (obj.password === "") {
            const passwordLabel = document.getElementById('password-label');
            passwordLabel.innerHTML += '<span class="error-message">\xa0\xa0\xa0\xa0\xa0Password mandatory!</span>';
        }
        return;
    }

    const fetchData = await callEndpoint('https://localhost:7008/api/User/login', 'POST', obj);
    console.log(fetchData);
    if (fetchData != null)
    {
        localStorage.setItem("token", fetchData.token);
        window.location = '../genealogy-tree/index.html';
    }
}

submitLogin.addEventListener('click', (e) => {
    e.preventDefault();
    sendData();
})