//My first component - App.jsx
import CoursesList from './Courses/CourseList';

function App() {
  return (
    <main>
      <h1 className="page-title">Hey! From App.jsx component page</h1>
      <CoursesList />
    </main>
  );
}

export default App;
