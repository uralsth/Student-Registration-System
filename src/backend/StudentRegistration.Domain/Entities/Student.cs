using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Domain.Entities

{
	public class Student
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
		public int Contact { get; set; }
		public string? PhotoPath { get; set; }

		public ICollection<Project> Projects { get; set; }
		public ICollection<Document> Documents { get; set; }
    }
}
