# mvc-react web project Notes

  
    
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
		2:30:00 CreateRole method   
    
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

