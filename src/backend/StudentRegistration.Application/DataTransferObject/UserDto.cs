using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Application.DataTransferObject
{
	public class UserDto
	{
		[Required]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
