using Microsoft.AspNetCore.Http;

namespace StudentRegistration.Helpers.Interfaces
{
    public interface IImageHelper
    {
        string Convert(IFormFile photoFile);
        List<string> ConvertMultiple(List<IFormFile> photoFiles);
    }
}