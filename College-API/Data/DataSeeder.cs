// using College_API.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;

// namespace College_API.Data
// {
//     public static class DataSeeder
//     {
//         public static void SeedData(CollegeDatabaseContext context)
//         {
//             Console.WriteLine("Seeding data...");
//             if (!context.Categories.Any())
//             {
//                 context.Categories.AddRange(
//                     new Category { Id = 1, CategoryName = "Computer Science" },
//                     new Category { Id = 2, CategoryName = "Information Technology" }
//                     // Add more categories as needed
//                 );
//             }

//             if (!context.Courses.Any())
//             {
//                 context.Courses.AddRange(
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
//             }

//             context.SaveChanges();
//             Console.WriteLine("Data seeded successfully.");
//         }
//     }
// }
