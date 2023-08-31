// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.Json;
// using System.Threading.Tasks;
// using College_API.Models;
// using College_API.ViewModels;
// using College_API.ViewModels.CustomerViewModel;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;

// namespace College_API.Data
// {
//     public class LoadData
//     {
//         public static async Task LoadCourses(CollegeDatabaseContext context)
//         {
//             if (await context.Courses.AnyAsync()) return;

//             var courseData = await File.ReadAllTextAsync("Data/course.json");
//             var courses = JsonSerializer.Deserialize<List<Course>>(courseData);
//             if (courses != null)
//             {
//                 foreach (var course in courses)
//                 {
//                     context.Courses.Add(course);
//                 }

//                 await context.SaveChangesAsync();
//             }
//         }

//         // public static async Task LoadUsers(CollegeDatabaseContext context)
//         // {
//         //     if (await context.Users.AnyAsync()) return;

//         //     var userData = await File.ReadAllTextAsync("Data/user.json");
//         //     var users = JsonSerializer.Deserialize<List<User>>(userData);

//         //     await context.AddRangeAsync(users!);
//         //     await context.SaveChangesAsync();
//         // }
//         public static async Task LoadUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//         {
//             if (await userManager.Users.AnyAsync()) return;

//             var userData = await File.ReadAllTextAsync("Data/user.json");
//             var users = JsonSerializer.Deserialize<List<ApplicationUser>>(userData);

//             foreach (var user in users)
//             {
//                 await userManager.CreateAsync(user, "password");

//                 if (user.UserRoles != null)
//                 {
//                     foreach (var userRole in user.UserRoles)
//                     {
//                         var role = await roleManager.FindByIdAsync(userRole.RoleId);
//                         if (role != null && !await userManager.IsInRoleAsync(user, role.Name))
//                         {
//                             await userManager.AddToRoleAsync(user, role.Name);
//                         }
//                     }
//                 }
//             }
//         }

//     }
// }