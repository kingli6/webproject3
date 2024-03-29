import { NavLink } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';

function Navbar() {
  const { userRole, setUserRole } = useAuth();
  const navigate = useNavigate();
  //TODO 2
  console.log('userRole in Navbar:', userRole);

  const handleLogout = () => {
    // Clear token from local storage and reset isAuthenticated
    localStorage.removeItem('token');
    setUserRole(null);
    navigate('/');
    // Perform any additional logout logic as needed
  };

  return (
    <nav id="navbar">
      <ul>
        {userRole && (
          <li>
            <NavLink to="/" onClick={handleLogout}>
              Log Out
            </NavLink>
          </li>
        )}
        {userRole === 'Administrator' && (
          <>
            <li>
              <NavLink to="/adminDashboard">Dashboard</NavLink>
            </li>
          </>
        )}
        {userRole === 'User' && (
          <>
            <li>
              <NavLink to="/userDashboard">Dashboard</NavLink>
            </li>
            {/* Add more links specific to the User role */}
          </>
        )}
      </ul>
      <NavLink
        to={
          userRole === 'Administrator'
            ? '/adminDashboard'
            : userRole === 'User'
            ? '/userDashboard'
            : '/login'
        }
        className="logo"
      >
        <h1>
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
// {/*<li>
//  {userRole === 'Administrator' ? (
//   <>
//     <NavLink to="/" onClick={handleLogout}>
//       Log Out
//     </NavLink>
//     <NavLink to="/courseList">Courses</NavLink>
//     <NavLink to="/addCourse">Add Course</NavLink>
//   </>
// ) : (
//   <NavLink to="/login">Log In</NavLink>
// )}
//  <NavLink to="/courseList">Courses</NavLink>
// <NavLink to="/addCourse">Add Course</NavLink>
// </li>
// */}

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
