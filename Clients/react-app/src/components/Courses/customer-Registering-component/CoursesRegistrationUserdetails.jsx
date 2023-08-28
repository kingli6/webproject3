import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom'; // Import Link component
import './CourseListPage.css';

function CoursesRegistrationUserdetails() {
  const [courses, setCourses] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [expandedCourseId, setExpandedCourseId] = useState(null);

  //TODO
  console.log(courses);
  const handleCourseClick = (courseId) => {
    if (expandedCourseId === courseId) {
      // If the clicked course is already expanded, close it
      setExpandedCourseId(null);
    } else {
      // Otherwise, expand the clicked course
      setExpandedCourseId(courseId);
    }
  };

  // const handleRegisterClick = (courseId) => {
  //   // Navigate to the course details page with the courseId as a URL parameter
  //   history.push(`/courses/${courseId}`); <-using history. showed error
  // };

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
  //TODO
  courses.forEach((course) => {
    console.log(':::::::::::::::::::::');
    console.log(course.courseId);
    console.log(typeof course.courseId);
  });
  // const transformedCourses = courses.map((course) => {
  //   console.log(':::::::::::::::::::::');
  //   console.log(course.courseId);
  //   console.log(course.courseId.type());

  //   // Return a transformed value
  //   return {
  //     ...course,
  //     transformedField: course.courseId + '_transformed',
  //   };
  // });
  console.log('Courses::::: ' + courses);
  console.log('expandedCourseId:: ' + expandedCourseId);

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
          <div
            className={`course-container ${
              expandedCourseId === course.courseId ? 'active' : ''
            }`}
            key={course.courseId}
            onClick={() => handleCourseClick(course.courseId)}
          >
            <div className="course-header">
              <h3>{course.name}</h3>
              <p>{course.description}</p>
            </div>
            <div className="course-details">
              <Link
                to={`/courses/${course.courseId}`}
                className="register-link"
              >
                Register
              </Link>
            </div>
            {expandedCourseId === course.courseId && (
              <div className="course-expand-details">
                {/* <p>
                  <strong>Details:</strong> {course.details}
                </p> */}
                <p>
                  <strong>Course Number:</strong> {course.courseNumber}
                </p>
                <p>
                  <strong>Duration:</strong> {course.duration}
                </p>
                <p>
                  {/* TODO change it to enrolled students */}
                  <strong>Enrolled Students:</strong> {course.courseId}
                </p>
              </div>
            )}
          </div>
        ))}
      </div>
    </>
  );
}

export default CoursesRegistrationUserdetails;
