//My first component - App.jsx
import CoursesList from './components/Courses/CourseList';

import './utilities.css';
import './styles.css';

function App() {
  return (
    <main>
      <h1 className="page-title">Hey! From App.jsx component page</h1>
      <CoursesList />
    </main>
  );
}

export default App;
