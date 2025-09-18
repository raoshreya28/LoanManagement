namespace Lending.Models
{
    public class LoginResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
    }
}
