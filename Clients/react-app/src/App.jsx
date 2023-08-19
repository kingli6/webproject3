import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { useState } from 'react';

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

import { AuthProvider } from './components/context/AuthContext';
import React from 'react';

function App() {
  const [userRole, setUserRole] = useState(null);
  return (
    <AuthProvider>
      <Router>
        <Navbar />
        <main>
          <Routes>
            <Route path="/" element={<Login setUserRole={setUserRole} />} />
            {userRole === 'Administrator' && (
              <Route path="/adminDashboard" element={<AdminDashboard />} />
            )}
            {userRole === 'User' && (
              <Route path="/userDashboard" element={<UserDashboard />} />
            )}

            <Route path="/courseList" element={<CoursesList />} />
            <Route path="/addCourse" element={<AddCourse />} />
            <Route path="/editCourse/:id" element={<EditCourse />} />
            <Route path="/login" element={<Login />} />
          </Routes>
        </main>
      </Router>
    </AuthProvider>
  );
}
export default App;
