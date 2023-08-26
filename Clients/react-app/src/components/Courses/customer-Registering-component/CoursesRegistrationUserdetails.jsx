function CoursesRegistrationUserdetails() {
  //
  //
  return (
    <>
      <h2>Hey</h2>
    </>
  );
}
export default CoursesRegistrationUserdetails;

// import React from 'react';
// import { Link } from 'react-router-dom';

// function AdminDashboard() {
//   return (
//     <div>
//       <h2>Welcome to the Admin Dashboard!</h2>
//       <nav>
//         <ul>
//           <li>
//             <Link to="/admin/courses">Courses</Link>
//           </li>
//           <li>
//             <Link to="/admin/users">Users</Link>
//           </li>
//         </ul>
//       </nav>
//     </div>
//   );
// }

// export default AdminDashboard;
// build links to user Log out button. Edit profile button.

// Here is a component for Viewing A List of users
// import { useEffect, useState } from 'react';
// import { Link } from 'react-router-dom';
// import UserItem from './UserItems';

// function UserList({ userRole }) {
//   const [users, setUsers] = useState([]);

//   useEffect(() => {
//     loadUsers();
//   }, []);

//   //
//   const loadUsers = async () => {
//     const token = JSON.parse(localStorage.getItem('token'));
//     const url = process.env.REACT_APP_BASEURL + '/auth/getallusersByAdmin';
//     const response = await fetch(url, {
//       method: 'GET',
//       headers: {
//         Authorization: `bearer ${token}`,
//       },
//     });

//     if (!response.ok) {
//       console.log("Oops.. couldn't find any USERS or something went wrong!");
//     } else {
//       setUsers(await response.json());
//     }
//   };
//   const deleteUser = async (email) => {
//     console.log('deletes user with email' + email);
//     const url = `${process.env.REACT_APP_BASEURL}/auth/${email}`;
//     const response = await fetch(url, {
//       method: 'DELETE',
//     });
//     if (response.status >= 200 && response.status <= 299) {
//       console.log('User is deleted');
//       loadUsers();
//     } else {
//       console.log('Something went wrong while deleting');
//     }
//   };

//   return (
//     <>
//       {userRole === 'Administrator' && (
//         <div className="course-actions">
//           <Link to="/addUser" className="btn">
//             Add User
//           </Link>
//         </div>
//       )}
//       <table>
//         <thead>
//           <tr>
//             <th></th>
//             <th>First Name</th>
//             <th>Last Name</th>
//             <th>Email</th>
//             <th>Phone Number</th>
//             <th>Address</th>
//             <th>Role</th>
//             <th></th>
//           </tr>
//         </thead>
//         <tbody>
//           {users.map((items) => (
//             <UserItem
//               user={items}
//               key={items.id}
//               handleDeleteUser={deleteUser}
//             />
//           ))}
//         </tbody>
//       </table>
//     </>
//   );
// }
// export default UserList;

// Similarly, I want user to viiew a all the list of courses from this link: /courses/GetAllCourses
// The object coming through has this:
// {
//     "courseId": 1,
//     "courseNumber": "1000",
//     "name": "Progamming Basics A",
//     "duration": "Two weeks",
//     "description": "The course is primarily aimed at professionals in software development, suitable for those who want to renew their skills in an existing role or move on to new, more advanced tasks.",
//     "details": "The course is primarily aimed at professionals in software development, suitable for those who want to renew their skills in an existing role or move on to new, more advanced tasks. Proficiency in C++ provides diverse career opportunities and continues to be in high demand across various industries. It’s also crucial for system programming, scientific computing, and working with low-level hardware.C++ is known for its efficiency and performance. It enables developers to write code that executes quickly and consumes fewer system resources. It is widely used in performance-critical applications such as game development, embedded systems, and high-frequency trading. C++ offers efficiency, control, and interoperability with other languages, making it valuable for maintaining legacy codebases and optimizing performance.The course includes a short overview of embedded systems, hardware-oriented programming, and resource-conscious programming. It also contains an overview of how code/programs can be uploaded and executed on microcontrollers.",
//     "registrations": []
//   }

// Notice the registrations property. from there I want information on how many students are registered, Teacher names,
