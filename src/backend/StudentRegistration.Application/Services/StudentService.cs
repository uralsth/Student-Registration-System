using AutoMapper;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Application.Repositories;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.Services
{
    public class StudentService : IStudentService
	{
		private readonly IStudentRepository _studentRepository;

		//private readonly IEntityRepository<Student> _entityRepository;
		private readonly IMapper _mapper;

		private readonly IUnitOfWork _unitOfWork;

		public StudentService(IStudentRepository studentRepository, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_studentRepository = studentRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		//public async Task<Student> AddStudent(StudentProjectCreateDto student, List<Project> projects)
		//{
		//	Student mappedStudent = _mapper.Map<Student>(student);
		//	mappedStudent.Projects = projects;
		//	Student newStudent = await _studentRepository.Add(mappedStudent);
		//	_unitOfWork.Save();
		//	return newStudent;
		//}

		public async Task<Student> AddStudent(StudentCreateDto student)
		{
			Student mappedStudent = _mapper.Map<Student>(student);

			Student newStudent = await _studentRepository.Add(mappedStudent);
			_unitOfWork.Save();
			return newStudent;
		}

		public async Task<Student> DeleteStudent(int id)
		{
			Student deletedStudent = await _studentRepository.Delete(id);
			_unitOfWork.Save();
			return deletedStudent;
		}

		public async Task<IEnumerable<Student>> GetAllStudent()
		{
			return await _studentRepository.GetAll();
		}

		public async Task<Student> GetStudentById(int id)
		{
			Student student = await _studentRepository.GetById(id);
			return student;
		}

		//public async Task<Student> GetStudentWithProjectById(int id)
		//{
		//	//Student student = await _studentRepository.GetStudentWithProject(id);
		//	Student student = await _studentRepository.GetStudentWithProject(id);
		//	return student;
		//}
		//public async Task<IList<Student>> GetAllStudentWithProject()
		//{
		//	//var queryResult = await (from s in _unitOfWork.Students.Table
		//	//						 join p in _unitOfWork.Projects.Table on s.Id equals p.StudentId
		//	//						 select new StudentProjectListDto
		//	//						 {
		//	//							 Contact = s.Contact,
		//	//							 Description = p.Description,
		//	//							 Email = s.Email,
		//	//							 Name = p.Name,
		//	//							 ProjectId = p.ProjectId,
		//	//							 StudentId =s.Id,
		//	//							 UserName = s.UserName
		//	//						 }).ToListAsync();
		//	//  select new StudentProjectListDto
		//	//  {
		//	//Name = p.Name,
		//	//StudentId = s.Id,
		//	//ProjectId = p.ProjectId,
		//	//  }
		//	//return await _unitOfWork.Students.GetAllStudentWithProject();
		//	//return _studentRepository.GetAllStudentWithProject();
		//	//return queryResult;
		//	return await _studentRepository.GetAllStudentWithProject();
		//}

		public async Task<Student> UpdateStudent(Student oldStudent, StudentEditDto editedStudent)
		{
			var mappedStudent = _mapper.Map(editedStudent, oldStudent);
			Student newupdatedStudent = await _studentRepository.Update(mappedStudent);
			_unitOfWork.Save();
			return newupdatedStudent;
		}

		//public async Task<Student> UpdateStudentWithProject(Student oldStudent, StudentProjectEditDto editedStudentProject)
		//{
		//	Project existingProject = oldStudent.Projects.FirstOrDefault(p => p.ProjectId == editedStudentProject.ProjectId);
		//	if (existingProject != null)
		//	{
		//		var mappedStudentProject = _mapper.Map(editedStudentProject, oldStudent);
		//	}

		//	//var mappedStudent = _mapper.Map<Student>(editedStudentProject);
		//	//var mappedProject = _mapper.Map<Project>(editedStudentProject);
		//	//var mappedStudent = _mapper.Map<Student>(mappedStudentProject);
		//	//var mappedProject = _mapper.Map<Project>(editedStudentProject);
		//	//Project existingProject = mappedStudent.Projects.FirstOrDefault(p => p.ProjectId == mappedProject.ProjectId);
		//	//if (existingProject != null)
		//	//{
		//	//	_mapper.Map(mappedProject, existingProject);
		//	//}
		//	//else
		//	//{
		//	//	mappedStudent.Projects.Add(mappedProject);
		//	//}
		//	Student newupdatedStudent = await _studentRepository.Update(oldStudent);
		//	_unitOfWork.Save();
		//	return newupdatedStudent;
		//}
		//public async Task<StudentProjectListDto> GetStudentWithProjectByEmail(string Email)
		//{
		//	var Student = await (from s in _unitOfWork.Students.Table
		//						 join p in _unitOfWork.Projects.Table on s.Id equals p.StudentId
		//						 where s.Email == Email
		//						 select new StudentProjectListDto()
		//						 {
		//							 Email = s.Email,
		//							 UserName = s.UserName,
		//							 Contact = s.Contact,
		//							 Name = p.Name,
		//							 Description = p.Description
		//						 }).Distinct().ToListAsync();
		//	return Student.FirstOrDefault();
		//}

		public async Task<Student> AddStudentAndProject(Student newStudent)
		{
			Student student = await _studentRepository.Update(newStudent);
			_unitOfWork.Save();
			return student;
		}

		public async Task<Student> AddStudentAndDocuments(Student oldStudent, StudentDocumentCreateDto studentDocument)
		{
			if (oldStudent.Documents.Any())
			{
				oldStudent.Documents.Clear();
			}
			var mappedStudent = _mapper.Map(studentDocument, oldStudent);
			Student student = await _studentRepository.Update(mappedStudent);
			_unitOfWork.Save();
			return student;
		}

	}
}