import React, { useState } from 'react';

const AddUserForm = () => {
  const [isSuccess, setIsSuccess] = useState(false);
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    address: '',
    userRole: '',
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSaveUser = async (e) => {
    e.preventDefault();

    try {
      const url = `${process.env.REACT_APP_BASEURL}/auth/createUser`;
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: formData.email,
          password: formData.password,
          firstName: formData.firstName,
          lastName: formData.lastName,
          phoneNumber: formData.phoneNumber,
          address: formData.address,
          userRole: [formData.userRole],
        }),
      });

      if (response.ok) {
        setIsSuccess(true);
      } else {
        setIsSuccess(false);
      }
    } catch (error) {
      // Handle network or other errors
    }
  };

  return (
    <div>
      <h1 className="page-title">Add User</h1>
      {isSuccess && <h3 className="page-title">User added successfully!</h3>}
      <section className="form-container">
        <h4>Create New User</h4>
        <section className="form-wrapper">
          <form className="form" onSubmit={handleSaveUser}>
            {/* Email input */}
            <div className="form-control">
              <label htmlFor="email">
                Email<span style={{ color: 'red' }}>*</span>
              </label>
              <input
                onChange={handleInputChange}
                value={formData.email}
                type="email"
                id="email"
                name="email"
                required
              />
            </div>

            {/* Password input */}
            <div className="form-control">
              <label htmlFor="password">
                Password<span style={{ color: 'red' }}>*</span>
              </label>
              <input
                onChange={handleInputChange}
                value={formData.password}
                type="password"
                id="password"
                name="password"
                required
              />
            </div>

            {/* First Name input */}
            <div className="form-control">
              <label htmlFor="firstName">First Name</label>
              <input
                onChange={handleInputChange}
                value={formData.firstName}
                type="text"
                id="firstName"
                name="firstName"
              />
            </div>

            {/* Last Name input */}
            <div className="form-control">
              <label htmlFor="lastName">Last Name</label>
              <input
                onChange={handleInputChange}
                value={formData.lastName}
                type="text"
                id="lastName"
                name="lastName"
              />
            </div>

            {/* Phone Number input */}
            <div className="form-control">
              <label htmlFor="phoneNumber">Phone Number</label>
              <input
                onChange={handleInputChange}
                value={formData.phoneNumber}
                type="tel"
                id="phoneNumber"
                name="phoneNumber"
              />
            </div>

            {/* Address input */}
            <div className="form-control">
              <label htmlFor="address">Address</label>
              <input
                onChange={handleInputChange}
                value={formData.address}
                type="text"
                id="address"
                name="address"
              />
            </div>

            {/* User Role dropdown */}
            <div className="form-control">
              <label htmlFor="userRole">
                User Role<span style={{ color: 'red' }}>*</span>
              </label>
              <select
                onChange={handleInputChange}
                value={formData.userRole}
                id="userRole"
                name="userRole"
                required
              >
                <option value="">Choose a role</option>
                <option value="User">User</option>
                <option value="Administrator">Administrator</option>
              </select>
            </div>

            {/* Save button */}
            <button type="submit" className="btn">
              Save
            </button>
          </form>
        </section>
      </section>
    </div>
  );
};

export default AddUserForm;
// import { useState } from 'react';
// import SaveUser from './SaveUser';

// function AddUser() {
//   const [email, setEmail] = useState('');
//   const [password, setPassword] = useState('');
//   const [firstName, setFirstName] = useState('');
//   const [lastName, setLastName] = useState('');
//   const [phoneNumber, setPhoneNumber] = useState('');
//   const [address, setAddress] = useState('');
//   const [userRole, setUserRole] = useState('');

//   const [isSuccess, setIsSuccess] = useState(false);
//   const clearForm = () => {
//     setEmail('');
//     setPassword('');
//     setFirstName('');
//     setLastName('');
//     setPhoneNumber('');
//     setAddress('');
//     setUserRole('');
//   };
//   const handleEmailChange = (e) => {
//     setEmail(e.target.value);
//   };
//   const handlePasswordChange = (e) => {
//     setPassword(e.target.value);
//   };
//   const handleFirstNameChange = (e) => {
//     setFirstName(e.target.value);
//   };
//   const handleLastNameChange = (e) => {
//     setLastName(e.target.value);
//   };
//   const handlePhoneNumberChange = (e) => {
//     setPhoneNumber(e.target.value);
//   };
//   const handleAddressChange = (e) => {
//     setAddress(e.target.value);
//   };
//   const handleUserRoleChange = (e) => {
//     setUserRole(e.target.value);
//   };

//   // const handleSaveUser = async (e) => {
//   //   e.preventDefault();
//   //   console.log('xxx' + e);
//   //   const newUser = {
//   //     email,
//   //     password,
//   //     firstName,
//   //     lastName,
//   //     phoneNumber,
//   //     address,
//   //     userRole: [userRole],
//   //   };

//   //   const savedUser = await saveUser(newUser);
//   //   if (savedUser) {
//   //     setIsSuccess(true);
//   //     clearForm();
//   //   } else {
//   //     setIsSuccess(false);
//   //   }
//   // };
//   const handleSaveUser = async (e) => {
//     e.preventDefault();
//     const user = {
//       email,
//       password,
//       firstName,
//       lastName,
//       phoneNumber,
//       address,
//       userRole: 'User', //JSON.stringify(Object.entries(userRole)),
//     };

//     await SaveUser(user);
//   };

//   // async function saveUser(user) {
//   //   const url = `${process.env.REACT_APP_BASEURL}/auth/createUser`;
//   //   const response = await fetch(url, {
//   //     method: 'POST',
//   //     headers: {
//   //       'Content-Type': 'application/json',
//   //     },
//   //     body: JSON.stringify(user),
//   //   });

//   //   if (response.status >= 200 && response.status <= 299) {
//   //     setIsSuccess(true);
//   //     clearForm();
//   //   } else {
//   //     setIsSuccess(false);
//   //   }
//   // }
//   //xx

//   return (
//     <>
//       {isSuccess && <h3>User added successfully!</h3>}
//       <h1 className="page-title">Add User</h1>
//       <section className="form-container">
//         <h4>Create New User</h4>
//         <section className="form-wrapper">
//           <form className="form" onSubmit={handleSaveUser}>
//             <div className="form-control">
//               <label htmlFor="email">
//                 Email<span style={{ color: 'red' }}>*</span>
//               </label>
//               <input
//                 onChange={handleEmailChange}
//                 value={email}
//                 type="email"
//                 id="email"
//                 name="email"
//               />
//             </div>
//             <div className="form-control">
//               <label htmlFor="password">
//                 Password<span style={{ color: 'red' }}>*</span>
//               </label>
//               <input
//                 onChange={handlePasswordChange}
//                 type="text"
//                 id="current-password"
//                 name="current-password"
//               />
//             </div>
//             <div className="form-control">
//               <label htmlFor="firstName">First Name</label>
//               <input
//                 onChange={handleFirstNameChange}
//                 type="text"
//                 id="firstName"
//                 name="firstName"
//               />
//             </div>
//             <div className="form-control">
//               <label htmlFor="lastName">Last Name</label>
//               <input
//                 onChange={handleLastNameChange}
//                 type="text"
//                 id="lastName"
//                 name="lastName"
//               />
//             </div>
//             <div className="form-control">
//               <label htmlFor="phoneNumber">Phone Number</label>
//               <input
//                 onChange={handlePhoneNumberChange}
//                 type="tel"
//                 id="phoneNumber"
//                 name="phoneNumber"
//               />
//             </div>
//             <div className="form-control">
//               <label htmlFor="address">Address</label>
//               <input
//                 onChange={handleAddressChange}
//                 type="text"
//                 id="address"
//                 name="address"
//               />
//             </div>
//             <div className="form-control">
//               <label htmlFor="userRole">
//                 User Role<span style={{ color: 'red' }}>*</span>
//               </label>
//               <select
//                 onChange={handleUserRoleChange}
//                 value={userRole}
//                 id="userRole"
//                 name="userRole"
//               >
//                 <option value="">Choose a role</option>
//                 <option value="User">User</option>
//                 <option value="Administrator">Administrator</option>
//                 {/* <option value="Teacher">Teacher</option> */}
//               </select>
//             </div>
//             <button type="submit" className="btn">
//               Save
//             </button>
//           </form>
//         </section>
//       </section>
//     </>
//   );
// }
// export default AddUser;
