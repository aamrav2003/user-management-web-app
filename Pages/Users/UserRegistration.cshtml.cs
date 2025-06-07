using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IAB_251_Assessment2.Application.Interfaces;
using IAB_251_Assessment2.BusinessLogic.Entities;
using IAB_251_Assessment2.Pages.User.Model;
using Microsoft.AspNetCore.Identity;


namespace IAB_251_Assessment2.Pages.Users
{
    public class UserRegistrationModel : PageModel
    {
        private readonly IUserRegistrationAppService _userAppService;

        public UserRegistrationModel(IUserRegistrationAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [BindProperty]
        public UserViewModel User { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _userAppService.AddUser(new BusinessLogic.Entities.User
            {
                FirstName = User.FirstName,
                FamilyName = User.FamilyName,
                EmailAddress = User.EmailAddress,
                PhoneNumber = User.PhoneNumber,
                CompanyName = User.CompanyName,
                Address = User.Address,
                Password = User.Password,
                Role = User.Role ?? "Customer" // Default to Customer if no role selected
            });

            return RedirectToPage("/Index");
        }
    }
}
