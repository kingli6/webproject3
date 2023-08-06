import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './components/home/Home';
import Navbar from './components/navbar/Navbar';
//My first component - App.jsx
import CoursesList from './components/Courses/CourseList';

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
        </Routes>
      </main>
    </Router>
  );
}

export default App;
