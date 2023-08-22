import React from 'react';
import { Link } from 'react-router-dom';

function AdminDashboard() {
  return (
    <div>
      <h2>Welcome to the Admin Dashboard!</h2>
      <nav>
        <ul>
          <li>
            <Link to="/admin/courses">Courses</Link>
          </li>
          <li>
            <Link to="/admin/users">Users</Link>
          </li>
        </ul>
      </nav>
    </div>
  );
}

export default AdminDashboard;
