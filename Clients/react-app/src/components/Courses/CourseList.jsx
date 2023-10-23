import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import CoursesItem from './CoursesItem';

function CourseList({ userRole }) {
  // const [courseData, setCourseData] = useState([]);
  const [courses, setCourses] = useState([]);

  useEffect(() => {
    loadCourses();
  }, []);

  const loadCourses = async () => {
    const token = JSON.parse(localStorage.getItem('token')); //220519_09   2:17:00
    // console.log(token);

    const url = process.env.REACT_APP_BASEURL + '/courses/GetAllCourses';
    const response = await fetch(url, {
      method: 'GET',
      header: {
        Authorization: `bearer ${token}`, ////220519_09   2:20:00
      },
    });

    if (!response.ok)
      console.log('Opps.. couldnt find any courses or something went wrong!!!');
    else {
      setCourses(await response.json());
    }
  };

  const deleteCourse = async (id) => {
    console.log('deletes course with email' + id);
    const url = `${process.env.REACT_APP_BASEURL}/courses/${id}`;
    const response = await fetch(url, {
      method: 'DELETE',
    });
    if (response.status >= 200 && response.status <= 299) {
      console.log('Course is deleted');
      loadCourses();
    } else {
      console.log('Something went wrong while deleting');
    }
  };

  return (
    <>
      {userRole === 'Administrator' && (
        <div className="course-actions">
          <Link to="/addCourse" className="btn">
            Add New Course
          </Link>
        </div>
      )}
      <table>
        <thead>
          <tr>
            <th></th>
            <th>ID</th>
            <th>CourseNr</th>
            <th>Title</th>
            <th>Duration</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {courses.map((items) => (
            <CoursesItem
              course={items}
              // CourseNumber={course.CourseNumber}
              // Name={course.Name}
              // Duration={course.Duration}
              key={items.courseId}
              handleDeleteCourse={deleteCourse}
            />
          ))}
        </tbody>
      </table>
    </>
  );
}
export default CourseList;
