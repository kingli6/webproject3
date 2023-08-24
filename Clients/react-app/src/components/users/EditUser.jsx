import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

function EditUser() {
  const params = useParams();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [address, setAddress] = useState('');
  const [role, setRole] = useState('');

  useEffect(() => {
    fetchUser(params.id);
    console.log('params id ' + params.id); // this takes in the Id!!
  }, [params.id]);

  const fetchUser = async (id) => {
    const url = `${process.env.REACT_APP_BASEURL}/auth/getUserById/${id}`;
    const response = await fetch(url);
    //TODO
    console.log('response: ' + response); //ans response: [object Response]

    if (!response.ok) {
      console.log("Couldn't find user, or something went wrong...");
    }
    const user = await response.json();
    setEmail(user.email);
    setPassword(user.password);
    setFirstName(user.firstName);
    setLastName(user.lastName);
    setPhoneNumber(user.phoneNumber);
    setAddress(user.address);
    setRole(user.role);

    console.log('user stuff ' + user.email + user.firstName);
  };

  const handleSaveUser = async (e) => {
    e.preventDefault();
    const updatedUser = {
      email,
      password,
      firstName,
      lastName,
      phoneNumber,
      address,
      userRole: role,
    };

    saveUser(updatedUser);
  };

  const saveUser = async (user) => {
    console.log('email: ' + user.email); //ans here you get the email..
    console.log('id: ' + user.Id); // ans id: undefined
    const url = `${process.env.REACT_APP_BASEURL}/auth/updateUserByEmail/${user.email}`;
    console.log('url ' + url);
    const response = await fetch(url, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(user),
    });

    if (response.status >= 200 && response.status <= 299) {
      console.log('User is updated');
    } else {
      console.log('Something went wrong while updating user');
    }
  };

  return (
    <>
      <h1 className="page-title">Update User</h1>
      <section className="form-container">
        <h4>Edit User</h4>
        <section className="form-wrapper">
          <form className="form" onSubmit={handleSaveUser}>
            <div className="form-control">
              <label htmlFor="email">Email</label>
              <input
                onChange={(e) => setEmail(e.target.value)}
                value={email}
                type="email"
                id="email"
                name="email"
              />
            </div>
            <div className="form-control">
              <label htmlFor="password">Password</label>
              <input
                onChange={(e) => setPassword(e.target.value)}
                value={password}
                type="text"
                id="password"
                name="password"
              />
            </div>
            <div className="form-control">
              <label htmlFor="firstName">First Name</label>
              <input
                onChange={(e) => setFirstName(e.target.value)}
                value={firstName}
                type="text"
                id="firstName"
                name="firstName"
              />
            </div>
            <div className="form-control">
              <label htmlFor="lastName">Last Name</label>
              <input
                onChange={(e) => setLastName(e.target.value)}
                value={lastName}
                type="text"
                id="lastName"
                name="lastName"
              />
            </div>
            <div className="form-control">
              <label htmlFor="phoneNumber">Phone Number</label>
              <input
                onChange={(e) => setPhoneNumber(e.target.value)}
                value={phoneNumber}
                type="tel"
                id="phoneNumber"
                name="phoneNumber"
              />
            </div>
            <div className="form-control">
              <label htmlFor="address">Address</label>
              <input
                onChange={(e) => setAddress(e.target.value)}
                value={address}
                type="text"
                id="address"
                name="address"
              />
            </div>
            <div className="form-control">
              <label htmlFor="userRole">User Role</label>
              <select
                onChange={(e) => setRole(e.target.value)}
                value={role}
                id="role"
                name="role"
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

export default EditUser;
