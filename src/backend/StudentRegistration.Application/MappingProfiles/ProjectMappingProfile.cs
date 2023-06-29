using AutoMapper;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.MappingProfiles
{
	public class ProjectMappingProfile : Profile
	{
        public ProjectMappingProfile()
        {
			CreateMap<ProjectCreateDto, Project>();
			CreateMap<Project, ProjectEditDto>().ReverseMap().ForMember(dest => dest.ProjectId, opts => opts.Ignore());
        }
    }
}