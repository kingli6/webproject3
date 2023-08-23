import { useState } from 'react';

function AddUser() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [address, setAddress] = useState('');
  const [userRole, setUserRole] = useState('');

  const [isSuccess, setIsSuccess] = useState(false);

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };
  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };
  const handleFirstNameChange = (e) => {
    setFirstName(e.target.value);
  };
  const handleLastNameChange = (e) => {
    setLastName(e.target.value);
  };
  const handlePhoneNumberChange = (e) => {
    setPhoneNumber(e.target.value);
  };
  const handleAddressChange = (e) => {
    setAddress(e.target.value);
  };
  const handleUserRoleChange = (e) => {
    setUserRole(e.target.value);
  };

  const handleSaveUser = async (e) => {
    e.preventDefault();
    const newUser = {
      email,
      password,
      firstName,
      lastName,
      phoneNumber,
      address,
      userRole,
    };

    const savedUser = await saveUser(newUser);
    if (savedUser) {
      setIsSuccess(true);
      clearForm(); // Clear the form fields
    } else {
      setIsSuccess(false);
    }
  };
  const clearForm = () => {
    setEmail('');
    setPassword('');
    setFirstName('');
    setLastName('');
    setPhoneNumber('');
    setAddress('');
    setUserRole('');
  };

  const saveUser = async (user) => {
    const url = `${process.env.REACT_APP_BASEURL}/auth/createUser`;
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(user),
    });

    if (response.status >= 200 && response.status <= 299) {
      console.log('User is saved');
    } else {
      console.log('Something went wrong while saving user');
    }
  };

  //xx

  return (
    <>
      {isSuccess && <h3>User added successfully!</h3>}
      <h1 className="page-title">Add User</h1>
      <section className="form-container">
        <h4>Create New User</h4>
        <section className="form-wrapper">
          <form className="form" onSubmit={handleSaveUser}>
            <div className="form-control">
              <label htmlFor="email">Email</label>
              <input
                onChange={handleEmailChange}
                value={email}
                type="email"
                id="email"
                name="email"
              />
            </div>
            <div className="form-control">
              <label htmlFor="password">Password</label>
              <input
                onChange={handlePasswordChange}
                type="text"
                id="password"
                name="password"
              />
            </div>
            <div className="form-control">
              <label htmlFor="firstName">First Name</label>
              <input
                onChange={handleFirstNameChange}
                type="text"
                id="firstName"
                name="firstName"
              />
            </div>
            <div className="form-control">
              <label htmlFor="lastName">Last Name</label>
              <input
                onChange={handleLastNameChange}
                type="text"
                id="lastName"
                name="lastName"
              />
            </div>
            <div className="form-control">
              <label htmlFor="phoneNumber">Phone Number</label>
              <input
                onChange={handlePhoneNumberChange}
                type="tel"
                id="phoneNumber"
                name="phoneNumber"
              />
            </div>
            <div className="form-control">
              <label htmlFor="address">Address</label>
              <input
                onChange={handleAddressChange}
                type="text"
                id="address"
                name="address"
              />
            </div>
            <div className="form-control">
              <label htmlFor="userRole">User Role</label>
              <select
                onChange={handleUserRoleChange}
                value={userRole}
                id="userRole"
                name="userRole"
              >
                <option value="">Choose a role</option>
                <option value="User">User</option>
                <option value="Administrator">Administrator</option>
                <option value="Teacher">Teacher</option>
              </select>
            </div>
            <button type="submit" className="btn">
              Save
            </button>
          </form>
        </section>
      </section>
    </>
  );
}
export default AddUser;
