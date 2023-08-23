import React from 'react';
import UserList from '../users/UserList';

function AdminCourseList() {
  return (
    <div>
      <h2>Users List</h2>
      <UserList userRole="Administrator" />
    </div>
  );
}

export default AdminCourseList;
