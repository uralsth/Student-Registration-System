using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Domain.Entities;
using StudentRegistration.Helpers.Interfaces;

namespace StudentRegistration.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IImageHelper _imageHelper;
		private readonly IProjectService _projectService;
		private readonly IStudentService _studentService;

		public StudentController(IStudentService studentService, IImageHelper imageHelper, IProjectService projectService)
		{
			_studentService = studentService;
			_imageHelper = imageHelper;
			_projectService = projectService;
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromForm] StudentCreateDto student)
		{
			string photoPath = _imageHelper.Convert(student.Photo);
			if (photoPath == null)
				return BadRequest("Please check your image extension or size of image");
			student.PhotoPath = photoPath;
			await _studentService.AddStudent(student);
			return Ok(student);
		}

		//[HttpPost]
		//public async Task<IActionResult> Add([FromForm] StudentCreateDto student)
		//{
		//	string photoPath = _imageHelper.Convert(student.Photo);

		//	List<string> citizenshipPhotoPath = _imageHelper.ConvertMultiple(student.Citizenship.DocumentPhoto);
		//	List<string> bankChequePhotoPath = _imageHelper.ConvertMultiple(student.BankCheque.DocumentPhoto);
		//	List<string> passportPhotoPath = _imageHelper.ConvertMultiple(student.Passport.DocumentPhoto);
		//	if (photoPath == null && citizenshipPhotoPath == null && bankChequePhotoPath == null && passportPhotoPath == null)
		//		//if (photoPath == null)
		//		return BadRequest("Please check your image extension or size of image");
		//	student.PhotoPath = photoPath;
		//	//student.BankCheque.DocumentImages. = bankChequePhotoPath;
		//	//student.Citizenship.DocumentImages = citizenshipPhotoPath;
		//	//student.Passport.DocumentImages = passportPhotoPath;
		//	Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>
		//	{
		//		{0 , citizenshipPhotoPath }, {1 , bankChequePhotoPath}, {2, passportPhotoPath}
		//	};




		//	await _studentService.AddStudent(student);
		//	return Ok(student);
		//}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			Student deleteStudent = await _studentService.DeleteStudent(id);
			return Ok(deleteStudent);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await _studentService.GetAllStudent();
			if (users == null)
			{
				return NotFound();
			}
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByID(int id)
		{
			Student student = await _studentService.GetStudentById(id);
			if (student == null)
			{
				return NotFound();
			}
			return Ok(student);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromForm] StudentEditDto editedStudent)
		{
			Student oldStudentWithProject = await _studentService.GetStudentById(id);
			if (oldStudentWithProject == null)
			{
				return NotFound();
			}

			string photoPath = _imageHelper.Convert(editedStudent.Photo);
			if (photoPath == null)
				return BadRequest("Please check your image extension or size of image");
			editedStudent.PhotoPath = photoPath;
			Student result = await _studentService.UpdateStudent(oldStudentWithProject, editedStudent);
			return Ok(result);
		}

		[HttpPut("AddProjectToStudent")]
		public async Task<IActionResult> Add(StudentProjectCreateDto studentProject)
		{
			Student newStudent = await _studentService.GetStudentById(studentProject.Id);
			List<Project> selectedProjects = await _projectService.GetProjectsByIds(studentProject.SelectedProjectIds);
			newStudent.Projects.Clear();
			newStudent.Projects = selectedProjects;
			Student addedStudent = await _studentService.AddStudentAndProject(newStudent);
			return Ok(addedStudent);
		}

		[HttpPut("AddDocumentToProject")]
		public async Task<IActionResult> Add([FromForm] StudentDocumentCreateDto studentDocument)
		{
			Student oldStudent = await _studentService.GetStudentById(studentDocument.StudentId);

			List<string> citizenshipPhotoPath = _imageHelper.ConvertMultiple(studentDocument.Citizenship.DocumentPhoto);
			List<string> bankChequePhotoPath = _imageHelper.ConvertMultiple(studentDocument.BankCheque.DocumentPhoto);
			List<string> passportPhotoPath = _imageHelper.ConvertMultiple(studentDocument.Passport.DocumentPhoto);

			if (citizenshipPhotoPath == null && bankChequePhotoPath == null && passportPhotoPath == null)
				return BadRequest("Please check your image extension or size of image");

			studentDocument.BankCheque.DocumentImages = bankChequePhotoPath;
			studentDocument.BankCheque.AddImage();
			studentDocument.Citizenship.DocumentImages = citizenshipPhotoPath;
			studentDocument.Citizenship.AddImage();
			studentDocument.Passport.DocumentImages = passportPhotoPath;
			studentDocument.Passport.AddImage();

			studentDocument.AddDocuments();


			await _studentService.AddStudentAndDocuments(oldStudent, studentDocument);
			return Ok(studentDocument);
		}
	}
}