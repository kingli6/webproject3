import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import './CourseListPage.css';

function CoursesRegistrationUserdetails() {
  const [courses, setCourses] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');

  useEffect(() => {
    loadCourses();
  }, []);

  const loadCourses = async (query) => {
    const token = JSON.parse(localStorage.getItem('token'));
    const url = process.env.REACT_APP_BASEURL + '/courses/GetAllCourses';
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        Authorization: `bearer ${token}`,
      },
    });

    if (!response.ok) {
      console.log("Oops.. couldn't find any courses or something went wrong!");
    } else {
      const coursesData = await response.json();

      // Filter courses based on the search query
      const filteredCourses = coursesData.filter(
        (course) =>
          // Check if the search query is present in course name, description, or course number
          course.name.toLowerCase().includes(searchQuery.toLowerCase()) ||
          course.description
            .toLowerCase()
            .includes(searchQuery.toLowerCase()) ||
          course.courseNumber.toLowerCase().includes(searchQuery.toLowerCase())
      );

      setCourses(filteredCourses);
    }
  };

  useEffect(() => {
    loadCourses();
  }, [searchQuery]);

  return (
    <>
      <div className="search-container">
        <input
          type="text"
          placeholder="Search by name or number"
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
        />
        <button className="search-button" onClick={loadCourses}>
          Search
        </button>
      </div>
      <div className="course-list">
        {courses.map((course) => (
          <div className="course-container" key={course.id}>
            <div className="course-header">
              <h3>{course.name}</h3>
              <p>{course.description}</p>
            </div>
            <div className="course-details">
              <p>
                <strong>Course Number:</strong> {course.courseNumber}
              </p>
              <p>
                <strong>Duration:</strong> {course.duration}
              </p>
              <button className="register-button">Register</button>
            </div>
            <div className="course-expand-details">
              <p>
                <strong>Details:</strong> {course.details}
              </p>
              <p>
                <strong>Enrolled Students:</strong> {course.enrolledStudents}
              </p>
            </div>
          </div>
        ))}
      </div>
    </>
  );
}

export default CoursesRegistrationUserdetails;
