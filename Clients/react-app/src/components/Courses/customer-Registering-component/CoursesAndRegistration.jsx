import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom'; // Import Link component
import './CourseListPage.css';

function CoursesAndRegistration() {
  const [courses, setCourses] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState('');
  const [expandedCourseId, setExpandedCourseId] = useState(null);

  //this doesn't work //TODO
  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  };

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
    loadCategories();
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
          // Filter courses based on the selected category and search query
          ((selectedCategory === '' ||
            course.categoryId === selectedCategory) &&
            // Check if the search query is present in course name, description, or course number
            course.name.toLowerCase().includes(searchQuery.toLowerCase())) ||
          course.description
            .toLowerCase()
            .includes(searchQuery.toLowerCase()) ||
          course.courseNumber.toLowerCase().includes(searchQuery.toLowerCase())
      );
      setCourses(filteredCourses);
    }
  };

  const loadCategories = async () => {
    try {
      const token = JSON.parse(localStorage.getItem('token'));
      const url = `${process.env.REACT_APP_BASEURL}/categories`; // Change the URL to your API endpoint for categories
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          Authorization: `bearer ${token}`,
        },
      });

      if (response.ok) {
        const categoriesData = await response.json();
        setCategories(categoriesData);
      }
    } catch (error) {
      console.error('Error fetching categories:', error);
    }
  };

  useEffect(() => {
    loadCourses();
  }, [searchQuery]);

  return (
    <>
      <Link to="/userProfile" className="profile-button">
        Profile
      </Link>
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
      <div className="category-select">
        <label>Select Category:</label>
        <select
          value={selectedCategory}
          onChange={(e) => setSelectedCategory(e.target.value)}
        >
          <option value="">All Categories</option>
          {categories.map((category) => (
            <option key={category.id} value={category.id}>
              {category.name}
            </option>
          ))}
        </select>
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
              <h3>{course.name}</h3>{' '}
              <p>
                <strong>Course Number:</strong> {course.courseNumber}
              </p>
              <p>{course.description}</p>
            </div>
            <div className="course-details">
              <Link
                to={`/courses/${course.courseId}`}
                className="register-link"
              >
                View
              </Link>
            </div>
            {expandedCourseId === course.courseId && (
              <div className="course-expand-details">
                {/* <p>
                  <strong>Details:</strong> {course.details}
                </p> */}

                <p>
                  <strong>Duration:</strong> {course.duration}
                </p>
                <p>
                  {/* TODO Add Teachers name? */}
                  <strong>Enrolled Students:</strong> {course.enrolledStudents}
                </p>
              </div>
            )}
          </div>
        ))}
      </div>
      <button className="scroll-to-top-button" onClick={scrollToTop}>
        Move to Top
      </button>
    </>
  );
}

export default CoursesAndRegistration;
