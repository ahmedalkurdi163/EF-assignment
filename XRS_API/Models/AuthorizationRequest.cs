namespace XRS_API.Models
{
    public class AuthorizationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class XRS_User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
