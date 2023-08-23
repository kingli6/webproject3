import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import UserItem from './UserItems';

function UserList({ userRole }) {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    loadUsers();
  }, []);

  const loadUsers = async () => {
    const token = JSON.parse(localStorage.getItem('token'));
    const url = process.env.REACT_APP_BASEURL + '/auth/GetAllUsers';
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        Authorization: `bearer ${token}`,
      },
    });

    if (!response.ok) {
      console.log("Oops.. couldn't find any USERS or something went wrong!");
    } else {
      setUsers(await response.json());
    }
  };
  const deleteUser = async (email) => {
    console.log('deletes user with email' + email);
    const url = `${process.env.REACT_APP_BASEURL}/auth/${email}`;
    const response = await fetch(url, {
      method: 'DELETE',
    });
    if (response.status >= 200 && response.status <= 299) {
      console.log('User is deleted');
      loadUsers();
    } else {
      console.log('Something went wrong while deleting');
    }
  };

  return (
    //TODO Add User button Link
    <table>
      <thead>
        <tr>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Email</th>
          <th>Phone Number</th>
          <th>Address</th>
          <th>Role</th>
        </tr>
      </thead>
      <tbody>
        {/* //TODO
          // .map functtion to display everything */}
        {users.map((items) => (
          <UserItem user={items} key={items.id} handleDeleteUser={deleteUser} />
          //   <tr key={user.id}>
          //     <td>{user.firstName}</td>
          //     <td>{user.lastName}</td>
          //     <td>{user.email}</td>
          //     <td>{user.phoneNumber}</td>
          //     <td>{user.address}</td>
          //     <td>{user.role}</td>
          //   </tr>
        ))}
      </tbody>
    </table>
  );
}
export default UserList;
