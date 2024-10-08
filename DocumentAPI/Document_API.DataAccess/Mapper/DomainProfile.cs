using AutoMapper;
using Document_API.DataAccess.Entity;
using Document_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.DataAccess.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Document, DocumentEntity>().ReverseMap();
            CreateMap<Photo, PhotoEntity>().ReverseMap();
        }
    }
}
