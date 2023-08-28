import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom'; // Import useParams hook
import './CourseListPage.css';
function CourseDetailsPage() {
  const { courseId } = useParams(); // Get courseId from URL parameter
  const [course, setCourse] = useState(null);

  useEffect(() => {
    // Fetch course details using courseId
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
      }
    };

    fetchCourseDetails();
  }, [courseId]);

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
      <button className="register-button">Register</button>
    </div>
  );
}

export default CourseDetailsPage;
