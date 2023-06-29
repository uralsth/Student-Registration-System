using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudentRegistration.Application.DataTransferObject;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Application.MappingProfiles
{
	public class DocumentMappingProfile : Profile
	{
        public DocumentMappingProfile()
        {
            CreateMap<AddDocuments, Document>().ForMember(dest => dest.DocPath, opts => opts.MapFrom(src => src.Images));
            CreateMap<AddDocuments, ImagePath>();
        }
    }
}
