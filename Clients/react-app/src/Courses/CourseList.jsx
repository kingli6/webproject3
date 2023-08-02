import CoursesItem from './CoursesItem';

function CourseList() {
  const courseData = [
    {
      CourseNumber: '1000',
      Name: 'Progamming Basics A',
      Duration: 'Two weeks',
      Description:
        'The course is primarily aimed at professionals in software development, suitable for those who want to renew their skills in an existing role or move on to new, more advanced tasks.',
      Details:
        'The course is primarily aimed at professionals in software development, suitable for those who want to renew their skills in an existing role or move on to new, more advanced tasks. Proficiency in C++ provides diverse career opportunities and continues to be in high demand across various industries. It’s also crucial for system programming, scientific computing, and working with low-level hardware.C++ is known for its efficiency and performance. It enables developers to write code that executes quickly and consumes fewer system resources. It is widely used in performance-critical applications such as game development, embedded systems, and high-frequency trading. C++ offers efficiency, control, and interoperability with other languages, making it valuable for maintaining legacy codebases and optimizing performance.The course includes a short overview of embedded systems, hardware-oriented programming, and resource-conscious programming. It also contains an overview of how code/programs can be uploaded and executed on microcontrollers.',
    },
    {
      CourseNumber: '1001',
      Name: 'Progamming Basics B',
      Duration: 'Four weeks',
      Description:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna',
      Details:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint',
    },
    {
      CourseNumber: '1002',
      Name: 'Console Application and algorythms',
      Duration: 'Two weeks',
      Description: 'sed do eiusmod tempor incididunt ut labore et dolore magna',
      Details:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint',
    },
    {
      CourseNumber: '1003',
      Name: 'Collaberation and Clean Code',
      Duration: 'Two weeks',
      Description:
        'Lorem ipsum dolor sit amet, consectetur adipiscinncididunt ut labore et dolore magna',
      Details:
        'Lorem ipsum dolor sabore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint',
    },
  ];
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
        {courseData.map((items) => (
          <CoursesItem
            course={items}
            // CourseNumber={course.CourseNumber}
            // Name={course.Name}
            // Duration={course.Duration}
            key={items.CourseNumber}
          />
        ))}
      </tbody>
    </table>
  );
}
export default CourseList;
