using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using College_API.Data;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels.RegisterUserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace College_API.Repositories
{
    public class AdminRepository : ControllerBase, IAdminRepository
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CollegeDatabaseContext _context;
        private readonly IMapper _mapper;
        public AdminRepository(IMapper mapper, CollegeDatabaseContext context, IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _context = context;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        public async Task<IActionResult> CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }
            else
            {
                return StatusCode(200, $"Role name with name '{roleName}' already exists.");
            }
            return StatusCode(201, $"Role '{roleName}' created.");
        }

        public async Task<ActionResult<ViewModels.UserViewModel>> RegisterAdminAsync(RegisterUserViewModel model)
        {
            throw new NotImplementedException();
            // if (!ModelState.IsValid)
            // {
            //     return StatusCode(500, ModelState);
            // }

            // var user = new User
            // {
            //     Email = model.Email!.ToLower(),
            //     // UserName = model.Email.ToLower(),
            //     RegisterDate = DateTime.Now
            // };

            // var result = await _userManager.CreateAsync(user, model.Password!);

            // if (result.Succeeded)
            // {
            //     // Check if Administrator role exists, else create it.
            //     if (!await _roleManager.RoleExistsAsync("Administrator"))
            //     {
            //         var adminRole = new IdentityRole("Administrator");
            //         await _roleManager.CreateAsync(adminRole);
            //     }
            //     // Add roles/claims to user.
            //     await _userManager.AddToRoleAsync(user, "Administrator");
            //     await _userManager.AddClaimAsync(user, new Claim("Administrator", "true"));
            //     await _userManager.AddClaimAsync(user, new Claim("User", "true"));
            //     await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
            //     await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));

            //     // if (_signInManager.Options.SignIn.RequireConfirmedEmail)
            //     // {
            //     //     // Generate email verification link.
            //     //     var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //     //     // Encode token.
            //     //     confirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

            //     //     var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}"; // API base URL.
            //     //     var confirmationLink = baseUrl + $"/ConfirmEmail?userId={user.Id}&token={confirmationToken}";
            //     //     var subject = "Verifiera ditt konto hos Omställningsstudiestöd.";
            //     //     var message =
            //     //         $"Välkommen till Omställningsstudiestöd!<br><br><br>" +
            //     //         $"Ditt konto är nu registrerat och kan användas så fort det har verifierats.<br>" +
            //     //         $"Verifiera ditt konto genom att <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>klicka här</a>.<br><br><br>" +
            //     //         $"Med vänliga hälsningar Omställningsstudiestöd.";
            //     //     await _emailSender.SendEmailAsync(user.Email, subject, message);
            //     // }

            //     var userVM = _mapper.Map<UserViewModel>(user);

            //     return StatusCode(201, userVM);
            // }
            // else
            // {
            //     foreach (var error in result.Errors)
            //     {
            //         ModelState.AddModelError("User registration", error.Description);
            //     }

            //     return StatusCode(500, ModelState);
            // }
        }
    }
}