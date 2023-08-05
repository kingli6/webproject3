function CoursesItem({ course }) {
  function onEditClickHandler() {
    console.log(`'should update course${course.CourseNumber}`);
  }

  const onDeleteClickHandler = () => {
    console.log(`We will delete course${course.CourseNumber}`);
  };

  return (
    <tr>
      <td>
        <span onClick={onEditClickHandler}>
          <i className="fa-solid fa-pencil edit"></i>
        </span>
      </td>
      <td>{course.courseNumber}</td>
      <td>{course.name}</td>
      <td>{course.duration}</td>
      <td>Things</td>
      <td>
        <span onClick={onDeleteClickHandler}>
          <i className="fa-solid fa-trash-can delete"></i>
        </span>
      </td>
    </tr>
  );
}

export default CoursesItem;
