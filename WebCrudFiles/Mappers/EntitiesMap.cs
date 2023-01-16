using AutoMapper;
using WebCrudFiles.DTOs;
using File = WebCrudFiles.Entities.File;

namespace WebCrudFiles.Mappers
{
    public class EntitiesMap:Profile
    {
        public EntitiesMap()
        {
            CreateMap<File, GetAllFiles>().ReverseMap();
            CreateMap<File, GetFileDto>().ReverseMap();
            CreateMap<CreateFileDto, File>().ReverseMap();
            CreateMap<UpdateFileDto, File>().ReverseMap();
        }
    }
}
