using StudentRegistration.Domain.Enums;

namespace StudentRegistration.Domain.Entities
{
    public class Document
	{
        //public int DocId { get; set; }
        public Docs DocType { get; set; }
        public List<ImagePath> DocPath { get; set; }

		public Student Student { get; set; }
        public int? StudentId { get; set; }

    }
}