import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

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
      console.log("Oops.. couldn't find any users or something went wrong!");
    } else {
      setUsers(await response.json());
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
        {users.map((user) => (
          <tr key={user.id}>
            <td>{user.firstName}</td>
            <td>{user.lastName}</td>
            <td>{user.email}</td>
            <td>{user.phoneNumber}</td>
            <td>{user.address}</td>
            <td>{user.role}</td>
          </tr>
        ))}
        {/* //TODO
        // .map functtion to display everything */}
      </tbody>
    </table>
  );
}
export default UserList;
