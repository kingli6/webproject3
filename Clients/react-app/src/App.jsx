import {
  BrowserRouter as Router,
  Route,
  Routes,
  Switch,
} from 'react-router-dom';
import React, { useState } from 'react';

import Navbar from './components/navbar/Navbar';
import AddCourse from './components/Courses/AddCourse';
//My first component - App.jsx
import CoursesList from './components/Courses/CourseList';
import EditCourse from './components/Courses/EditCourse';
import Login from './components/authentication/Login';
import './utilities.css';
import './styles.css';
import AdminDashboard from './components/adminDashboard/AdminDashoard';
import UserDashboard from './components/userDashboard/UserDashboard';
import AdminCourseList from './components/adminDashboard/AdminCourseList';
import { AuthProvider } from './components/context/AuthContext';
import AddUser from './components/users/AddUser';
import AdminUserList from './components/adminDashboard/AdminUserList';
import EditUser from './components/users/EditUser';
import CourseDetailsPage from './components/Courses/customer-Registering-component/CourseDetailsPage';

function App() {
  const [userRole, setUserRole] = useState(null);

  return (
    <AuthProvider>
      <Router>
        <Navbar userRole={userRole} />
        <main>
          <Routes>
            <Route path="/" element={<Login setUserRole={setUserRole} />} />
            {userRole === 'Administrator' && (
              <Route path="/adminDashboard" element={<AdminDashboard />} />
            )}
            {userRole === 'User' && (
              <Route path="/userDashboard" element={<UserDashboard />} />
            )}
            <Route path="/adminDashboard" element={<AdminDashboard />} />
            <Route path="/admin/courses" element={<AdminCourseList />} />
            {/* //WHERE DOES THIS GO!? */}
            {/* <Route path="/courseList" element={<CoursesList />} /> */}
            {/* /////////////////////// */}
            <Route path="/addCourse" element={<AddCourse />} />
            <Route path="/editCourse/:id" element={<EditCourse />} />
            <Route
              path="/login"
              element={<Login setUserRole={setUserRole} />}
            />
            <Route path="/admin/users" element={<AdminUserList />} />

            {/* USER LINKS */}
            <Route path="/userDashboard" element={<UserDashboard />} />
            <Route path="/addUser" element={<AddUser />} />
            <Route path="/editUser/:id" element={<EditUser />} />
            <Route path="/courses/:courseId" element={<CourseDetailsPage />} />
          </Routes>
        </main>
      </Router>
    </AuthProvider>
  );
}
export default App;
