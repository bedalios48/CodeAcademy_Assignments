const registration = document.querySelector('#registration');
const register = document.querySelector('#register');
const message = document.getElementById('message');

checkUser = async () => {
    const errorMessages = document.querySelectorAll(".error-message");
    for (m of errorMessages) {
        m.parentElement.removeChild(m);
    }
    message.innerHTML = '';
    let data = new FormData(registration);
    const obj = {};
    data.forEach((value, key) => (obj[key] = value));
    const settings = {
        method: 'GET',
        headers: {
            Accept: 'application/json'
        }
    };
    try {
        const fetchResponse = await fetch('https://testapi.io/api/bedalios/resource/user', settings);
        const fetchData = await fetchResponse.json();
        let user;
        fetchData.data.forEach(d => {
            if (d.name === obj.name && d.surname === obj.surname) {
                user = d;
            }
        });
        if (!fetchResponse.ok) {
            message.innerHTML = `Connection unsuccessful. Check console log`;
            return;
        }
        if (user !== undefined) {
            message.innerHTML = `User ${obj.name} ${obj.surname} already exists.
             <a class="sign-in-link">Sign in</a> or use another credentials`;
            const messageLink = message.getElementsByClassName("sign-in-link")[0];
            addLoginLink(messageLink);
            return;
        }
        sendData(obj);
    } catch (e) {
        console.log(e);
        message.innerHTML = `Connection unsuccessful. Check console log`;
    }

}

sendData = async (obj) => {
    const settings = {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(obj)
    };
    try {
        const fetchResponse = await fetch('https://testapi.io/api/bedalios/resource/user', settings);
        const fetchData = await fetchResponse.json();
        if (fetchResponse.ok) {
            localStorage.setItem("userInfo", JSON.stringify(fetchData));
            window.location = '../to-do-app/index.html';
        }
        if (fetchData.errors.name !== undefined) {
            const nameLabel = document.getElementById('name-label');
            nameLabel.innerHTML += `<span class="error-message">\xa0\xa0\xa0\xa0\xa0${fetchData.errors.name}</span>`;
        }
        if (fetchData.errors.surname !== undefined) {
            const surnameLabel = document.getElementById('surname-label');
            surnameLabel.innerHTML += `<span class="error-message">\xa0\xa0\xa0\xa0\xa0${fetchData.errors.surname}</span>`;
        }
        if (fetchData.errors.email !== undefined) {
            const emailLabel = document.getElementById('email-label');
            emailLabel.innerHTML += `<span class="error-message">\xa0\xa0\xa0\xa0\xa0${fetchData.errors.email}</span>`;
        }
    } catch (e) {
        console.log(e);
        message.innerHTML = `Connection unsuccessful. Check console log`;
    }

}

register.addEventListener('click', (e) => {
    e.preventDefault();
    checkUser();
})

const addLoginLink = (element) => element.onclick = () => window.location = '../login-form/index.html';

const signInLinks = document.getElementsByClassName("sign-in-link");
for (link of signInLinks) addLoginLink(link);
