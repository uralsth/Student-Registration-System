using AutoMapper;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.MappingProfiles
{
	public class StudentMappingProfile : Profile
	{
		public StudentMappingProfile()
		{
			CreateMap<StudentProjectCreateDto, Student>();
			CreateMap<StudentCreateDto, Student>();
			CreateMap<StudentProjectCreateDto, Project>();
			//CreateMap<StudentDocumentCreateDto, Document>();
			CreateMap<Student, StudentEditDto>().ReverseMap().ForMember(dest => dest.Id, opts => opts.Ignore());
			//CreateMap<StudentProjectEditDto, Student>().ForMember(dest => dest.Id, opts =>
			//{
			//	opts.Ignore();
			//});
			//CreateMap<StudentProjectEditDto, Project>().ForMember(dest => dest.ProjectId, opts =>
			//{
			//	//opts.Ignore();
			//	opts.MapFrom(src => src.ProjectId);
			//});
			CreateMap<Student, StudentProjectListDto>();


			CreateMap<StudentDocumentCreateDto, Student>();
		}
	}
}