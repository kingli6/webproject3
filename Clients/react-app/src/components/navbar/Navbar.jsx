import { NavLink } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

function Navbar() {
  const { isAuthenticated, setIsAuthenticated } = useAuth();

  const handleLogout = () => {
    // Clear token from local storage and reset isAuthenticated
    localStorage.removeItem('token');
    setIsAuthenticated(false);
    // Perform any additional logout logic as needed
  };

  return (
    <nav id="navbar">
      <ul>
        <li>
          {isAuthenticated ? (
            <button onClick={handleLogout}>Log Out</button>
          ) : (
            <NavLink to="/login">Log In</NavLink>
          )}
          <NavLink to="/courseList">Courses</NavLink>
          <NavLink to="/addCourse">Add Course</NavLink>
        </li>
      </ul>
      <NavLink to="/">
        <h1 className="logo">
          <span className="text-primary">
            <i className="fa-solid fa-graduation-cap"></i> Westcoast
          </span>
          College
        </h1>
      </NavLink>
    </nav>
  );
}
export default Navbar;

// <li>
//           <a href="...">Start</a>
//         </li>
//         <li>
//           <a href="...." className="active">
//             Our Courses
//           </a>
//         </li>
//         <li>
//           <a href="....">Services</a>
//         </li>
//         <li>
//           <a href="....">About us</a>
//         </li>
//         <li>
//           <a href="...." title="admin">
//             <i className="fa-solid fa-gear"></i>
//           </a>
//         </li>
