import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import './CourseListPage.css';

function CourseDetailsPage() {
  const { courseId } = useParams(); // Get courseId from URL parameter
  const [course, setCourse] = useState(null);
  const [isRegistered, setIsRegistered] = useState(false);

  useEffect(() => {
    const fetchCourseDetails = async () => {
      const token = JSON.parse(localStorage.getItem('token'));
      const url = `${process.env.REACT_APP_BASEURL}/courses/${courseId}`;
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          Authorization: `bearer ${token}`,
        },
      });

      if (response.ok) {
        const courseData = await response.json();
        //TODO
        console.log('Course data::::' + courseData);
        setCourse(courseData);

        // Check if user is already registered for the course
        const registrationStatusUrl = `${process.env.REACT_APP_BASEURL}/registration/check-registration?courseId=${courseId}`;
        const registrationResponse = await fetch(registrationStatusUrl, {
          method: 'GET',
          headers: {
            Authorization: `bearer ${token}`,
          },
        });

        if (registrationResponse.ok) {
          const registrationData = await registrationResponse.json();
          setIsRegistered(registrationData.isRegistered);
        }
      }
    };
    fetchCourseDetails();
  }, [courseId]);

  // useEffect(() => {
  //   const fetchRegistrationStatus = async () => {
  //     const token = JSON.parse(localStorage.getItem('token'));
  //     const url = `${process.env.REACT_APP_BASEURL}/registration/check-registration?courseId=${courseId}`;
  //     const response = await fetch(url, {
  //       method: 'GET',
  //       headers: {
  //         Authorization: `bearer ${token}`,
  //       },
  //     });

  //     if (response.ok) {
  //       const registrationData = await response.json();
  //       setIsRegistered(registrationData.isRegistered);
  //     }
  //   };
  //   fetchRegistrationStatus();
  // }, [courseId]);

  const handleRegisterClick = async () => {
    const token = JSON.parse(localStorage.getItem('token'));
    const url = `${process.env.REACT_APP_BASEURL}/registration/register-course/${course.courseId}`;
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        Authorization: `bearer ${token}`,
      },
    });

    if (response.ok) {
      setIsRegistered(true);
    }
  };

  if (!course) {
    return <p>Loading...</p>;
  }

  return (
    <div className="course-details-container">
      <h2>{course.name}</h2>
      <p>{course.description}</p>
      <p>
        <strong>Course Number:</strong> {course.courseNumber}
      </p>
      <p>
        <strong>Duration:</strong> {course.duration}
      </p>
      <div className="button-container">
        {!isRegistered ? (
          <p>You are already registered for this course.</p>
        ) : (
          // Show "Delete Registration" button if registered or while deleting
          <button className="register-link" onClick={handleRegisterClick}>
            Register for Course
          </button>
        )}
        <Link to="/userDashboard" className="back-button">
          Back
        </Link>
      </div>
    </div>
  );
}

export default CourseDetailsPage;
