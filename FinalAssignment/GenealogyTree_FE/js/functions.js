async function callEndpoint(endpoint, method, obj) {
    const settings = {
        method: method,
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(obj)
    };
    try {
        console.log(settings);
        const fetchResponse = await fetch(endpoint, settings);
        console.log('response.status: ', fetchResponse.status);
        console.log(fetchResponse);
        if(!fetchResponse.ok)
            return null;
        const fetchData = await fetchResponse.json();
        return fetchData;
    } catch (e) {
        return e;
    }
}

export {callEndpoint};

const parseJwt = (token) => {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch (e) {
      return null;
    }
  };

export {parseJwt};

const callPost = async (endpoint, obj) => {
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
        const fetchResponse = await fetch(endpoint, settings);
        console.log('response.status: ', fetchResponse.status);
        console.log(fetchResponse);
        return fetchResponse;
    } catch (e) {
        return e;
    }
}

export {callPost};

const login = async (obj) => {
    const settings = {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(obj)
    };
    let fetchResponse;
    try {
        console.log(settings);
        fetchResponse = await fetch('https://localhost:7008/api/User/login', settings);
        if (fetchResponse.status === 401)
        return fetchResponse.status;
    } catch (e) {
        return e;
    }
    const fetchData = await fetchResponse.json();
    console.log(fetchData);
    if (fetchData != null)
    {
        localStorage.setItem("token", fetchData.token);
        const tokenData = parseJwt(fetchData.token);
        const userId = tokenData.unique_name;
        const userPerson = await callEndpoint(`https://localhost:7008/api/user/${userId}/person`, 'GET');
        console.log(userPerson);
        if(userPerson !== null)
        {
            localStorage.setItem("person", JSON.stringify(userPerson));
            localStorage.setItem("user", JSON.stringify(userPerson));
        }
        else
        {
            localStorage.setItem("person", null);
            localStorage.setItem("user", null);
        }

        window.location = '../genealogy-tree/index.html';
    }
}

export {login} ;