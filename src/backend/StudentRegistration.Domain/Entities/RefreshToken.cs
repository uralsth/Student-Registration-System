namespace StudentRegistration.Domain.Entities
{
	public class RefreshToken
	{
		public int Id { get; set; }
		public string Token { get; set; } = string.Empty;

		public DateTime Created { get; set; } = DateTime.Now;
		public DateTime Expires { get; set; }

		public string? IpAddress { get; set; }
		public string? MacAddress { get; set; }
		public int? DeviceId { get; set; }

		public bool? IsActive { get; set; }


	}
}
