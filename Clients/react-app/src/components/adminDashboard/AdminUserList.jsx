import React from 'react';
import CourseList from '../Courses/CourseList';

function AdminCourseList() {
  return (
    <div>
      <h2>Admin Course List</h2>
      <CourseList userRole="Administrator" />
    </div>
  );
}

export default AdminCourseList;
