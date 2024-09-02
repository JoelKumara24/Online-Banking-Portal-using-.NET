namespace OnlineBankPortal.Models
{
	public class UserProfile
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string ProfilePictureUrl { get; set; }
		public string PasswordHash { get; set; }
		public int AccountNumber { get; set; }
	}


}
