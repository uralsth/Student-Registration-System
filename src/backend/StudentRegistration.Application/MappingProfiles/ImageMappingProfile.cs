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
	public class ImageMappingProfile : Profile
	{
        public ImageMappingProfile()
        {
            CreateMap<ImageCreateDto, ImagePath>();
        }
    }
}
