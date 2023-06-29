using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using StudentRegistration.Helpers.Interfaces;

namespace StudentRegistration.API.Helpers
{
	public class ImageHelper : IImageHelper
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ImageHelper(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public string Convert(IFormFile photoFile)
		{
			string uniqueFileName = null;
			if (photoFile != null)
			{
				string[] allowedContentExtension = new string[] { "jpeg", "jpg", "png" };
				string[] allowedContentTypes = new string[] { "image/jpeg", "image/png" };
				const long maxFileSize = 7 * 1024 * 1024;
				if (!allowedContentTypes.Contains(photoFile.ContentType.ToLower()) || allowedContentExtension.Contains(Path.GetExtension(photoFile.FileName.ToLower())) && photoFile.Length > maxFileSize)
				{
					return null;
				}

				string uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}
				uniqueFileName = Guid.NewGuid().ToString() + "_" + photoFile.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var filestream = new FileStream(filePath, FileMode.Create))
				{
					photoFile.CopyTo(filestream);
				}
			}
			return uniqueFileName;
		}

		public List<string> ConvertMultiple(List<IFormFile> photoFiles)
		{
			List<string> uniqueFileNames = new List<string>();
			foreach (var photoFile in photoFiles)
			{
				uniqueFileNames.Add(Convert(photoFile));

			}
			return uniqueFileNames;
		}
	}
}
