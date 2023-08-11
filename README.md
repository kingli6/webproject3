# Dotnet backend and react front end project

A school website that allows users to create account, join courses, an admin site that can edit users.
Feel free to contact me for information.

### To get started

    Download the repo
    In the vscode terminal, while inside College-API folder, type:
     -dotnet restore	//to recreate system required files locally.
     -dotnet build		// to test if there are errors in the project.
     -dotnet run or -dotnet watch run //to start the api project

    To view all the backend api endpoints (with swagger). When the webpage pops up when you start the project, add "/swagger" to the end of the link.
    Ex: http://localhost:5223/swagger

    React
     -npm install 		// to install npm locally so you can start the project
     -npm start			// to start the project

    While

    If you still get errors, or unable to run, you might need to install the buget packages manually
    AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1"
    Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.9"
    Microsoft.AspNetCore.Identity" Version="2.2.0"
    Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="7.0.9"
    Microsoft.AspNetCore.OpenApi" Version="7.0.5"
    Microsoft.EntityFrameworkCore" Version="7.0.9"
    Microsoft.EntityFrameworkCore.Sqlite" Version="7.0.9"
    Microsoft.EntityFrameworkCore.Tools" Version="7.0.9

Following tutorials on "dotnet web api" or "dotnet core mvc" should let you get a good start
As an example: https://www.youtube.com/watch?v=Fbf_ua2t6v4&t=97s

<!--
https://www.notion.so/07f326a24db34eec8f9f7bea2c7f22b4?v=6a8d9729ff0a46a48758fbc489275087&p=d171fd63d9bd4e10b7bf631023d0f7f0&pm=s





























vid 20
[20220518_130639]
[06:21] installing router with -npm install react-router-dom	//document object model.
 [21:30] 	///this is to be able to navigate to a new page. [21:30] importing it in App.jx
		import { BrowserRouter as Router, Route, Routes} from 'react-router-dom';
	26:00 explanation of Routes
[28:30]// coding in Home.jsx -it's the homepage.  Introducing <> JSX fragment or React.Fragment.	Note. return ()  you need brackets if you are using more than one element.
//In App.js, we are including different pages with Router, routes and route [38:16]
31:00 TIP "If you have many things in the return statment, use a ( ) bracket"
	Also, you can use <> </>  JSX Fragment
 35:00 creating different paths or links. with <Routes>
	<Routes>
          <Route path="/" element={<Home />} />
          <Route path="/courseList" element={<CoursesList />} />
        </Routes>
										//Using functions
										function VehicleList() {
											const vehiclesRegNo = [
											{regNo: '66'}, {regNo: '61'}, {regNo: '32'},	//is this an array of string? or objects?
											];
											return(
												<table>...</table>
											);
										}

										//We are assinging the data (regNo) to a new const newList
										const newList = vehiclesRegNo.map((objectsOrX) => {
											return objectsOrX;	//newList will become an array of objects if you return the whole thing. It's dynamic
										});						// if you return objectsOrX.RegNo then it will become a list of string.

										//Example 2	Here we instantiate a new type with a property vehicleItemProperty to hold all the things inside vehiclesRegNo.
										{vehiclesRegNo.map( (propName) => (
											<VehicleItem vehicleItemProperty={propName} Key={propName.regNo}/>
											)
										)}
										vehiclesRegNo.map // vehicles has a lot of car objects. vehiclesRegNo is
													 // an array of objects. map is an advanced for loop
													 // that loops through the entire list.
										variableName 	//... accessing what's inside, so it's a property

										VehicleItem vehicleItemProperty	// here we create a new instance of VehicleItem.
															// "egenskapen" is what he calls VehicleItemProperty.
															// is vehicleItemProperty the name? No, It's a Dynamic property.
															// Which we can use as argument else where.

										[40:43]//Example 3 A function that recieves VehicleItemProperty [40:43]
										function VehicleItem({ vehicleItemProperty }) {
											return(
											<tr>
											<td>{vehicleItemProperty.regNo}</td>
											</tr>
										);?}
										//OR we can use props to access everything without knowing what's in it.
										function VehicleItem(props) {	// props can be used to get EVERYTHING in VehicleItem.
											console.log(props);			// Or we can use function VehicleItem(vehicleItemProperty)
										}
										////////////////[1:46:00] How to use a function that creates an action on click in a page777777777777
										const onEditClickHandler = () => {
											console.log(`ska uppdatera bilen ${vehicle.regNo}`)	//'' and ´´ is different or `` shift click
										};
											//and you place the other part on a html element
										<span onClick={onEditClickHandler}>	//if you place {onEditClickHandler()} the bracket () means do it all the time.

										////////////////////////////////////////////////////////////////
42:00 Building Navbar. //It should be placed below <Router> and above <main>
		46:00"Don't bother typing the li and u tags. We''' be removing them"
!NOTE!//he uses id='navbar' and className="text-primary"
			id's for bigger parts/sections and className for miscellaneous
37:40 Lär dig react ORDENTLIGT -Michael Gustavsson
[41:00]Creating Navbar(){}	//theres a wrong way to do it (without using import { NavLink }
	///With this we place two pages. A Start sida and lager fordon, which shows list of cars.
[1:23:00]//CReating AddVehicle(){} [1:23:00]. With the form tags filled in AddVehicle.jsx, we include the route -link to the new page in App.jsx
	///and add the button in the navbar to the new page.
1:28:00 FORM Creating Put method.

const [useCourseNum, setCourseNum] = useState('');	//<--

  const onHandlerCourseNumTextChange = (e) => {
    console.log('texten är ändrad');
    console.log(e.target.value);
    setCourseNum(e.target.value);	//
  };

	<div className="form-control">
              <label htmlFor="">Course Number</label>
              <input
                onChange={onHandlerCourseNumTextChange}
                value={useCourseNum}	//<--
                type="text"
                id="courseNumber"
                name="courseNumber"
		/>
	</div>

									[2:05:20]/////////////////// Sending vehicles to database/////////////
								const saveVehicle = async (vehicle) => {		//the above function didn't have anything inside the ()
									const url = `${process.env.REACT_APP_BASEURL}/vehicles`; //changing the link
									const response = await fetch(url, {
										method: 'POST',
										headers: {
											'Content-Type':'application/json',
										},
										body: JSON.stringify(vehicle),
									});
									console.log(response);

									if(!response.status >= 200 && response.status <= 299){
									console.log('Bilen är sparad');
									console.log(await response.json());
									} else {
										console.log('Det gick fel någonstans');
										console.log(await response.json());
									}
								};		//you do use semi colon here...


  //In the console, we see "SyntheticBaseEvent" -"Det är häftiga saker detta" //I need to think in this way when I see complex functions.
	//"Det man är ute efter är " target: input#regNo
							<NavLink to='/add'>Lägg till</NavLink>
						[1:33:0 ]//Data bindning	[1:33:0 ] First we build for "Registreringsnummer". Now we build the remaining [1:47:30]

							<input value={regNo} type='text' id='regNo' name='regNo' />	//we want to get a input value={regNo} and we want to bind it.
							<input onChange={onHandleRegNoTextChanged} value={regNo} type='text' id='regNo' name='regNo' />	//onChange={onHandleRegNoTextChanged} -this tracks what's being changed in the textbox??

							//[1:33:30] we need to import useState and define addVehcile function
							import { useState } from 'react';

							function AddVehicle() {
								const [regNo, setRegNo] = useState('');		//this makes regNo be accpeted at the value={regNo}

								let vehicle = {			//we are creating a vehicle object
									regNo: regNo		//since both have the same name, you can simply have it as regNo.
								}

								const onHandleRegNoTextChanged = (e) => {
									console.log("Text är ändrar")
									console.log(e.target.value); ///the console.log just displays it for us to see in the debugeer
									setRegNo(e.target.value);	//this records what you type in the form.
								}

								const handleSaveVehicle = (e) => {
									e.preventDefault();		//what does this do????

									console.log(vehicle);
								}

								return (
								<>
								<label htmlFor=''>Registreringsnummer</label>
								<input
								onChange = {onHandleRegNoTextChanged}
								value={regNo}
								type='text'
								id='regNo'
								name='regNo'
								/>
								);
							}
					//Adding the rest of the properties [1:50:27]
					[2:00:00]//Adding an img DEFAULT item to function AddVehicle().. [2:00:00]
					[2:03:26] [2:05:20] // saveVehicle(vehicle) => { }		//to database [2:03:26] [2:05:20] there is code below


					[2:04:00]//Loading a list of vehicles through Get method [http {"list"}] [2:04:00]
					const loadVehicle = async () => {
						const url = `${process.env.REACT_APP_BASEURL}/vehicles/list`; //we are using back ticks ``
						const response = await fetch(url);

						if(!response.ok){
							console.log('Hittade inga bilar, eller så gick något fel');
						}
						setVehicles(await response.json());
					}

2:11:00 Method for sending data to the database.
	Also where to see the incoming data in the console.
Error -I am pasting the wrong address.. Not sure if it needs to be get all vehicles or link for the post method...
						//we returned empty console.log(await response.json()); which gave an error.
						"Find out why after the break!"[2:15:00]
2:16:00 	We will not learn about how to set requirements on what to properties to have when sending a Post request.
			And those information will be found in the API documentation.
	ERROR I had an 404 error and not sure what I changed, but seems to work!?

	const saveCourse = async (course) => {
    const url = `${process.env.REACT_APP_BASEURL}/courses/AddCourse`;
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(course),
    });
	---------------------------
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

										[2:19:30] Quick explanation on how the methods are connected. saveVehicle and the above. [2:19:30]
										2:31:43//Edit vehicles 2:31:43		process{a) create a EditVehicle.jsx file. You'll have funtions there and then export it.
																			b) in App.jsx, you'll import it and add the <Route path='/edit/:id' element={<EditVehicle />} />}
2:33:00 So far the Routes are looking like this
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
										2:35:00//  import { useNavigate } from 'react-router-dom'; //we use this to navigate "kod mässigt?"
	function onEditClickHandler() {
		navigate(`/editCourse/${course.courseId}`);
	  }

2:39:00 Using incoming id to EDIT a object PUT method
						import { useParams } from 'react-router-dom';
2:42:00	initiate it :   const params = useParams();

	useEffect(() => {
		fetchCourse(params.id);
	}, [params.id]);

	const fetchCourse = async (id) => {
    const url = `${process.env.REACT_APP_BASEURL}/courses/${id}`;		//we use the id that is from params
    const response = await fetch(url);

    if (!response.ok) {
      console.log("Couldn't find course, or something went wrong...");
    }

	const course = await response.json(); //<-- here we have the entire object, with id...
    console.log(course);
    setCourseId(course.courseId);
    setCourseNum(course.courseNumber);
    setCourseName(course.name);
    setDuration(course.duration);
    setDescription(course.description);
    setDetails(course.details);
  };
										2:55:00		//	making the Put fucntion and the save function
										[3:10:00]//Adding extra steps to hide or veiw data [3:10:00] Adding an ResponseVeiwModel in Vehicles-API,
											//creating JsonSerializer in [HttpGet("list")] method
										2:20:00?//Documentation for swagger 2:20:00? [ProducesResponseType(StatusCodes.Status200OK)]
										2:33:00//<PropertyGroup> settings 2:33:00
2:53:00 Putting together two properties or Joining them... ex: vehicleName: 'Volvo XC90'
			setMake(vehicle.vehicleName.split(' ')[0]);
			setModel(vehicle.vehicleName.split(' ')[0]);

										2:53:00// Om Async await. tre olika sätt att kommunicera.
2:55:00 Micheal shows the API endpoints.. not entirely :'(
2:58:... We see how to use id to send the request. But it's a bit confusing.
	After getting the object. A) we set it in useState.
	B) we use it in value={useCourseId} in the html
			<input
              onChange={onHandlerCourseIdTextChange}
              value={useCourseId}
              type="hidden"
              id="courseId"
              name="courseId"
            />

	C) when onChange={onHandlerCourseIdTextChange}, if change happens, we store that in useState
		const onHandlerCourseIdTextChange = (e) => {
			setCourseId(e.target.value);
		};

	D) Finally, when save btn is pressed, we send the data
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
			saveCourse(course);  //Ans: We don't need to send the object with id...
		};

		const saveCourse = async (course) => {
			const url = `${process.env.REACT_APP_BASEURL}/courses/ReplaceCourse/${useCourseId}`;	//It's so easy to make error with the api link..
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

										3:04:00//MicroServices 3:04:00. Kuberneties is a deligating service/program that does the smart architect for you
3:14:00 showing the right way to have backend code in Node js?
	It needs to be a ResponseViewModel that is being returned
3:26:00  I need many to many relationship between users and courses they are studying..
- you should choose a category, choose a course, look at the detail, then register to the cours
									3:27:00//Talking to external API 3:27:00







vid 19
[20220518_091607 09:50] React Router
05:00 css, shows what changes he's made
// ESLint. [17:00] Helps you with javascript coding. [21:00] Repetition
	///Font awesome is mentioned to bring fonts.
	//More explanation regarding how Javascript works [36:30]
		"You can use props instead of a specific {object?}" with curly bracers, you break down and choose specific object
// Adding a Component Folder [50:45]
[45:37] How to DEBUG with the browser
1:15:00 creating the navbar
// [1:15:30] Moving the css files from Public folder to src Folder (change the script or code from index in Public to App.js in src Folder
1:21:00 right click the reload icon on the browser to empty cashe
// [1:23:30] adding edit and delete logo in the VehicleItem.jsx
// Händelse hantering. ie edit and delete logo, adding functions..
1:25:00 place the fontawesome in public>index.html //where the "root" id is.
// [1:48:00] USing an API!
	/// starting the API with -dotnet run
	///You might have to change the port. Go into Properties and launchSetting.json
1:33:00 cloning the react project from github.
1:40:00 onClick event. On edit method

Getting the api endpoints |Life cycle hook event
1:49:00	"IF YOU HAVE PORT PROBLEMS" in launchSettings.json
	/// change the port to something else. 7247/7237 and 5246/5146?  <- This is how we let the react get data from mvc backend project
[1:51:00]//  Building function to bring the data //LOADING in the API data
	// we need a useEffect funtion to use the incoming url BUT YOU will get an error
2:01:00 "We get an error" Failed to fetch...

2:01:00] We add the JS port to the .net API by adding it in the Program.cs
builder.Services.AddCors(options => {
	options.AddPolicy("WestcoastCors", policy => {
		policy.AllowAnyHeader(); policy.AllowAnyMethod();
		policy.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:3002")
	})
})
"Spent MANY hours on this error. My api didn't have an https, it just had an http. So the link I was calling from the react was wrong. "
"answer was given in 2:36:00
// Changing the link from hardcoding it to moving it to a proper place
[2:08:30]"Placing it in .env" inside the root folder.  with REACT_APP_BASEURL

		const url = `${process.env.REACT_APP_BASEURL}/vehicles/list`;
		const reponse = await fetch(url);
				//Dont forget to restart your React app
// the useEffect() DANGER. [2:27:20]
	///Don't fully understand how the flow of this works. But he explains during the end of the video a few times.
		"needs two function, the second function does a update-method."
		///If you see just square breackets, it means it's expecting an array
2:24:00 using useState

vid 18 0809
[20220517_130531]
[41:32] We are convinced that react is the shit. And it starts here
53:00 installing node
1:29:00 installing react app
/REACT
		///	-npx create-react-app .		"The dot . meanns make the project inside the folder called react-app? YES
		/// -npm install 		//You need to have node_modules in your project. BUT I DID. Couldn't get the website to launch without it
		///	NOT NEEDED UNLESS...-npm i -g npx 	"-g means to install the name npx" i means..
		///	-npm start
		///
		/// Had an issue with "npm" not working(windows) ERROR: global, local deprecated...https://github.com/npm/cli/issues/4980
			///solved it by following the link above.
// FInd jobs close to you and see what they need <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
>rm -rf nameOfTheFolder		//this deletes the folder
    >mkdir react-app
    >npx create-react-app .

1:41:00 Explanations for what all files do
	package.json is aslo called npm package file.
[1:43 40]	Two extensions to help with code. Jest and Jasmine
[1:53: 20] Extension neede ES7: ES7 React/Redux/GraphQL/React-Native snippets
	//Jasmine - Behavior-Driven JavaScript
[1:55:00]// after deleting things, we are coding in src Folder, in index.js
2:00:00	babeljs.io//Babel - tranforms JS to "real" js? Turn JS to another format, like scriptJS
// We create a new file called App.jsx, there we are the html things,
	// where we export and then import into index.js in the same src Folder.

2:16:00 Learning how to create a component that can be used inside App.jsx
2:20:00 A Module has multiple components.
2:35:00 Had an issue with a component cause I imported wrong.
[2:41:00]// Creating CSS . //Making things dynamic, as in using properties
[2:51:00]// from data?  // [2:58:35] Placing in huge data and calling
3:00:00 used a array and used them in the component with .map function.
3:04:00 But instead, we would like to send the whole array!
3:10.00 Destructuring... {}//don't FORGET to add the curly bracers
		Instead of porps, if you know whats coming in, you can call it by its name {don't FORGET to add the curly bracers}so you can skip using "props"

		function CoursesItem(props) {
  console.log(props);
  return (
    <tr>
      <td>{props.course.CourseNumber}</td>
      <td>{props.course.Name}</td>
      <td>{props.course.Duration}</td>
      <td>Things</td>
    </tr>
  );
}

export default CoursesItem;




[3:11:50]	//it in VehicleList.jsx  // Short summary
3:11:00 How components work. Is it better to use .map on a parent component instead of sending it down?
	A. We create a simple, empty component (<Vehicle_list />), in there there are tons of code and a head and body table that will show a list of vehicles. We'll have a simple component to display the repetetive vehicle list.
    //Since I don't care much for mastering programming with code, I should be able to maintain it for a job, its the entry to IT. What will I move to? Writing? managing people? Manager! A guy from Uppasala university named it as soon as he heard me explain what I like. Managers are quite dumb?
    https://github.com/MichaelGustavsson?tab=repositories



vid 17
[1:56:00][20220517_090026]	Razor pages  Creating Add car function

vid 16
[20220512_090134] Starts by talking about the project, maybe continues with the jsAPp and Razor pages starts at [2:31:00]
 - js the definitive guide 7th edition
 learning javascript Ethan Brown, JAva script design patterns Addy Osmani

vid 15
[20220511_125700] search functions and more. AT
19:00 creating HttpPost method
//NOTE //don't use model as a variable name "model" since in MVC it is being used as a ley word
36:00 //you don't need to add asp-controller=" " if you are getting it from the same somethingsomething..
42:00 using a tag in the view model to alter the name of the property so you can have a unique/diff name for display. RegNo can be turned to RegistrationNr on the web
		[Display(Name = RegistrationNr)]
1:31:00 sudo class? css based on the condition of an element. ex hover
			.btn:hover {
			  background: #888;
			  color: #333;
			  font-weight: bold;
			}
1:41:00 TIP. Before you complete the whole method, you can create how it will look like when completed, before building the method
	 [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCourseViewModel course)   //don't use model as a variable name since in MVC there is key word "model" is being used
        {
            if (!ModelState.IsValid)
            {
                return View("Create", course);
            }
            return View("CourseSavedConfirmation");
        }
2:08:00 completed post method. Starting of how to creat a global css variable for color.
2:16:00 moving css to another file.
2:27:00 We start the JS app.!!
2:31:0 install live server

vid 14
20220511_100737 --------------------------------
We are creating the Details page. 18:00	You need [HttpGet("Details/{id}")] above the method.
"TIP Before building the method or JavaScript, try testing if you can reach the site!!!"
27:20 slice(0, -1) javaScript method where you take a string and choose which part you want to keep and which to remove.
			0 means starting from 0 index, and -1 is removing the last index.
45:00	Working on Details method.
49:00  GOt an error:	Operator '??' cannot be applied to operands of type 'Task<CourseViewModel?>' and 'CourseViewModel'
		//Due to course variable missing a await...
		method for detail method.
1:13:00 css for detail page

[1:38:30] Needing Json serializer settings so program can read incoming data
[1:55:00] We create a model class VehicleServiceModel where we put in the hosting link and Json serializer through constructor so we can call it in the methods.
[2:23:50] PRESENTATION on MVc model? The timestamp will show you what Action methods can return
[2:35:00] Setting up app.MapCOntrollerRoute in Program.cs to automatically route to a certain place?

vid 13
20220510_130556 Continuing css----------------------------------------------
	17:30	aspect ratio calculator. working with img is hard in webapplication. //Tip: Get pictures that are 2000-5000 px big.
    23:00 WHich picture format to use? If its a foto: jpg, a drawn thingy: png, Else svg?
		33:00 never re-use id on the same page twice id="navbar"
35:00 Using javascript to have a pop up effect when you click on a container
	 &nbsp; non breaking space?`45:25
1:26:00 still building the pop up effect on items. 1:31:30 Explanation. Error with JS, needed to remove a line below at line 31.
1:49:00 Css for the pop up effect
		2:34:10 creating a button
	3:00:00 Moving css code to another file, and using @RenderSection("styles", false) in the @ _Layout.cshtml
		"similarly for scripts"
3:12:00 how to place a javascript file in the right index file.

vid 12
[20220510_090125]
05:30 ish, in app.development.json, we can change the port number where the project starts.
29:00	To be able to run the debugger with both projects in the main folder, you need to remove .vscode folder everywhere
except the main project folder.
In the launch.json folder. We are changing
			"program": "${workspaceFolder}/WCC-API/bin/Debug/net6.0/WCC-API.dll", to
			"program": "${workspaceFolder}/Clients/MvcApp/bin/Debug/net6.0/MvcApp.dll",
					In "env": we'll add another line, so it looks like this
					"env": {"ASPNETCORE_ENVIRONMENT": "Development",
							"ASPNETCORE_URLS": "https://localhost:5000"}
	We also add another element for API project, where there is two difference.
	  "program": "${workspaceFolder}/WCC-API/bin/Debug/net6.0/WCC-API.dll", and
	  "ASPNETCORE_URLS": "https://localhost:5001"							Look at 35:30

In the task.json folder. We are changing         "${workspaceFolder}/WCC-API/WCC-API.csproj", to "${workspaceFolder}",
													So removing the path and leaving the root folder.

-------------"This way you can run different projects together in the debug mode"--------------
A bit confusing, but we are changing the element with API to 5001 and changing the development.json in the MVC-APP to 5001...
49:00 In MvcApp, in appsettings.Development.json, you change the port to 5001 when you want to debug, otherwise have the default or where the web site is run on.
"YOU CAN have the same ip port for debug, then you need to make sure you're not running the program while debugging..."

52:30 NOTE: Run the api first (debugging), then the client
50:12 To know which port number you need to have in MVCapp, run the API project with dotnet watch run, and use that port

"LOAD DATA"
56:00	If you want to add in data automatically, you can follow here.
	///After filling in the code 1:06:00
	"dotnet ef database drop --force" and "dotnet ef database update"

1:39:00 ViewBag is dynamic and ViewData is like a dictionary?
//----------------------HTML and CSS----------------------// 1:36:00
1:49:00 creating nav bar. //obs! navbar needs to be id="navbar" and not class...1:52:10
	ul>li*4>a creates a list of a tags
1:55:00 	rem means relative to default value, 3 rem is 3* default
2:05:00 CSS babyyyyyy
2:10:00	We used fonts from Font-Awesome ///step 1 Choose a logo https://fontawesome.com/search?s=solid%2Cbrands
	///step 2.https://cdnjs.com/libraries Search for Font-awesome
	"How to place a logo, do the above steps"
2:19:00 Tip! Keep your css organised. Have your elements up, and classes below.
2:32:00 Creating a link to Våra bilar
2:42:00 Why isn't the image link working?
2:43:00	//Building the Course list page, trying to add img to the list.
2:58:00 //Fixing gallery-wrapper. with display: grid; grid-template-columns: repeat(4, 1fr);

vid 11
[20220505_130015 30:00]
	//We are building a MVC model and not Razor pages, so we deleted the css file, js file and erase everything except the @RenderBody() in the sharedLayout page.
	https://fonts.google.com/specimen/Poppins?query=poppins
	06:06 adding fonts to the library (Poppins and Roboto are simple)
	10:30 Adding the css styling to the _Layout.cshtml file
	<link rel="stylesheet" href="~/css/styles.css">
	19:40 //We have a controller(Vehicles), in it a method called index(). We create a View-file that will run the Index() method
	"So that file will be called Index.cshtml". 23:00. "We create a new Razor page file but delete the razorpage code and controller extention"
			"-since we are using a MVC project." With CS-code, we don't have any handholding so we have to create all the files ourselves.
	[32:00]//with @ _Layout.cshtml (the main page) we can add the different tabs in the page
	33:00 adding a nav tab with a link a-tag in _Layout.cshtml
	                <li><a asp-controller="Courses" asp-action="Index">Courses</a></li>

	"short hand for createing elements" ul>li>a "will create unordered list, a list and inside a link"
	[35:00]//HttpClient(); [35:00] Using Tag helper in the cshtml file to conntect to the controller.
	asp-controller="Vehicles" asp-action="Index"

Connecting to the API 40:00
	///We add our Get-method [41:19]
	[51:38]using var http = new HttpClient();
	var response = await http.GetAsync(url);
	///url is "https://localhost:####/api/v1/vehicles/list
	///Talk about Garbage collector
	57:39 "Under the map .vscode > launch.json > you can change in which order things are run"
	1:01:00 "How to debug and look at what's coming in"
//We run into an error when we try Debugger
	We solve it by running the api and the mvcApp in different VS-code. Due to mapstructure mechanics,
	the debugger runs everything at once. 58:00
	"You can run the debugger .NET Core Attach but you need to type something to make it work, its an extension maybe?" 1:26:00
1:20:00 Back from break. HTTPS development certificate
	If you don't have this, you can run the Terminal as Admin and type
	-dotnet dev-certs https --trust
//Second debugger run 1:30:00
1:31:00 //Creating a View Model 1:31:00 to take in the data thats coming in.
	The data thats coming in is screwed and since it doesn't match our viewModel properties, we need to fix it. 1:37:30
1:45:00 "Model folder in MvcApp is for classes which has methods that talks to the REST api"1:45:00
		Is that affärs logik, in the presentation picture?
///Instead of using in Courses Controller
	var options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
								///You can use in the CoursesViewModel.cs, but it will clutter the file. 1:48:300
								[JsonPropertyName("CoursesId")]	//so we don't use this

1:54:00 using VehicleServiceModel to JsonSerializerOptions and returning the base url for the method.
1:52:00 //Moving the logic above to the right place, which is the Model folder. 1:52:00
1:55:00 //creating baseUrl in appsettings.Development.json so it can be reused. 1:55:00
	_baseUrl = $"{_config.GetValue<string>("baseUrl")}/course";

//Using Repository Pattern by creating a CourseFunctionsModel file under Model folder, and having the functions
that has to do with talking to the API in there. 2:00:00 around here

			//Theory.
				///Michael is showing 2:30:000 how you can send ViewData from the Controller [ ] This could be worth experimenting
			In controller: ViewBag.Message = "Passa på at köpa...";  In View file: <div>Dagens meddelande är: @ViewBag.Message</div>
			2:35:00///In Program.cs you adjust how the routing is done.
				app.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");

					/// You can use HTTP tag above the methods or Class file where you change the name of the default route.
					[Route("[controller]")] above the class CourseController, can be changed.
					"If you dont have any Http tag above a method, by default it's [HttpGet]"
			Razor notes @ symbol can be used in different ways. 2:45:00
	//CoursesController
		public async Task<IActionResult> Index()
			{
				try
				{
					// ViewData["CourseID"] = "A million ID's";
					var courseService = new CourseServiceModel(_config);
					var courses = await courseService.ListAllCourse();
					return View(courses);
				}
				catch (System.Exception)
				{
					throw;
				}
			}

	//Courses>Index.cshtml
			@model IEnumerable<MvcApp.ViewModels.CourseViewModel>

			@{
				ViewData["Title"] = "Available Courses";
			}
			<h1>@ViewData["Title"]</h1>

			<article>
				<section>
					@* <div>Hey @ViewData["CourseID"]</div> //this is commented out *@
					<h3>Check our available courses</h3>
					@foreach (var course in Model){
						<div>
							<a asp-controller="Courses" asp-action="Details">@course.CourseNumber @course.Name</a>
							<p>@course.Duration</p>
							<p>@course.Description</p>
						</div>
					}
				</section>
			</article>

2:58:00	//We are adding a @ tag in the View file to tell where the data is coming from
	@model IEnumerable<MvcApp.ViewModels.CourseViewModel>
	///We fill in the view file with @ tag helpers to bring in the data. I don't fully follow here. [ ]
//We need to have HTTP tags above the methods to diffrentiate them. ??? asp-action="Details" didn't help...
		<a asp-controller="Courses" asp-action="Details">@course.Title</a>
		///asp-controller="Courses" means it will look at CoursesController. ///You don't need to type COntroller.
		///asp-action will search for the method name, BUT why didn't it work?
3:09:0	with [HttpGet("{id})], the methods stay unique, otherwise they are the same (ERROR AmbiguousMatchException)
3:12:00 //BAD practice. You must send in the return View("what the method name is", object)


vid 10
20220505_090110 MVC PROJECT
"Links"
app.diagrams.net 	For building diagrams. //under software, you will find database diagrams
namecheap.com 		"Domain names, "
netify.com - where you can host your website (react or html/css)

[ 1:21:30]	Theory starts on WEB
		Utvecklingsverktyg -1:22:10
		Hosting - 1:28:19	Exekveringsmodeller??(client or serverside)
		MVC design mönster 2:00:00

"mest vanligaste design mönster idag?" 	In ASP,pure JS = it's MVC.  React/Single page app = MVVM

//MVC asp code starts at [2:19:30??]
-dotnet new mvc -n MvcApp

-dotnet sln add Clients/MvcApp/		"Connecting the project with the MVC. OBS be on the parent folder"
//Client is a new folder that you can create with -mkdir Foldername
	"för att bli en duktig utvecklar måste man förstå, inte som de googlande utvecklare"
//Deletes folders and content from js and css [2:49:00]. At [2:59] he adds the Html: 5 semantic? in the _Layout.cshtml
	@RenderBody()
2:43:00 How to publish 	>dotnet publish
2:49:00 Start deleting unwanted files
2:56:00 editing _Layout.cshtml
	use autocomplete "html5" to auto complete html template.

Vid 9
20220505_090110 	There was discussion on [authenication] tag not working.
		discussion on what framework is good
	04:00 He mentions working with cookies is easier. I NEED A TUTOR, How do they learn all this
	40:00 we are git-cloning ITHS-STHLM-Westcoast-Cars-Starter

	43:00 Display of whats in the Auth controller from the project!!
	48:30 < ls -al // shows all the files in the folder
			< rm -rf .git 	//removes the git file. After this he creats a new git
							< git init, git add ., git commit -m "init"
			Disscussion on droping the database when creating new coloumn. You don't need to. Just set it to null when it's deleted.
			Unless when we added manufacturers.
	51:40 app.diagrams.net
	1:21:00	Presentation. WHat are the tools, hosting sites, where there api's end up?
			cheap hosting namecheap.com, app.netify.com?
	2:17:00 starting the web?
	2:20:00 dotnet new mvc -n... starts creating the project
	2:22:25 starts coding. Installs: > sln add. Clients/MvcApp/  ...// adding a mvcapp?

Vid 8
20220504 13...
		05:00 UsermManager<IdentityUser> is needed
		//Constructor will look like this
		public AuthController(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }
		00:09:30 creating a Post method [HttpPost("register")] to register a new user...
        23:00 checking user password
		34:00 Refactoring CreateJwtToken
		39:00 registering a user acction that can login.
		1:22:00 added claim IsAdmin in RegisterUserViewModel
		1:25:00 user, new claim("Admin", "true"));
		2:30:00 Use Roles
			add RoleManager<IdentityRole> in AuthController as dependency injection


 Vid 7
20220504_morning 12:00 If your getting Michaels project from github, do a >>dotnet restore .Due to bin and obj being ignored by his github, which you need.

		25:00 var claims = new List<Claim>{
					new Claim(ClaimTypes.Name, userName),
					new Claim("XYZ", "Value")
				};
		41:18 how to protect your endpoints with[Authorize]
		46:00 How to set up shortcut on URL in postman (New Environment)
		51:00 Pipeline, fixing middlewear
		59:00 adding "app.UseAuthentication();" in program.cs
		1:25:00 configure authentication in pogram.cs
			//Authentication configuration
			builder.Services.AddAuthentication(options =>
			{
				//defaultAuthenticationScheme and DefaultChallengeScheme
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.ASCII.GetBytes("Kasdf kje+dsg ksajf 98u4tlxc vfcdsjfg498a lmöasdflkerp")
					),
					ValidateLifetime = true,
					ValidateAudience = false,
					ValidateIssuer = false,
					ClockSkew = TimeSpan.Zero
				};
			});

		1:39:00 Moving the key into appsettings
		1:44:00 pasting the auth token into the auth tab in Postman... You first need to run the login method to get the auth token, now you use it on other places where it is needed.
		1:47:00 [authorize(policy: "admin")] - to restrict who has asscess to the methods
        	You define these policies in program.cs
			//Configure and create policies
			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("Admins", policy => policy.RequireClaim("Admin"));
			});
        2:00:00 new information on the token.
        2:16:00 when using auth token, use Bearer token (under Auth tab)in Postman
		2:20:00 downloading Nuget aspnetcore.identity, JwrBearer, FrameworkCore, Sqlite, Tools,
			Microsoft.AspNetCore.Identity by Microsoft
			Microsoft.AspNetCore.Identity.EntityFrameworkCore by Microsoft
			Microsoft.EntityFrameworkCore by Microsoft
			Microsoft.EntityFrameworkCore.Tools by Microsoft
			Microsoft.EntityFrameworkCore.Sqlite by Microsoft


        2:27:00 building the ApplicationContext (Db connection)
		2:34:00 configuring middleware for Identity	in program.cs

        2:45:00 settings for password, etc..

Vid 6
220503 13.. more . Around 2:30:0 we start with security. he creates a security demo, which I'm not sure if its connected to the project
		we start with building methods for the repo class.

		04:00 Question. In Category/Manu..Repo We have a SaveAllAsync(). code> return await _context.SaveChangesAsync() > 0;
		09:00 for controller to get in repo class with its own context manupilation, we need dependency injection.
		builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>(); //for the sake of getting a class that is instantiated.
		17:00 fixing automapper for Manufacturer. There was a mistake.<!!>// hot reload doesn't work with program.cs or Automapper.
		HOW TO add relationally connected database
		26:00 creating AddVehicleAsync WITH manufacturer(connecting table) //manufacturers is the parent table.

			public async Task AddVehicleAsync(PostVehicleViewModel model){
				//we need the right manufacturer (name == model.Make)
				var make = _context.Manufacturers.Include(c => c.Vehicles).Where(c => c.Name!.ToLower() == model.Make!.ToLower())
				.SingleOrDefualtAsync();

				if (make is null)
					throw new Exception($"Tyvärr vi har inte teillverkaren {model.Make} i systemet.");

				"converting to vehicle from postvechleveiwModel"
				var vehcileToAdd = _mapper.Map<Vehicle>(model);

				"Now we are in the vehicle class/table"
				vehicleToAdd.Manufacturer = make; //make is the correct manufacturer.
				"adding the vehicle to the database. " // vehicle is the child. 		Should there already exists a manufacturer to be able to add a vehicle? A. YES
				await _context.Vehicles.AddAsync(vehicleToAdd);
			}
				So you need to do two things. Add .manufacturer to the vehicle and add the vehicle to the db
				Or three things. Find the right manufacturer, add the manufacturer to the vehicle, add the vehicle to the db.
										"This is not a connection table. it is 1:many"
				//NOTE: Manufacturer(parent) has Icollection<Vehicle> Vehicle {get; set;} = new List<User>();
				// 			User has	public int ManufacID{get;set;}, [ForeignKey("ManufacID")], public Manufac Manufac {get; set;} = new Manufac();

		40:00 Important to do saveall before the request leaves the endpoint.
		44:00 Business rules. Besiness demands certain mechanics.  ex: we don't allow products from this company. Then it's vital that developers knows this in and out.
		1:36:00 Changing the link (bymake/{make})] to ("{id}/vehicles") 1:43:00 and using [FromQuery] in the argument (1:39:50)
		1:54:00 Getting all vehicles belonging to a manufacturer.
			public async Task<List<ManufacturerWithVehiclesViewModel>> ListManufacturersVehicles(){
				return await _context.Manufacturers.Include(c => c.Vehicles)
				.Select(m => new ManufacturerWithVehiclesViewModel
					{
						Manufactror = m.Id,
						Name = m.Name,
						Vehicles = m.Vehicles.Select(v => new VehicleViewModel{
							VehicleId = v.Id,
							RegNo = v.RegNo,
							...
							...
						}).ToList()
					}).ToListAsync();
			}
			//
		2:00:00 Till now we've made a method that calls all the cars listed in the manufacturer table. //Q. is this similar to getting students in a course? Everyone that's bought the course?
        A. Course is not a parent to user. A user can exist without being assigned to the course.
			//What to use singleOrDefault, SingleAsync? SingleOrdefault, if you find a null exception, it doesn't crash OR You need to do null check in the controller. 2:13:00

		2:00:00 POSTMAN tip. How to change the url to a set thing so you don't need to type it multiple times...
        How to get edit a object which is inside another opbject. 2:01:50 //I should find Michaels project on github.
        2:05:50 ListallManufacturersAsync() method

		2:30:00 Presentation on security
		2:45:0 creating a new project > dotnet new webapi -n Step01
		3:0:00 working on a method that accepts a username and pasword. Installing a Nuget for it. System.IdentityModel.Tokens.Jwt by Mic
		and Microsoft.AspNetCore.Authentication.JwtBearer by Microsoft 3:00:00

		3:18:00 jwt.io //to control that your token is valid/or working

Vid 5
220503 more database and other interface stuff..
		05:00 Five step demo by MichealGustavsson on Security..??? Claims Roles
		26:26 Building [HttpPost()] method. Using repo pattern
			We are not returning anything for a post method, so we are adjusting the function name.

		27:00 HATEOS //a standard way to confirm that your actions have been successful. and here is the object you created in the head...
		//	Your GET/POST methods, you don't want to return back an object everytime. Like for post method.
						// There you can use HATEOS
		31:00 Using [Required]
			in data model class, So incoming create object requests doesn't have ex null in CourseNumber...
				if(!ModelState.isValid)... //in case of model number. this can be done in front end.
				if (!ModelState.IsValid) return StatusCode(500, "Invalid model. Model must have Course number");
			or
				[Required(ErrorMessage = "Registreringsnummer är obligatoriskt")]

		35:28 A cooler way to catch error would be to set it in the required annotation. [Required(ErrorMessage = "Registreringsnummer is required)]
		41:00 UpdateCourse PUT method.
				From repo class Michael chooses to use throw exception. 50:00 !!!Since we used try/catch in repo, we can use it again in the controller since we will recieve a exception if it fails!!
		56:20 Changing void to Task in "public async Task DeleteVehicle(int id)" ??? why
		1:20:00  -dotnet --info
		// Läsbarhet är A och O. You can forget things over a weekend due to piled up work
		// Gör koden så enkelt som möjligt. Det ska underhållas av någon som inte är själv.
		1:25:00 building a Patch method
		1:36:00 don't return (in catch (Exception ex)) return StatusCode(500, ex). !!Dont return ex. Instead ex.Message...?
		2:00:00 //Nice to have a method that returns a list
			public async Task<List<VehicleViewModel>> GetVehicleByMakeAsync(string make){
				return await _context.Vehicles
					.Where(c => c.Make!.ToLower() = make.ToLower())
					.ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
					.ToListAsync();
			}
		2:03:00 Fixing the database structure.  To remove repetitive.

			STARTING A NEW CONTROLLER
		2:09:00 Adding a new controller for the manufacturer table. Creating Get methods

		2:13:00 he asks: How to go about to create 1:many conenction in entity framework
				with public ICollection<Course> Courses {get; set;} = newList<Course>();
		2:17:00 how to add ForeignKey. GOt an error due to using System.ComponentModel.DataAnnotations.Schema; was missing so the build didn't run.
			NOTE on Vehicles; public Manufacturer Manufacturer {get; set;} = new Manufacturer();	//ie single
				on Manufactuere; public ICollection<Vehicle> Vehicles {get;set;} = new List<Vehicle>(); //ICollection list!

		2:29:00	-dotnet ef migrations add "added make and vehcile relationship" -o "Data/Migrations"
		2:34:30 -dotnet ef database drop --force // droping the table due to complication in adjusting connetion tables
				-dotnet ef database update //updates the migration files. //sometimes this won't work

		"There was a discussion on tables depending on eachother. Here We made manufacturers. coupled with Vehicles."
			"You can't delete a item in the manufacturing table, if a vehicle is connected to it." "Man kan inte ha föräldarlösa barn."
			2:39:20//On delete: ReferentialAction. Cascade.. which means it will delete everything connected to the...
			You can change it to SetNull or NoAction or Restrict
		2:54:00	Michael Gustavsson is creating controller, Repo with Interface for Manufacturing table.


VID 4
220428 130301 13	githug tutorial by rasmus?
		2:00 //If you're adding strings together. Use Concat or StringBuilder
        	VehicleName = string.Concat(Vehicle.Name, " ", vehicle.Model),
        VSCODE shortcut mark similar variables Ctrl + D while staying on a variable.
		20:00Presentation on Repository Pattern
		39:00? Creating Interface
		<VScode Tip> Shift + alt + down/up arrow will duplicate the line

			public interface ICourseRepository
			{
				public Task<List<Course>> ListAllCoursesAsync();
				public Task<Course> GetCourseAsync(int id);
				public Task<Course> GetCourseAsync(string name);
				public Task AddCourseAsync(Course course);
				public void DeleteCourse(int id);
				public void UpdateCourse(int id);
				public Task<bool> SaveAllAsync();
			}
		41:28 Delete and update methods doesn't use Task so use void		//How the hell do I learn all these things the right way?
		45:40 <Tip> "the method name should have Async in the end, so coders know that the body should have wait/async"

		47:20 A. Creating a repositories folder and implementing the interface here with CourseRepository.cs
			51:44 B. bring in the db context through the constructor
		"Don't forget to include async word in the methods"   !imp To use Async method, you need library; EntityFrameworkCore
		54:00 We are updating the controller file CourseController.cs to use the ICourseRepository, instead of directly contacting the Db context class.

		//Dependency injection for our own classes and interfaces...
		0:56:00 Changing something in program.cs!??? We need a instance of something, so we need to tell the framework this
		We are making a choice of how the users recieve the api
			builder.Services.AddScope/ or AddSingleton/ or AddTransient? "beror på hur instansering ska ske för varje request"
			Singleton - the first request will get the data? But if there are more requests, you will recieve the same(first request) since its in the memory.
						"En instans delas av fler"
			Transient - will create a unique/new instans to each request
			Scoped -	You get a new instance for every new request WHEN IN DOUBT, use scoped. 1:06:00
				//Dependency injection for our own classes and interfaces...
										<Interface, konkret klass som implementerar föregånde interface>...
				builder.Services.AddScoped<ICourseRepository, CourseRepository>();

		//example for adding a list of courses to a new object
		[HttpGet()]
        public async Task<ActionResult<List<CourseViewModel>>> ListAllCourses()
        {
            var response = await _courseRepo.ListAllCoursesAsync();
            //should I translate it to viewmodel here or in the repo?
            var courseList = new List<CourseViewModel>();

            foreach (var course in response)
            {
                courseList.Add(new CourseViewModel
                {
                    Name = course.Name,
                    TeacherCourses = course.TeacherCourses
                });
            }
            return Ok(response);
        }

		1:39:00 building CourseRepository GetCourse by ID. FindAsync is not suitable due to null referense warning. so we are using SingleOrDefault...
			return await _context.Courses.FindAsync(id ?? null); //this is a way to remove the warning.
			instead we use this return await _context.Courses.SingleOrDefaultAsync(c => c.Id == id); //and also add ? like mentioned below
			1:40:00 adding  ? in the return argument public <Task<Course?> GetCourseAsync(int id);
		1:46:00 Adding CourseViewModel abstraction to the repository class. So from context/controller we moved getting response from database to repository class.
			Now we are moving or adding the viewmodel to the repository class. Null checks stays in the context/controller class
		1:47:30 We are using .Where method to find the correct data and create a new instanse to save all the information on it.
			return await _context.Courses.Where(c => c.Id == id)
			.Select(course => new CourseViewModel{
				this = that...
			})
			BUT there is an error //IQueryable<CourseViewModel>' does not contain a definition for 'GetAwaiter' and no accessible extension method 'GetAwaiter'
			in the end you need a }).SingleOrDefaultAsync();
		"THis is what they did before autoMapper..."
		RECAP. IF you want to find the data BUT change it do a different model type, you can use "Where"
		1:53:00 [ApiController] this decorator helps with controlling that the incoming data is not null..
			Regarding try catch controlls in the controller. "Att slänga är en bad practice..." 1:54:30
			"every time you throw, you create an object in the heap/stack. so its better to catch the errors higher up in the program rather than having try/catch everywhere.."
		2:05:00 Deletevehicle repo refactoring and SaveAllAsync NOTE! he doesn't use async in the repo
        2:07:00 return type bool for the method SaveAllAsync. So  Micheal added, > 0 "Q. what does this do?"
			return await _context.SaveChangesAsync() > 0;
		<Tip>.Remove doesnt have async/await //Add method didnt have async before. It showed upfrom a recent update... WHICH MEANS // I need to learn how to follow the updates.

        2:13:00 AutoMapper
			AutoMapper.Extensions.Microsoft.DependencyInjection by Jimmy Bogard
				Create a folder called Helpers, Create class AutoMapperProfiles and inherit : Profile, add using AutoMapper.
				// Map från -> till
					CreateMap<PostCourseViewModel, Course>();
					CreateMap<Course, CourseViewModel>();
		2:21:00 Creating setting for a new dependency injection for automapper.
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

		<Error> Creating a default class for made a protected security instead of public..
			"System.MissingMethodException: No parameterless constructor defined for type 'College_API.Helpers.AutoMapperProfiles'"
			Not i got another error //System.ArgumentException: GenericArguments[0], 'System.Single', on 'T MaxInteger[T](System.Collections.Generic.IEnumerable`1[T])' violates the constraint of type 'T'
<Tip> Ctrl+P can let you search for files in your project. VsCode tip.
		UPDATING THE PACKAGES TO RECENT VERSION MADE THIGNS WORK!!!
		2:28:00					//from -> till (PostVehiceViewModel course), so course is PostVehicle view model
			var courseToAdd = _mapper.Map<Course>(course);	//here we are taking Course(from db) to PostcourseViewModel.
		[HttpGet()]
        //api/v1/course
        public async Task<ActionResult<List<CourseViewModel>>> ListGetCourse()//How do I make changes everywhere? VSCODE command...
        {
            var response = await _courseRepo.ListAllCourseAsync();
            var courseList = _mapper.Map<List<CourseViewModel>>(response);	//<---here. Q. this is confusing. Teacher, Michael has written from CourseViewModel -> till response... But we are getting
			// the entire list through response, and we are converting it to ViewModel... So it should be the other way around...
            return Ok(courseList);											// A. On 2:49:50 he says till and from. So I'm ccorrect.
        }
		2:37:40 "Configuring Automapper"
		CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, options => options.MapFrom(src => string.Concat(src.FirstName, " ", src.LastName)));

		2:46:45 Continuing with Repository pattern. We are moving the automapper to the repository class.
		2:50:00 we used .ProjectTO (needed a using statement), and _mapper.ConfigurationProvider)
			public async Task<CourseViewModel?> GetCourseByIdAsync(int id)
			{
				return await _context.Courses.Where(c => c.Id == id)
				.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
			}
		Robins github https://github.com/robinskoogh

 VID 3
 220428 09 adding more methods, checking with swagger
		//Importance of knowing clean code
		1:40: Creating a Put method. And Discussion on "change tracking"
		<Tip> Använd patch om det är delvis uppdatering. Put för uppdatering av hela objekt.
		00:14:00 Creating GetBy("{property}")
		"SignleOrDefaultAsync" or "FirstOrDefault", what's the difference?
			single when there is only one(but if there's more, you will get all them).
			First finds the first one.
			var response = await _context.Courses.SingleOrDefaultAsync( c => c.CourseNumber.ToLower() == courseNumber.ToLower());
		40:00 ViewModels. Your not suppose to be directly manupilating database objects.
			00:46:00 Debugging
		1:24:00 building a GetByID("{id}")
		1:31:00 TiP! //use FindAsync or anything with find if you are searching for a primary key.
					//for anythingelse use Where or singleordefault FirstOrDefault

					//When to use put(WHOLE object) and patch(partical update)
					//when to use singleOrDefault or FirstOrDefault? If you use singleOrDefault and find more than one identical item in the database, then it'll crash...
		1:45:00 PUT method. before this, we complete delete method, before that, get by id...
		2:18:00 //to hide the null warning in your code, use !
				FirstOrDefaultAsync(c => c.CourseName!.ToLower() == courseName.ToLower());

				GIT HELP
				-git stash /*stores away changes*/ -git stash pop, //pops back the changes if your in another branch
				-git rest --hard //resets the branch to a previous commit
		2:22:00 "Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: "
				"College_API.Controllers.CoursesController.GetCourseByName (College-API)"
				//get by regNo, or a string, you get an error. [HttpGet("{courseName}")]
				To solve it, you need to expand the httpGet link. [HttpGet("byCourseName/{courseName}")]
		2:39:00 Creating ViewModels

        I couldn't use dotnet ef migration command on Linux, I needed it installed.
        >>dotnet tool install --global dotnet-ef

        -an example of how to make a new object and passing the values to it from incoming object

        public async Task<ActionResult<PostCourseViewModel>> AddCourse(PostCourseViewModel course){
        	var CourseToAdd = new Course
              {
                  Name = course.Name
              };
            await _context.Courses.AddAsync(CourseToAdd);
            await _context.SaveChangesAsync();
            return StatusCode(201, course);

        https://github.com/kingli6/API-MVC-Lecture/blob/main/Vehicles-API/Controllers/VehiclesController.cs

 VID 2
220427 1259 we create api endpoints and controller
	15:00 talk on how you can disable and enable null warning. In csproj file. <propertyGroup><Nullable>you can disasble it</Nullable>
		Tip // to avoid null warnings, you can set it to = Empty or =""
	23:00 creating our new controller file.
	29:00 returning json! return Ok("{ 'message': 'det funcakr'}");
	32:00 -TIP //If you don't provide the appropriate method, it will take what it can find 32:00
	405 Method Not Allowed. If the methods are decorated with [HttpGet()] "{}"
		OK = 200, NotFound = 404, BadRequest = 400
	<<dotnet watch run>>
	1:00:00 for HttpPut, you return NoContent
	1:28:20 explanation of developer console.
	1:33:00 Installing NUGETS  ctrl + shift + p
		Microsoft.EntityFrameworkCore
		Microsoft.EntityFrameworkCore.Tools
		Microsoft.EntityFrameworkCore.Sqlite
	Tip models can also be called as entities.
	1:41:00 [Key] decorator, if you want to call it something else than Id
	CREATING DATABASE Connection?
	1:49:00	Adding Data folder and created a CourseContext.cs //The coupling between database and its memory
			A)VehicleContext : DbContext	//step 1
	1:52:00	B) public DbSet<Course> Courses => Set<Course>();  Explanation on why intializing this is needed
			"there was a null string warning. ? wasn't right of a object... = new () wasn't allowed to create abstract or interface type of DbSet"
			1:55:00"why you don't want to instantiate through a constructor is due to it being hard to do tests"
			"creating contructor to handle configuration connections "
	2:00:00	c) creating a contructor?
				public VehicleContext(DbContextOptions options) : base(options){}
	2:03:00 D) Setting dependency injection through program.cs
		"Skapar databas koppling. Letting the program know which classContext I'm using, "
		"Which database manager I'm using; Sqlite"
			builder.Services.AddDbContext<CollegeDataContext>(options => options.UseSqlite(""));
	2:08:00 E) Instead of hard coding the ConntectionString for Sqlite, we use appsetting.Dev..json file
			"ConnectionStrings": {
				"Sqlite": "Data Source=westcoastcollege.db"
			}
		E.1) Now we can complete the dependency injection in program.cs
			builder.Services.AddDbContext<CollegeDataContext>(options =>
				options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"))
			);
		//builder.Configuration. lets you get things from appsettings.dev... json file

	2:20:25 Creating migrations
		>> dotnet ef migrations add InitialCreate -o "Data/Migratons"  // The -o is needed to reassign where it is saved.
		"Ni ska alltid ALLTID ska titta på. Vad gör den förnåt!?"
			//if you get error saying that you might have misspelled, you are missing the tool due to changes. you need to download dotnet-ef seperatly
				>> dotnet tool install --global dotnet-ef>> or dotnet tool update --global dotnet-ef
	2:23:40	 >> dotnet ef database update>>
	2:29:00 right click .db file, choose open database. SQLITE Explorer should show up
				//table should be empty when you open Show Table on Courses

	2:34:00 Creating a constructor in CourseController.cs //to be able to save course to database?
		auto generating field through contsructor. to avoid this. keyword. Open settings, search private. Change field to _ 2:38:30
		Also uncheck this, when you search this in settings.
	2:43:00 Defining Get() method with async await Task<ActionResult<List<Course>>>
	2:49:00	Defining a Post() method. returning a Task<ActionResult<Course>>. Using await _context.Course.AddAsync(course);
		and saving all the changes with await _context.SaveChangesAsync();
	2:53:00 After re-running the dotnet. Swagger will show what to expect in it's body.
		2:54:00 Postman configuration. Add key in the Header. //we need to mention in the header that we are sending in application/json
				Content-Type	application/json
		3:02:00 A big NONO . you cant use your model objects in your CRUD method
				You shouldn't have too much code in your controller.

 VID 1
220427 Lots of theory and by the end of it, he starts creating a project..
>> dotnet new sln -n Westcoast-College
>> dotnet new webapi -n College-API, >>
>> dotnet sln add College-API	//stay where the sln file is located
	1:55:00 //connecting your project to the solution file. //I dont know much about this.
	2:07:00 // adding .vscode folder to VSCODE by oppening ctrl+shift+p and typing "generate assets for build and debug"
	-you can <<dotnet build>> and by moving to a folder where there is .proj file, you can run <<dotnet run>>

	Settings for the port is in launchSettings.json file, where you can change incase of port not available
			"applicationURL": "https://localhost:7227;http://localhost:5145",
	-How to exclude files in VS -open settings and type exclude
	2:25:00 git ignore file
	>ls -al //to see all files


^^^^^^^^^^START OF .NET Project Webb utveckling-20220427_090136-Meeting Recording^^^^^^^^





Därför är det också viktigt att ni är beredda att jobba med och hitta lösningar för bland annat följande områden:
Välja databaser och skapa en hållbar arkitektur
Sätta er in i och förbättra redan befintlig kod/lösningar
Screen scraping av webbsidor samt identifikation av data
Hämta data och bearbeta, samt lagra från olika API:er
Deploya lösningarna på DigitalOcean.com
Kunna växla fokus och jobba med olika uppdrag under LIAn (dock ej parallellt)
Kunna skapa lösningar både för front- och backend


YH mer -to help TH with 			60k students from yh
									åtagandeslut för... skolorna
ärande & Lia/utblidare/konsulter


	https://www.tutorialspoint.com/javascript/index.htm
	https://javascript.info/
	https://www.w3schools.com/js/
	https://www.javatpoint.com/javascript-tutorial

	Måste ha böcker om JavaScript
    JavaScript the Definitive Guide 7th Edition (Flanagan, O'Reilly)
    Learning JavaScript (Ethan Brown)
    JavaScript Design Patterns (Addy Osmani)

    C# Pro 9 with .NET 5(Andrew Troelsen + 1) https://www.adlibris.com/se/bok/pro-c-9-with-net-5-9781484269381
    The Definitve Guide to HTML5(Adam Freeman) https://www.adlibris.com/se/bok/the-definitive-guide-to-html5-9781430239604
    JavaScript the Definitive Guide Seventh Edition(David Flanagan) https://www.adlibris.com/se/bok/javascript---the-definitive-guide-9781491952023

    JavaScript https://www.udemy.com/course/the-complete-javascript-course/
    JavaScript https://www.udemy.com/course/modern-javascript-from-the-beginning/
    React.js https://www.udemy.com/course/react-the-complete-guide-incl-redux/
    React.js https://www.udemy.com/course/react-front-to-back-2022/



	/*
		Focus your energy
		gaurd your time
		train your mind
		train your body
		think for yourself
		curate your friends
		curate your environment
		keep your promises
		stay cheerful and constructive
		upgrade the world
	*/

	*/ -->
