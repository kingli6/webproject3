import CoursesItem from './CoursesItem';

function CourseList() {
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
        <CoursesItem />
      </tbody>
    </table>
  );
}
export default CourseList;
