using System.Text;
using College_API.Data;
using College_API.Helpers;
using College_API.Interfaces;
using College_API.Models;
using College_API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.////////////////////////////////////////////////////////////

// Create database connection
builder.Services.AddDbContext<CollegeDatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"))
);
//Identity management and which datacontext should be used; to store users, roles and claims
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(7);
    }
).AddEntityFrameworkStores<CollegeDatabaseContext>();


// Dependency injection for our own Interfaces and classes
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
// builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

//Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

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
            Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("apiKey")!)
        ),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

//Configure and create policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admins", policy => policy.RequireClaim("Admin"));
});





///////////////////////////////////////////////////////////////////////////////////////
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Rules for which users have permission to access our api?
builder.Services.AddCors(options =>
{
    options.AddPolicy("College-APICors",
    policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins("http://localhost:3000");// Allow the React app's origin
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Use the CORS policy before authorization
app.UseCors("College-APICors");

app.UseAuthentication(); //220519_09.. 2:23:00
app.UseAuthorization();

app.MapControllers();

//LoadData
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<CollegeDatabaseContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await context.Database.MigrateAsync();
    // DataSeeder.SeedData(context);
    // await LoadData.LoadCourses(context);
    // await LoadData.LoadUsers(userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured while migrating loadData");
}
app.Run();

// await SeedDataAsync(app.Services);
// async Task SeedDataAsync(IServiceProvider services)
// {
//     using var scope = services.CreateScope();
//     var context = scope.ServiceProvider.GetRequiredService<CollegeDatabaseContext>();

//     if (!context.Categories.Any())
//     {
//         // ... seeding logic ...
//         context.Categories.AddRange(
//                     new Category { Id = 1, CategoryName = "Computer Science" },
//                     new Category { Id = 2, CategoryName = "Information Technology" }
//                     // Add more categories as needed
//                 );
//     }

//     if (!context.Courses.Any())
//     {
//         // ... seeding logic ...
//         context.Courses.AddRange(
//                     new Course
//                     {
//                         Id = 1,
//                         CourseNumber = "CS101",
//                         Name = "Introduction to Programming",
//                         Duration = "3 months",
//                         Description = "Explore the basics of programming and problem-solving.",
//                         Details = "In this course, you will learn the fundamentals of programming using popular languages like Python and Java. You'll discover how to write code, solve problems, and create simple applications. Topics covered include variables, data types, control structures, and basic algorithms.",
//                         CategoryId = 1
//                     },
//                     new Course
//                     {
//                         Id = 2,
//                         CourseNumber = "IT202",
//                         Name = "Network Fundamentals",
//                         Duration = "2 months",
//                         Description = "Learn the basics of networking and network administration.",
//                         Details = "This course provides an introduction to computer networks and their importance in today's digital world. You'll learn about network architecture, protocols, IP addressing, and network security. Practical exercises and labs will help you understand how to set up and manage a simple network.",
//                         CategoryId = 2
//                     }
//                     // Add more courses as needed
//                 );
//     }

//     await context.SaveChangesAsync();

