import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import UserItem from './UserItems';

function UserList({ userRole }) {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    loadUsers();
  }, []);

  //
  const loadUsers = async () => {
    const token = JSON.parse(localStorage.getItem('token'));
    const url = process.env.REACT_APP_BASEURL + '/auth/getallusersByAdmin';
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
    <>
      {userRole === 'Administrator' && (
        <div className="course-actions">
          <Link to="/addUser" className="btn">
            Add User
          </Link>
        </div>
      )}
      <table>
        <thead>
          <tr>
            <th></th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Phone Number</th>
            <th>Address</th>
            <th>Role</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {users.map((items) => (
            <UserItem
              user={items}
              key={items.id}
              handleDeleteUser={deleteUser}
            />
          ))}
        </tbody>
      </table>
    </>
  );
}
export default UserList;
