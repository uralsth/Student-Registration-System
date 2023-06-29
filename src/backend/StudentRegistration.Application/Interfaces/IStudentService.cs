using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.Interfaces
{
	public interface IStudentService
	{
		Task<Student> GetStudentById(int id);

		Task<IEnumerable<Student>> GetAllStudent();

		Task<Student> AddStudent(StudentCreateDto student);

		Task<Student> DeleteStudent(int id);

		Task<Student> UpdateStudent(Student oldStudent, StudentEditDto editedStudent);

		Task<Student> AddStudentAndProject(Student newStudent);
		//Task<Student> AddStudentAndDocument(Student newStudent);
		Task<Student> AddStudentAndDocuments(Student newStudents, StudentDocumentCreateDto studentDocument);

		#region Comments
		//Task<IList<StudentProjectListDto>> GetAllStudentWithProject();
		//Task<IList<Student>> GetAllStudentWithProject();
		//Task<Student> GetStudentWithProjectById(int id);

		//Task<Student> UpdateStudentWithProject(Student oldStudent, StudentProjectEditDto editedStudentProject);

		//Task<StudentProjectListDto> GetStudentWithProjectByEmail(string Email);
		#endregion
	}
}