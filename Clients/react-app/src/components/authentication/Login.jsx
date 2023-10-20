import { useState } from 'react';
import { useNavigate } from 'react-router-dom'; //220519_09.. 1:53:00
import { useAuth } from '../context/AuthContext';

function Login() {
  const navigate = useNavigate();
  const [useUserName, setUserName] = useState('');
  const [usePassword, setPassword] = useState('');
  const { setUserRole, setUserDetails } = useAuth();

  const onHandleUserNameTextChanged = (e) => {
    setUserName(e.target.value);
  };

  const onHandlePasswordTextChange = (e) => {
    setPassword(e.target.value);
  };

  const handleCreateAccount = () => {
    navigate('/addUser');
    alert('HEY dejen');
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

    if (response.status >= 200 && response.status <= 299) {
      const result = await response.json();
      //TODO
      console.log('User Object:: ', result);
      localStorage.setItem('token', JSON.stringify(result.token)); //220519_09   2:16:00

      //Capture information about the user: Wether admin or not.
      const isAdmin = result.roles.includes('Administrator');
      //TODO 1
      console.log('Login successful! isAdmin:', isAdmin);
      setUserRole(isAdmin ? 'Administrator' : 'User'); // Update userRole

      // Set user details in the context
      setUserDetails({
        id: result.id,
        email: result.email,
        // ...other fields you may have
      });

      if (isAdmin) {
        navigate('/adminDashboard');
      } else {
        navigate('/userDashboard');
      }
    } else {
      console.log("We couldn't log you in...");
    }
  };
  // const handleLogout = () => {
  //   // Clear token from local storage and reset isAuthenticated
  //   localStorage.removeItem('token');
  //   setIsAuthenticated(false);
  //   setUserRole(null);
  //   // Perform any additional logout logic as needed
  //   navigate('/login'); // Navigate to login page after logout
  // };
  return (
    <>
      <h1 className="page-title">Log In</h1>
      <section className="form-container">
        <h4>Log in to your Account</h4>
        <section className="form-wrapper">
          <form className="form" onSubmit={handleLogin}>
            <div className="form-control">
              <label htmlFor="userName">Username</label>
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

            <button type="submit" className="btn">
              Log In
            </button>
            <button type="button" className="btn" onClick={handleCreateAccount}>
              Create Account
            </button>
          </form>
        </section>
      </section>
    </>
  );
}
export default Login;
