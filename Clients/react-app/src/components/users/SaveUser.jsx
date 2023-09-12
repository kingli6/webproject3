async function SaveUser(user) {
  const url = `${process.env.REACT_APP_BASEURL}/auth/createUser`;
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(user),
  });

  if (response.status >= 200 && response.status <= 299) {
    return true;
  } else {
    return false;
  }
}

export default SaveUser;
