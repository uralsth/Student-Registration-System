using StudentRegistration.Domain.Enums;

namespace StudentRegistration.Application.DataTransferObject
{
	public class ImageCreateDto
	{
		public string Path { get; set; }
		public Docs DocType { get; set; }
	}
}