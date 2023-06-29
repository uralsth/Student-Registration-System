using StudentRegistration.Domain.Enums;

namespace StudentRegistration.Domain.Entities
{
	public class ImagePath
	{
        public int ImageId { get; set; }
        public string Path { get; set; }

        public Document Document { get; set; }
        public Docs DocType { get; set; }
		public int StudentId { get; set; }
		//public int DocumentId { get; set; }
	}
}