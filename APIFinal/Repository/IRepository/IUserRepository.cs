using System;
using APIFinal.Models;
using APIFinal.Models.Dtos;

namespace APIFinal.Repository.IRepository;

public interface IUserRepository
{
ICollection<User> GetUsers();

User? GetUser(int id);

bool IsUniqueUser(string name);

Task<UserLoginDto> login(UserLoginDto userLoginDto);

Task<User> Register(CreateUserDto createUserDto);
}
