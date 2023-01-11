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
    const settings = {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(obj)
    };
    try {
        console.log(settings);
        const fetchResponse = await fetch('https://localhost:7008/api/User/login', settings);
        console.log('response.status: ', fetchResponse.status);
        console.log(fetchResponse);
        const fetchData = await fetchResponse.json();
        console.log(fetchData);
        const token = fetchData.token;
        localStorage.setItem("token", token);
        console.log(token);
        return token;
    } catch (e) {
        return e;
    }

}

submitLogin.addEventListener('click', (e) => {
    e.preventDefault();
    sendData();
})