using Microsoft.AspNetCore.Http;
using StudentRegistration.Domain.Entities;
using StudentRegistration.Domain.Enums;

namespace StudentRegistration.Application.DataTransferObject
{
	public class AddDocuments
	{
		public AddDocuments()
		{
			Images = new List<ImageCreateDto>();
		}

		public Docs DocType { get; set; }
		public List<IFormFile> DocumentPhoto { get; set; }
		public IList<string>? DocumentImages { get; set; }
		public IList<ImageCreateDto> Images { get; private set; }

		public void AddImage()
		{
			foreach (var image in DocumentImages)
			{
				ImageCreateDto imageCreateDto = new ImageCreateDto
				{
					Path = image,
					DocType = DocType
				};
				Images.Add(imageCreateDto);
			}
		}

		//public List<ImagePaths>? ImagePath { get; set; }
	}
}