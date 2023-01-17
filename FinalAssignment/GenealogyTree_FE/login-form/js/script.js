import { login } from "../../js/functions.js";
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

    const status = await login(obj);
    if (status === 401)
    {
        const message = document.getElementById('message');
        message.innerHTML = `Bad username or password.
        <a class="register-link">Register</a> or use another credentials`;
       const messageLink = message.getElementsByClassName("register-link")[0];
       console.log(messageLink);
       messageLink.onclick = () => window.location = '../registration-form/index.html';
    }
}

submitLogin.addEventListener('click', (e) => {
    e.preventDefault();
    sendData();
})