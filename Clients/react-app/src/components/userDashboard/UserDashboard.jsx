import React from 'react';
import CoursesRegistrationUserdetails from '../Courses/customer-Registering-component/CoursesRegistrationUserdetails';

function UserDashboard() {
  return (
    <div>
      {/* //TODO User name? */}
      <h2>Welcome to the User Dashboard!</h2>
      {/* Load the Courses RegistationUserdetails page here */}
      <CoursesRegistrationUserdetails />
    </div>
  );
}
//LInk to Courses.-> To register
export default UserDashboard;
