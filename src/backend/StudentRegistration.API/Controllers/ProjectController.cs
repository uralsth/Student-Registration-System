using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProjectController : Controller
	{
		private readonly IProjectService _projectService;

		public ProjectController(IProjectService projectService)
        {
			_projectService = projectService;
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromForm] ProjectCreateDto project)
		{
			await _projectService.AddProject(project);
			return Ok(project);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			Project deleteProject = await _projectService.DeleteProject(id);
			return Ok(deleteProject);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var projects = await _projectService.GetAllProject();
			if (projects == null)
			{
				return NotFound();
			}
			return Ok(projects);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByID(int id)
		{
			Project project = await _projectService.GetProjectById(id);
			if (project == null)
			{
				return NotFound();
			}
			// fix
			//var studentEditView = _mapper.Map<Student>(student);
			return Ok(project);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromForm]ProjectEditDto editedProject)
		{

			Project oldProject = await _projectService.GetProjectById(id);
			if (oldProject == null)
			{
				return NotFound();
			}
			Project result = await _projectService.UpdateProject(oldProject, editedProject);
			return Ok(result);
		}

    }
}
