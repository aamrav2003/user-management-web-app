namespace IAB_251_Assessment2.Pages.User.Model
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public string CompanyName { get; set; } //only if applicable -> optional??
        public string Address { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
