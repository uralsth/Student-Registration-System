using AutoMapper;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Application.Repositories;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.Services
{
    public class ProjectService : IProjectService
	{
		private readonly IEntityRepository<Project> _projectRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public ProjectService(IEntityRepository<Project> projectRepository, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_projectRepository = projectRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<Project> AddProject(ProjectCreateDto project)
		{
			Project mappedProject = _mapper.Map<Project>(project);
			Project newProject = await _projectRepository.Add(mappedProject);
			_unitOfWork.Save();
			return newProject;
		}

		public async Task<Project> DeleteProject(int id)
		{
			Project deletedProject = await _projectRepository.Delete(id);
			_unitOfWork.Save();
			return deletedProject;
		}

		public async Task<IEnumerable<Project>> GetAllProject()
		{
			return await _projectRepository.GetAll();
		}

		public async Task<Project> GetProjectById(int id)
		{
			return await _projectRepository.GetById(id);
		}

		public async Task<List<Project>> GetProjectsByIds(List<int> selectedProjectIds)
		{
			List<Project> result = new List<Project>();	
			foreach (var projectID in selectedProjectIds)
			{
				 result.Add(await _projectRepository.GetById(projectID));
			}
			return result;
		}

		public async Task<Project> UpdateProject(Project oldProject, ProjectEditDto editedProject)
		{
			var mappedProject = _mapper.Map(editedProject, oldProject);
			Project newupdatedProject = await _projectRepository.Update(mappedProject);
			_unitOfWork.Save();
			return newupdatedProject;
		}
	}

}