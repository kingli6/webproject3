import { NavLink } from 'react-router-dom';

function Navbar() {
  return (
    <nav id="navbar">
      <ul>
        <li>
          <NavLink to="/">Home</NavLink>
          <NavLink to="/courseList">Courses</NavLink>
          <NavLink to="/addCourse">Add Course</NavLink>
        </li>
      </ul>
      <h1 className="logo">
        <span className="text-primary">
          <i className="fas fa-car"></i> Westcoast
        </span>
        College
      </h1>
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
