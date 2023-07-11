# mvc-react web project

> dotnet restore // Run "dotnet restore" command due to bin and obj folders being ignored in the repo, so you will have everything required to run the project locally.

<!--
Task 5 Know what to start on.

Crud on both courses and users
	-have user list in course? yes? but it doesn't work and I'll solve it later

Find other peoples projects?
	-I can try to find Robin and Milad
	https://github.com/milad055/Schoolprojekt
	https://github.com/robinskoogh/BookStore

Git hub skill where i make branches and delete them...

10 Monday
Ways to move forward...
	-ignore the errors of not being able to create a new object (thats huge)
	-remove i collection
	-use automapper, viewmodel all that... and not have i collection...

	-jump to authentication

09 Sunday

THis looks promising https://github.com/MichaelGustavsson/westcoast-cars-api/blob/main/Data/SeedData.cs

I shouldn't build without testing.

Using https://github.com/MichaelGustavsson/WestCoast-Education-Solution/blob/main/API/Data/DataContext.cs



https://www.notion.so/07f326a24db34eec8f9f7bea2c7f22b4?v=6a8d9729ff0a46a48758fbc489275087&p=d171fd63d9bd4e10b7bf631023d0f7f0&pm=s

Test 123

College_API.Controllers.CoursesController.GetUserById (College-API)


220503 more database and other interface stuff..
		05:00 Five step demo by MichealGustavsson on Security..??? Claims Roles
		27:00 HATEOS //a standard way to confirm that your actions have been successful. and here is the object you created in the head...
		//	Your GET/POST methods, you don't want to return back an object everytime. Like for post method.
						// There you can use HATEOS
		32:00 Using [Required] in data model class, So incoming create object requests doesn't have ex null in CourseNumber...
				if(!ModelState.isValid)... //in case of model number. this can be done in front end.
				if (!ModelState.IsValid) return StatusCode(500, "Invalid model. Model must have Course number");
		35:28 A cooler way to catch error would be to set it in the required annotation. [Required(ErrorMessage = "Registreringsnummer is required)]
		41:00 UpdateCourse PUT method.
				From repo class Michael chooses to use throw exception. 50:00 !!!Since we used try/catch in repo, we can use it again in the controller since we will recieve a exception if it fails!!
		1:20:00  -dotnet --info
		1:25:00 building a Patch method
		1:36:00 don't return (in catch (Exception ex)) return StatusCode(500, ex). !!Dont return ex. Instead ex.Message...?

		2:03:00 Fixing the database structure.  To remove repetitive.
		2:09:00 Adding a new controller for the manufacturer table. Creating Get methods
		2:13:00 he asks: How to go about to create 1:many conenction in entity framework
				with public ICollection<Course> Courses {get; set;} = newList<Course>();
			2:17:00 how to add ForeignKey. GOt an error due to using System.ComponentModel.DataAnnotations.Schema; was missing so the build didn't run.


		2:29:00	-dotnet ef migrations add "added make and vehcile relationship" -o "Data/Migrations"
		2:34:30 -dotnet ef database drop --force // droping the table due to complication in adjusting connetion tables
				-dotnet ef database update //updates the migration files. //sometimes this won't work

		"There was a discussion on tables depending on eachother. Here We made manufacturers. coupled with Vehicles."
			"You can't delete a item in the manufacturing table, if a vehicle is connected to it." "Man kan inte ha föräldarlösa barn."
			2:39:20//On delete: ReferentialAction. Cascade.. which means it will delete everything connected to the...
			You can change it to SetNull or NoAction or Restrict
		2:54:00	Michael Gustavsson is creating controller, Repo with Interface for Manufacturing table.


220503 13.. more . Around 2:30:0 we start with security. he creates a security demo, which I'm not sure if its connected to the project
		we start with building methods for the repo class.

		04:00 Question. In Category/Manu..Repo We have a SaveAllAsync(). This return await _context.SaveChangesAsync() > 0;

		STOPPING HERE ListManufacturerAsync() in manufacturerRepo.cs

		09:00 for controller to get in repo class with its own context manupilation, we need dependency injection.
		builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>(); //for the sake of getting a class that is instantiated.
		17:00 fixing automapper for Manufacturer. There was a mistake.<!!>// hot reload doesn't work with program.cs or Automapper.
		26:00 creating AddVehicleAsync WITH manufacturer(connecting table) //manufacturers is the parent table.

			public async Task AddVehicleAsync(PostVehicleViewModel model){
				"finding a vehicle in the Db with the same name(car manufacturer)"  "you need include to include the vehicle table?"
				var make = _context.Manufacturers.Include(c => c.Vehicles).Where(c => c.Name!.ToLower() == model.Make!.ToLower())
				.SingleOrDefualtAsync();

				//??? what are we creating? THe whole car? why should it have the company name to begin with...
				if (make is null)
					throw new Exception($"Tyvärr vi har inte teillverkaren {model.Make} i systemet.");

				"converting to vehicle from postvechleveiwModel"
				var vehcileToAdd = _mapper.Map<Vehicle>(model);
				"adding manufacturer to the model." //What's inside make?  A. a manufacturer item with vehicle in it.

				"Now we are in the vehicle class/table"
				vehicleToAdd.Manufacturer = make;
				"adding the vehicle to the database. " // vehicle is the child. 		Should there already exists a manufacturer to be able to add a vehicle? A. YES
				await _context.Vehicles.AddAsync(vehicleToAdd);
			}
				So you need to do two things. Add .manufacturer to the vehicle and add the vehicle to the db
										"This is not a connection table. it is 1:many"
		40:00 Important to do saveall before the request leaves the endpoint.
		44:00 Business rules. Besiness demands certain mechanics.  ex: we don't allow products from this company. Then it's vital that developers knows this in and out.
		1:36:00 Changing the link (bymake/{make})] to ("{id}/vehicles") 1:43:00 and using [FromQuery] in the argument (1:39:50)
		2:00:00 Till now we've made a method that calls all the cars listed in the manufacturer table. //Q. is this similar to getting students in a course? Everyone that's bought the course?
        A. Course is not a parent to user. A user can exist without being assigned to the course.
			//What to use singleOrDefault, SingleAsync? SingleOrdefault, if you find a null exception, it doesn't crash OR You need to do null check in the controller. 2:13:00
		2:00:00 POSTMAN tip. How to change the url to a set thing so you don't need to type it multiple times...
        How to get edit a object which is inside another opbject. 2:01:50 //I should find Michaels project on github.
        2:05:50 ListallManufacturersAsync() method

		2:30:00 Presentation on security
		2:45:0 creating a new project > dotnet new webapi -n Step01
		3:0:00 working on a method that accepts a username and pasword. Installing a Nuget for it.
			also authenication.JwtBearer
		3:18:00 jwt.io //to control that your token is valid/or working

20220504_morning 12:00 If your getting Michaels project from github, do a >>dotnet restore .Due to bin and obj being ignored by his github, which you need.

		41:18 how to protect your endpoints with[Authorize]
		46:00 How to set up shortcut on URL in postman (New Environment)
		51:00 Pipeline, fixing middlewear
		59:00 adding "app.UseAuthentication();" in program.cs
		1:25:00 configure authentication in pogram.cs
		1:44:00 pasting the auth token into the auth tab in Postman... You first need to run the login method to get the auth token, now you use it on other places where it is needed.
		1:47:00 [authorize(policy: "admin")] - to restrict who has asscess to the methods
        	You define these policies in program.cs
        2:00:00 new information on the token.
        2:18:00 when using auth token, use Bearer token in Postman
		2:20:00 downloading Nuget aspnetcore.identity, JwrBearer, FrameworkCore, Sqlite, Tools,
        2:27:00 building the ApplicationContext (Db connection)
        2:45:00 settings for password, etc..

20220504 13...	UsermManager<IdentityUser> is needed
		00:09:30 creating a Post method [HttpPost("register")] to register a new user...
        23:00 checking user password
		39:00 registering a user acction that can login.

		1:25:00 user, new claim("Admin", "true"));
		2:30:00 CreateRole method

20220505_090110 	There was discussion on [authenication] tag not working.
		discussion on what framework is good
		04:00 He mentions working with cookies is easier. I NEED A TUTOR, How do they learn all this
		40:00 we are git-cloning ITHS-STHLM-Westcoast-Cars-Starter
		48:30 < ls -al // shows all the files in the folder
				< rm -rf .git 	//removes the git file. After this he creats a new git
								< git init, git add ., git commit -m "init"
				Disscussion on droping the database when creating new coloumn. You don't need to. Just set it to null when it's deleted.
				Unless when we added manufacturers.
		51:40 app.diagrams.net
		1:21:00	Presentation. WHat are the tools, hosting sites, where there api's end up?
				cheap hosting namecheap.com, app.netify.com?
		2:17:00 starting the web?

	2:22:25 starts coding. Installs: > sln add. Clients/MvcApp/  ...// adding a mvcapp?

//-----------------PROJECT MVC, Razor, React--------------------------------------------------------

"Links"
app.diagrams.net 	For building diagrams. //under software, you will find database diagrams
namecheap.com 		"Domain names, "
netify.com - where you can host your website (react or html/css)

[20220505_090110 1:21:30]	Theory starts on WEB
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

[20220505_130015 30:00]
//We are building a MVC model and not Razor pages, so we deleted the css file, js file and erase everything except the @RenderBody() in the sharedLayout page.
	06:06 adding fonts to the library (Poppins and Roboto are simple)
//We have a controller(Vehicles), in it a method called index(). We create a View-file that will run the Index() method
	"So that file will be called Index.cshtml". 23:00. "We create a new Razor page file but delete the razorpage code and controller extention"
			"-since we are using a MVC project." With CS-code, we don't have any handholding so we have to create all the files ourselves.
//with @ _Layout.cshtml (the main page) we can add the different tabs in the page [32:00]
	"short hand for createing elements" ul>li>a "will create unordered list, a list and inside a link"
//HttpClient(); [35:00] Using Tag helper in the cshtml file to conntect to the controller.
	asp-controller="Vehicles" asp-action="Index"

Connecting to the API 40:00
	///We add our Get-method [41:19]
	[51:38]using var http = new HttpClient(); var response = await http.GetAsync(url);
	///url is "https://localhost:####/api/v1/vehicles/list
	///Talk about Garbage collector
//We run into an error when we try Debugger
	We solve it by running the api and the mvcApp in different VS-code. Due to mapstructure mechanics,
	the debugger runs everything at once. 58:00
	"You can run the debugger .NET Core Attach but you need to type something to make it work, its an extension maybe?" 1:26:00
1:20:00 Back from break. HTTPS development certificate
	If you don't have this, you can run the Terminal as Admin and type
	-dotnet dev-certs https --trust
//Second debugger run 1:30:00
//Creating a View Model 1:31:00 to take in the data thats coming in.
	The data thats coming in is screwed and since it doesn't match our viewModel properties, we need to fix it. 1:37:30
"Model folder in MvcApp is for classes which has methods that talks to the REST api"1:45:00
		Is that affärs logik, in the presentation picture?
///Instead of using in Courses Controller
	var options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
	///You can use in the CoursesViewModel.cs, but it will clutter the file. 1:48:300
    [JsonPropertyName("CoursesId")]

//Moving the logic above to the right place, which is the Model folder. 1:52:00
//creating baseUrl in appsettings.Development.json so it can be reused. 1:55:00
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

2:58:00	//We are adding a £ tag in the View file to tell where the data is coming from
	@model IEnumerable<MvcApp.ViewModels.CourseViewModel>
	///We fill in the view file with @ tag helpers to bring in the data. I don't fully follow here. [ ]
//We need to have HTTP tags above the methods to diffrentiate them. ??? asp-action="Details" didn't help...
		<a asp-controller="Courses" asp-action="Details">@course.Title</a>
		///asp-controller="Courses" means it will look at CoursesController. ///You don't need to type COntroller.
		///asp-action will search for the method name, BUT why didn't it work?

3:12:00 //BAD practice. You must send in the return View("what the method name is", object)

[20220510_090125]
05:30 ish, in app.development.json, we can change the port number where the project starts.
29:00	To be able to run the debugger with both projects in the main folder, you need to remove .vscode folder everywhere
except the main project folder.
In the launch.json folder. We are changing "program": "${workspaceFolder}/WCC-API/bin/Debug/net6.0/WCC-API.dll", to
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

50:12 To know which port number you need to have in MVCapp, run the API project with dotnet watch run, and use that port
56:00	If you want to add in data automatically, you can follow here.
	///After filling in the code 1:06:00
	"dotnet ef database drop --force" and "dotnet ef database update"
//----------------------HTML and CSS----------------------// 1:36:00
1:49:00 creating nav bar. //obs! navbar needs to be id="navbar" and not class...1:52:10
2:10:00	We used fonts from Font-Awesome ///step 1 Choose a logo https://fontawesome.com/search?s=solid%2Cbrands
	///step 2.https://cdnjs.com/libraries Search for Font-awesome
2:43:00	//Building the Course list page, trying to add img to the list.
2:58:00 //Fixing gallery-wrapper. with display: grid; grid-template-columns: repeat(4, 1fr);

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

20220511_100737 --------------------------------
We are creating the Details page. 18:00	You need [HttpGet("Details/{id}")] above the method.
"TIP Before building the method or JavaScript, try testing if you can reach the site!!!"
27:20 slice(0, -1) javaScript method where you take a string and choose which part you want to keep and which to remove.
			0 means starting from 0 index, and -1 is removing the last index.
45:00	Working on Details method.


	//Fast forwarding
[1:38:30] Needing Json serializer settings so program can read incoming data
[1:55:00] We create a model class VehicleServiceModel where we put in the hosting link and Json serializer through constructor so we can call it in the methods.
[2:23:50] PRESENTATION on MVc model? The timestamp will show you what Action methods can return
[2:35:00] Setting up app.MapCOntrollerRoute in Program.cs to automatically route to a certain place?

[20220510_090125 27:33] How to set up the debugger MVC model and API from an external project?

[20220510_090125] Building the website by setting up the front page. and things explained above.

[20220510_130556] Lots of JS and css

[20220511_100737] Adding functions to the site. Like opening item, flexible resolution(mobile, pad, large screen)

[20220511_125700] search functions and more. AT [2:28:00] We start the JS app.!!

[20220512_090134] Starts by talking about the project, maybe continues with the jsAPp and Razor pages starts at [2:31:00]
 - js the definitive guide 7th edition
 learning javascript Ethan Brown, JAva script design patterns Addy Osmani

[20220517_090026]	Razor pages [1:56:00] Creating Add car function

[20220517_130531] [41:32] We are convinced that react is the shit. And it starts here



		//REACT
		///	-npx create-react-app .		"The dot . meanns make the project inside the folder called react-app? YES
		/// -npm install 		//You need to have node_modules in your project. BUT I DID. Couldn't get the website to launch without it
		///	NOT NEEDED UNLESS...-npm i -g npx 	"-g means to install the name npx" i means..
		///	-npm start
		///
		/// Had an issue with "npm" not working(windows) ERROR: global, local deprecated...https://github.com/npm/cli/issues/4980
			///solved it by following the link above.
// FInd jobs close to you and see what they need <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


[20220517_130531]
1:26:00 installing react app in a client folder.
	>rm -rf nameOfTheFolder		//this deletes the folder
    >mkdir react-app
    >npx create-react-app .

[1:43 40]	Two extensions to help with code. Jest and Jasmine [1:52: 20] Extension neede ES7: ES7 React/Redux/GraphQL/React-Native snippets
	//Jasmine - Behavior-Driven JavaScript
// after deleting things, we are coding in src Folder, in index.js [1:55:00]
	//Babel - tranforms JS to "real" js? Turn JS to another format, like scriptJS
// We create a new file called App.jsx, there we are the html things,
	// where we export and then import into index.js in the same src Folder.
	// [2:11:00] SO you can import inside another file.

// Creating CSS [2:41:00]. //Making things dynamic, as in using properties
// from data?[2:51:00]  // [2:58:35] Placing in huge data and calling
	//it in VehicleList.jsx  // Short summary [3:11:50]
3:11:00 How components work. Is it better to use .map on a parent component instead of sending it down?
	A. We create a simple, empty component (<Vehicle_list />), in there there are tons of code and a head and body table that will show a list of vehicles. We'll have a simple component to display the repetetive vehicle list.
    //Since I don't care much for mastering programming with code, I should be able to maintain it for a job, its the entry to IT. What will I move to? Writing? managing people? Manager! A guy from Uppasala university named it as soon as he heard me explain what I like. Managers are quite dumb?
    https://github.com/MichaelGustavsson?tab=repositories

[20220518_091607 09:50] React Router
// ESLint. [17:00] Helps you with javascript coding. [21:00] Repetition
	///Font awesome is mentioned to bring fonts.
	//More explanation regarding how Javascript works [36:30]
		"You can use props instead of a specific {object?}" with curly bracers, you break down and choose specific object
// Adding a Component Folder [50:45]
// [45:37] How to DEBUG with the browser
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
Getting the api endpoints |Life cycle hook event
	/// change the port to something else. 7247 and 5246?  <- This is how we let the react get data from mvc backend project
// [1:51:00] Building function to bring the data //LOADING in the API data
	// we need a useEffect funtion to use the incoming url BUT YOU will get an error
2:01:00] We add the JS port to the .net API by adding it in the Program.cs
builder.Services.AddCors(options => {
	options.AddPolicy("WestcoastCors", policy => {
		policy.AllowAnyHeader(); policy.AllowAnyMethod();
		policy.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:3002")
	})
})
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


[20220518_130639] [06:21] installing router with -npm install react-router-dom	//document object model.
	///this is to be able to navigate to a new page. [21:30] importing it in App.jx
//[28:30] coding in Home.jsx -it's the homepage.  Introducing <> JSX fragment or React.Fragment.	Note. return ()  you need brackets if you are using more than one element.
//In App.js, we are including different pages with Router, routes and route [38:16]
35:00 creating different paths or links. with <Routes>
37:40 Lär dig react ORDENTLIGT -Michael Gustavsson
[41:00]Creating Navbar(){}	//theres a wrong way to do it (without using import { NavLink }
	///With this we place two pages. A Start sida and lager fordon, which shows list of cars.
//CReating AddVehicle(){} [1:23:00]. With the form tags filled in AddVehicle.jsx, we include the route -link to the new page in App.jsx
	///and add the button in the navbar to the new page.
	<NavLink to='/add'>Lägg till</NavLink>
//Data bindning	[1:33:0 ] First we build for "Registreringsnummer". Now we build the remaining [1:47:30]

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
//Adding an img DEFAULT item to function AddVehicle().. [2:00:00]
// saveVehicle(vehicle) => { }		//to database [2:03:26] [2:05:20] there is code below
	//we returned empty console.log(await response.json()); which gave an error.
	"Find out why after the break!"[2:15:00]
Quick explanation on how the methods are connected. saveVehicle and the above. [2:19:30]
//Edit vehicles 2:31:43		process{a) create a EditVehicle.jsx file. You'll have funtions there and then export it.
									b) in App.jsx, you'll import it and add the <Route path='/edit/:id' element={<EditVehicle />} />}
// 2:35:00 import { useNavigate } from 'react-router-dom'; //we use this to navigate "kod mässigt?"
		//2:55:00	making the Put fucntion and the save function
//Adding extra steps to hide or veiw data [3:10:00] Adding an ResponseVeiwModel in Vehicles-API,
	//creating JsonSerializer in [HttpGet("list")] method
//Documentation for swagger 2:20:00? [ProducesResponseType(StatusCodes.Status200OK)]
//<PropertyGroup> settings 2:33:00
//2:53:00 Om Async await. tre olika sätt att kommunicera.
//MicroServices 3:04:00. Kuberneties is a deligating service/program that does the smart architect for you
//Talking to external API 3:27:00
	.
	.
	.
	.
	.

///////////////////////////JS EXAMPLE///////[40:43]

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

//Example 3 A function that recieves VehicleItemProperty [40:43]
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
//Loading a list of vehicles through Get method [http {"list"}] [2:04:00]
const loadVehicle = async () => {
	const url = `${process.env.REACT_APP_BASEURL}/vehicles/list`; //we are using back ticks ``
	const response = await fetch(url);

	if(!response.ok){
		console.log('Hittade inga bilar, eller så gick något fel');
	}
	setVehicles(await response.json());
}
/////////////////// Sending vehicles to database///////////// [2:05:20]
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

 ------------------------------------THE END---------------------------------------------------------
  ------------------------------------THE END---------------------------------------------------------
   ------------------------------------THE END---------------------------------------------------------
    ------------------------------------THE END---------------------------------------------------------




























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



Reminders
----------------
[ ] WHere the hell is this doc with the project info??

Making sense of Identity
-------------------------------
When you register, your sending in an email and pass.
	You save that with a new IdentityUser. 		///How do I connect that to Kunder? Kunder needs to have claims that sets them apart from eachother
	///Admin/student/Teacher. BUT How does it connect to the controller? Or
How to connect controller to login info? With a function...?




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

 -->
