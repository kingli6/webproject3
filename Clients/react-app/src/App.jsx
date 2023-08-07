import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './components/home/Home';
import Navbar from './components/navbar/Navbar';
import AddCourse from './components/Courses/AddCourse';
//My first component - App.jsx
import CoursesList from './components/Courses/CourseList';
import EditCourse from './components/Courses/EditCourse';

import './utilities.css';
import './styles.css';

function App() {
  return (
    <Router>
      <Navbar />
      <main>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/courseList" element={<CoursesList />} />
          <Route path="/addCourse" element={<AddCourse />} />
          <Route path="/editCourse/:id" element={<EditCourse />} />
        </Routes>
      </main>
    </Router>
  );
}

export default App;
