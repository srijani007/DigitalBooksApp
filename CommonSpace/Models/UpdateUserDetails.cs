namespace CommonSpace.Models
{
    public class UpdateUserDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserPass { get; set; } = null!;
        public string UserRole { get; set; } = null!;
    }
}
