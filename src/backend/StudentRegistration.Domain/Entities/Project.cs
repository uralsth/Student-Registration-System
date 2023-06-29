namespace StudentRegistration.Domain.Entities
{
	public class Project
	{
		public int ProjectId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime? UpdatedDate { get; set; } = DateTime.Now;

		public int? StudentId { get; set; }
		public Student Student { get; set; }
	}

}
