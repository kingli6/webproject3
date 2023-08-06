function AddCourse() {
  return (
    <>
      <h1 clasNames="page-title">Add a new course</h1>
      <section className="form-container">
        <h4>Course somethingsomething</h4>
        <form className="form" asp-action="Create" method="post">
          <div className="form-control">
            <label asp-for="@Model!.CourseNumber"></label>
            <input asp-for="@Model!.CourseNumber" />
          </div>
          <div className="form-control">
            <label asp-for="@Model!.Name"></label>
            <input asp-for="@Model!.Name" />
          </div>
          <div className="form-control">
            <label asp-for="@Model!.Duration"></label>
            <input asp-for="@Model!.Duration" />
          </div>
          <div className="form-control">
            <label asp-for="@Model!.Description"></label>
            <input asp-for="@Model!.Description" />
          </div>
          <div className="form-control">
            <label asp-for="@Model!.Details"></label>
            <textarea asp-for="@Model!.Details"></textarea>
          </div>
          <button type="submit" className="btn">
            Save
          </button>
        </form>
      </section>
    </>
  );
}
export default AddCourse;
