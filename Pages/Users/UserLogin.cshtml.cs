using IAB_251_Assessment2.DataAccess.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace IAB_251_Assessment2.Pages.Users
{
    public class UserLoginModel : PageModel
    {
        private readonly UserManagementDBContext _context; //IUserLoginContext

        public UserLoginModel(UserManagementDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Credential credential { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {

        }

        //public void OnPost()
        //{
        //    // check that there is something there
        //    if (!ModelState.IsValid) return;

        //    // verify credentials
        //    if (credential.EmailAddress == "ad" && credential.Password == "pw")
        //    {
        //        // create security context
                
        //    }
        //}

        public class Credential
        {
            [Required]
            public string EmailAddress { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.EmailAddress == credential.EmailAddress);

            if (user == null || !VerifyPassword(credential.Password, user.Password))
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            //return RedirectToPage("/Index");

            // this bit cets up the asociation between a user and claimed vals.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role ?? "Customer") // Default to Customer if Role is null
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            // Redirect based on role
            if (user.Role == "Employee")
            {
                return RedirectToPage("/Quotation"); // Employee dashboard with quotation requests
            }
            else
            {
                return RedirectToPage("/CustomerQuotes"); // Customer dashboard with their quotations
            }
        }

        
        private bool VerifyPassword(string InputPassword, string DataBasePW)
        {
            return InputPassword == DataBasePW;
        }

    }
}
