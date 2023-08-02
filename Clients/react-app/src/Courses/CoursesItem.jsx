function CoursesItem({ course }) {
  return (
    <tr>
      <td>{course.CourseNumber}</td>
      <td>{course.Name}</td>
      <td>{course.Duration}</td>
      <td>Things</td>
    </tr>
  );
}

export default CoursesItem;
