using AutoMapper;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Profiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<TodoItemCreateDto, TodoItem>();
        CreateMap<TodoItemUpdateDto, TodoItem>();
        CreateMap<TodoItem, TodoItemReadDto>();
    }
}
