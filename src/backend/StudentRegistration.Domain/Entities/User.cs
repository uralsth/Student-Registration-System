namespace StudentRegistration.Domain.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }

		public string? RefreshToken { get; set; }
		public DateTime? TokenCreated { get; set; }
		public DateTime? TokenExpires { get; set; }
	}
}
