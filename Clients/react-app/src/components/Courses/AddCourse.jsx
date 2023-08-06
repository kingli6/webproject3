import { useState } from 'react';

function AddCourse() {
  const [courseNum, setCourseNum] = useState('');

  const onHandlerCourseNumTextChange = (e) => {
    console.log('texten är ändrad');
    console.log(e);
    setCourseNum();
  };
  return (
    <>
      <h1 clasNames="page-title">Add a new course</h1>
      <section className="form-container">
        <h4>Course somethingsomething</h4>
        <section className="form-wrapper">
          <form className="form" method="post">
            <div className="form-control">
              <label htmlFor="">Course Number</label>
              <input
                onChange={onHandlerCourseNumTextChange}
                value="courseNumber"
                type="text"
                id="courseNumber"
                name="courseNumber"
              />
            </div>
            <div className="form-control">
              <label htmlFor="name">Name</label>
              <input type="text" id="name" name="name" />
            </div>
            <div className="form-control">
              <label htmlFor="duration">Duration</label>
              <input type="text" id="duration" name="duration" />
            </div>
            <div className="form-control">
              <label htmlFor="description">Description</label>
              <input type="text" id="description" name="description" />
            </div>
            <div className="form-control">
              <label htmlFor="details">Course Details</label>
              <textarea type="text" id="details" name="details"></textarea>
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
