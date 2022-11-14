const loginForm = document.querySelector('#login');
const submitLogin = document.querySelector('#submit-login');

sendData = async () => {
    const errorMessages = document.querySelectorAll(".error-message");
    for (m of errorMessages) {
        m.parentElement.removeChild(m);
    }
    message.innerHTML = '';
    let data = new FormData(loginForm);
    const obj = {};
    data.forEach((value, key) => (obj[key] = value));
    if (obj.name === "" || obj.surname === "") {
        if (obj.name === "") {
            const nameLabel = document.getElementById('name-label');
            nameLabel.innerHTML += '<span class="error-message">\xa0\xa0\xa0\xa0\xa0Name mandatory!</span>';
        }
        if (obj.surname === "") {
            const surnameLabel = document.getElementById('surname-label');
            surnameLabel.innerHTML += '<span class="error-message">\xa0\xa0\xa0\xa0\xa0Surname mandatory!</span>';
        }
        return;
    }
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
        if (user === undefined) {
            const message = document.getElementById('message');
            message.innerHTML = 'User does not exist!';
            return;
        }
        localStorage.setItem("userInfo", JSON.stringify(user));
        window.location = '../to-do-app/index.html'
        return fetchData;
    } catch (e) {
        return e;
    }

}

submitLogin.addEventListener('click', (e) => {
    e.preventDefault();
    sendData();
})