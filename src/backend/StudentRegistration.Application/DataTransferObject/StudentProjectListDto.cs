namespace StudentRegistration.Application.DataTransferObject
{
	public class StudentProjectListDto
	{
		public int StudentId { get; set; }

		public int ProjectId { get; set; }
		public string Email { get; set; }

		public string UserName { get; set; }
		public int Contact { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
