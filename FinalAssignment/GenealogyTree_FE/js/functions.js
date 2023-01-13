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