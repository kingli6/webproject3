import { useNavigate } from 'react-router-dom';

function CoursesItem({ course, handleDeleteCourse }) {
  const navigate = useNavigate();

  function onEditClickHandler() {
    navigate(`/editCourse/${course.courseId}`);
  }

  const onDeleteClickHandler = () => {
    console.log(`We will delete course${course.CourseNumber}`);
    handleDeleteCourse(course.courseId); //delegates 220519_09.. 1:20:00
  };

  return (
    <tr>
      <td>
        <span onClick={onEditClickHandler}>
          <i className="fa-solid fa-pencil edit"></i>
        </span>
      </td>
      <td>{course.courseId}</td>
      <td>{course.courseNumber}</td>
      <td>{course.name}</td>
      <td>{course.duration}</td>
      <td>
        <span onClick={onDeleteClickHandler}>
          <i className="fa-solid fa-trash-can delete"></i>
        </span>
      </td>
    </tr>
  );
}

export default CoursesItem;
