using Microsoft.AspNetCore.Http;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.DataTransferObject
{
	public class StudentProjectCreateDto
	{
		//public string UserName { get; set; }
		//public string Email { get; set; }
		//public string Password { get; set; }
		//public DateTime DateOfBirth { get; set; }
		//public string Address { get; set; }
		//public int Contact { get; set; }
		//public string? PhotoPath { get; set; }
		//public IFormFile? Photo { get; set; }

		public int Id { get; set; }

		public List<int>? SelectedProjectIds { get; set; }
	}
}
