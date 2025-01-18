using AutoMapper;
using TaskShare.Application.DTO;
using TaskShare.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskShare.Application.Mappers;

public class TaskListMappingProfile : Profile
{
    public TaskListMappingProfile()
    {
        CreateMap<TaskList, TaskListDto>().ReverseMap();
    }
}