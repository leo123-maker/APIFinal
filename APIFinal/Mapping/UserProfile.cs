using System;
using APIFinal.Models;
using APIFinal.Models.Dtos;
using AutoMapper;

namespace APIFinal.Mapping;

public class UserProfile: Profile
{
public UserProfile()
    {
    CreateMap<User, UserDto>().ReverseMap();
    CreateMap<User, CreateUserDto>().ReverseMap();
    CreateMap<User, UserLoginDto>().ReverseMap();
    CreateMap<User, UserLoginResponseDto>().ReverseMap();    
    }
}
