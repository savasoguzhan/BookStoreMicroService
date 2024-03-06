namespace BookStore.Web.Models.AuthDtos
{
    public class RegisterationRequestDto
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public DateTime BrithData { get; set; }
        public string? Role { get; set; }
    }
}
