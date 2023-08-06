import { useEffect, useState } from 'react';

import CoursesItem from './CoursesItem';

function CourseList() {
  // const [courseData, setCourseData] = useState([]);
  const [courses, setCourses] = useState([]);

  useEffect(() => {
    loadCourse();
  }, []);

  const loadCourse = async () => {
    const baseUrl = process.env.REACT_APP_BASEURL + '/courses/GetAllCourses'; //${process.env.REACT_APP_BASEURL}
    const response = await fetch(baseUrl);

    if (!response.ok)
      console.log('Opps.. couldnt find any courses or something went wrong!!!');

    const data = await response.json();
    console.log(data);
    setCourses(data);
    // try {
    //   const response = await fetch(url);

    //   if (!response.ok) {
    //     console.log('Response not OK:', response);
    //     console.log('Response status:', response.status);
    //   }

    //   const data = await response.json();
    //   console.log('Data:', data);

    //   setCourseData(data);
    // } catch (error) {
    //   console.error('Error:', error);
    // }
  };

  return (
    <table>
      <thead>
        <tr>
          <th>Item1</th>
          <th>Item2</th>
          <th>Item3</th>
          <th>Item4</th>
        </tr>
      </thead>
      <tbody>
        {/* <CoursesItem thing="Here is something" name="Jonathan" place="Övik" />
        <CoursesItem
          thing="here is another item"
          name="Björn"
          place="Stockholm"
        /> */}
        {courses.map((items) => (
          <CoursesItem
            course={items}
            // CourseNumber={course.CourseNumber}
            // Name={course.Name}
            // Duration={course.Duration}
            key={items.courseId}
          />
        ))}
      </tbody>
    </table>
  );
}
export default CourseList;