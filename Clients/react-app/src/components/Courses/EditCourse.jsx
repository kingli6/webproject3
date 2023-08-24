import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

function EditCourse() {
  const params = useParams();

  const [useCourseId, setCourseId] = useState('');
  const [useCourseNum, setCourseNum] = useState('');
  const [useCourseName, setCourseName] = useState('');
  const [useDuration, setDuration] = useState('');
  const [useDescription, setDescription] = useState('');
  const [useDetails, setDetails] = useState('');

  useEffect(() => {
    fetchCourse(params.id);
  }, [params.id]);

  const fetchCourse = async (id) => {
    const url = `${process.env.REACT_APP_BASEURL}/courses/${id}`;
    const response = await fetch(url);

    if (!response.ok) {
      console.log("Couldn't find course, or something went wrong...");
    }
    const course = await response.json(); //<-- here we have the entire object, with id...
    // console.log(course);
    setCourseId(course.courseId);
    setCourseNum(course.courseNumber);
    setCourseName(course.name);
    setDuration(course.duration);
    setDescription(course.description);
    setDetails(course.details);
  };

  const onHandlerCourseIdTextChange = (e) => {
    setCourseId(e.target.value);
  };
  const onHandlerCourseNumTextChange = (e) => {
    // console.log('texten är ändrad', e.target.value);
    setCourseNum(e.target.value);
  };
  const onHandlerCourseNameTextChange = (e) => {
    setCourseName(e.target.value);
  };
  const onHandlerDurationTextChange = (e) => {
    setDuration(e.target.value);
  };
  const onHandlerDescriptionTextChange = (e) => {
    setDescription(e.target.value);
  };
  const onHandlerDetailsTextChange = (e) => {
    setDetails(e.target.value);
  };
  const handleSaveCourse = (e) => {
    e.preventDefault(); //don't act (form) in the standard way when we submit(to empty field, reload page, etc).
    const course = {
      courseNumber: useCourseNum, //if both variables have the same name, you can simply use it once 220518_13 1:45:00
      name: useCourseName,
      duration: useDuration,
      description: useDescription,
      details: useDetails,
    };

    console.log(course);
    saveCourse(course); //we are not getting in the id. ..220518_13... 2:59:00  Ans: We don't need to send the object with id...
  };

  const saveCourse = async (course) => {
    const url = `${process.env.REACT_APP_BASEURL}/courses/ReplaceCourse/${useCourseId}`;
    const response = await fetch(url, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(course),
    });
    console.log('Here is the response:');
    console.log(response);
    if (response.status >= 200 && response.status <= 299) {
      console.log('Course is saved');
    } else {
      console.log('something went wrong while saving course');
    }
  };

  return (
    <>
      <h1 className="page-title">Update Course</h1>
      <section className="form-container">
        <h4>Create New Course</h4>
        <section className="form-wrapper">
          <form className="form" onSubmit={handleSaveCourse}>
            <input
              onChange={onHandlerCourseIdTextChange}
              value={useCourseId}
              type="hidden"
              id="courseId"
              name="courseId"
            />
            <div className="form-control">
              <label htmlFor="">Course Number</label>
              <input
                onChange={onHandlerCourseNumTextChange}
                value={useCourseNum}
                type="text"
                id="courseNumber"
                name="courseNumber"
              />
            </div>
            <div className="form-control">
              <label htmlFor="name">Name</label>
              <input
                onChange={onHandlerCourseNameTextChange}
                value={useCourseName}
                type="text"
                id="name"
                name="name"
              />
            </div>
            <div className="form-control">
              <label htmlFor="duration">Duration</label>
              <input
                onChange={onHandlerDurationTextChange}
                value={useDuration}
                type="text"
                id="duration"
                name="duration"
              />
            </div>
            <div className="form-control">
              <label htmlFor="description">Description</label>
              <input
                onChange={onHandlerDescriptionTextChange}
                value={useDescription}
                type="text"
                id="description"
                name="description"
              />
            </div>
            <div className="form-control">
              <label htmlFor="details">Course Details</label>
              <textarea
                onChange={onHandlerDetailsTextChange}
                value={useDetails}
                type="text"
                id="details"
                name="details"
              ></textarea>
            </div>
            <button type="submit" className="btn">
              Save
            </button>
          </form>
        </section>
      </section>
    </>
  );
}
export default EditCourse;
