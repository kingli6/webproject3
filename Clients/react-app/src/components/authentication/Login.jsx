import { useState } from 'react';
import { useNavigate } from 'react-router-dom'; //220519_09.. 1:53:00

function Login({ setUserRole }) {
  const navigate = useNavigate();
  const [useUserName, setUserName] = useState('');
  const [usePassword, setPassword] = useState('');
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  const onHandleUserNameTextChanged = (e) => {
    console.log(e.target.name);
    setUserName(e.target.value);
  };
  const onHandlePasswordTextChange = (e) => {
    console.log(e.target.name);
    setPassword(e.target.value);
  };
  const handleLogin = async (e) => {
    e.preventDefault();

    const url = `${process.env.REACT_APP_BASEURL}/auth/login`;
    const user = {
      userName: useUserName,
      password: usePassword,
    };
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(user),
    });

    // console.log(response);
    if (response.status >= 200 && response.status <= 299) {
      const result = await response.json();
      //Capture information about the user: Wether admin or not.
      const isAdmin = result.roles.includes('Administrator');

      localStorage.setItem('token', JSON.stringify(result.token)); //220519_09   2:16:00
      setIsAuthenticated(true);

      if (isAdmin) {
        setUserRole('Administrator');
        navigate('/adminDashboard');
      } else {
        setUserRole('User');
        navigate('/userDashboard');
      }
    } else {
      console.log("We couldn't log you in...");
    }
  };
  const handleLogout = () => {
    // Clear token from local storage and reset isAuthenticated
    localStorage.removeItem('token');
    setIsAuthenticated(false);
    // Perform any additional logout logic as needed
    navigate('/login'); // Navigate to login page after logout
  };
  return (
    <>
      <h1 className="page-title">Log In</h1>
      <section className="form-container">
        <h4>Log in to your Account</h4>
        <section className="form-wrapper">
          <form className="form" onSubmit={handleLogin}>
            <div className="form-control">
              <label htmlFor="userName">Username/E-mail</label>
              <input
                onChange={onHandleUserNameTextChanged}
                value={useUserName}
                type="text"
                id="userName"
                name="userName"
              />
            </div>
            <div className="form-control">
              <label htmlFor="password">Password</label>
              <input
                onChange={onHandlePasswordTextChange}
                value={usePassword}
                type="password"
                id="password"
                name="password"
              />
            </div>

            <button
              type="button"
              className="btn"
              onClick={isAuthenticated ? handleLogout : handleLogin}
            >
              {isAuthenticated ? 'Log Out' : 'Log In'}
            </button>
          </form>
        </section>
      </section>
    </>
  );
}
export default Login;
