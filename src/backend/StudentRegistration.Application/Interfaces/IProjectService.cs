using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.Interfaces
{
    public interface IProjectService
    {
        Task<Project> GetProjectById(int id);
        Task<IEnumerable<Project>> GetAllProject();
        Task<Project> AddProject(ProjectCreateDto project);
        Task<Project> DeleteProject(int id);
        Task<Project> UpdateProject(Project oldProject, ProjectEditDto editedProject);
        Task<List<Project>> GetProjectsByIds(List<int> selectedProjectIds);
    }
}
