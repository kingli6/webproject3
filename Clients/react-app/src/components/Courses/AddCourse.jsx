import { useState } from 'react';

function AddCourse() {
  const [isSuccess, setIsSuccess] = useState(false);
  const [useCourseNum, setCourseNum] = useState('');
  const [useCourseName, setCourseName] = useState('');
  const [useDuration, setDuration] = useState('');
  const [useDescription, setDescription] = useState('');
  const [useDetails, setDetails] = useState('');
  const [useCategory, setCategory] = useState('');

  const onHandlerCourseNumTextChange = (e) => {
    //220518_13 1:45:00
    console.log('texten är ändrad', e.target.value);
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
  const onHandlerCategoryTextChange = (e) => {
    setCategory(e.target.value);
  };

  const handleSaveCourse = (e) => {
    //220518_13 1:45:00
    e.preventDefault(); //don't act (form) in the standard way when we submit(to empty field, reload page, etc).
    const course = {
      courseNumber: useCourseNum, //if both variables have the same name, you can simply use it once 220518_13 1:45:00
      name: useCourseName,
      duration: useDuration,
      description: useDescription,
      details: useDetails,
      category: useCategory,
    };

    saveCourse(course);
  };

  const saveCourse = async (course) => {
    const url = `${process.env.REACT_APP_BASEURL}/courses/AddCourse`;
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(course),
    });
    if (response.status >= 200 && response.status <= 299) {
      setIsSuccess(true);
      console.log('Course is saved');
      console.log('Here is the response:');
      console.log(response);
    } else {
      console.log('something went wrong while saving course');
    }
  };
  return (
    <>
      <h1 className="page-title">Add Course</h1>
      {isSuccess && <h3 className="page-title">Course added successfully!</h3>}
      <section className="form-container">
        <h4>Create New Course</h4>
        <section className="form-wrapper">
          <form className="form" onSubmit={handleSaveCourse}>
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
                type="text"
                id="name"
                name="name"
              />
            </div>
            <div className="form-control">
              <label htmlFor="duration">Duration</label>
              <input
                onChange={onHandlerDurationTextChange}
                type="text"
                id="duration"
                name="duration"
              />
            </div>
            <div className="form-control">
              <label htmlFor="category">Category</label>
              <input
                onChange={onHandlerCategoryTextChange}
                value={useCategory}
                type="text"
                id="category"
                name="category"
              />
            </div>
            <div className="form-control">
              <label htmlFor="description">Description</label>
              <input
                onChange={onHandlerDescriptionTextChange}
                type="text"
                id="description"
                name="description"
              />
            </div>
            <div className="form-control">
              <label htmlFor="details">Course Details</label>
              <textarea
                onChange={onHandlerDetailsTextChange}
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
export default AddCourse;
