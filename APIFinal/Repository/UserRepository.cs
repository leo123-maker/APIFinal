using System;
using APIFinal.Models;
using APIFinal.Models.Dtos;
using APIFinal.Repository.IRepository;

namespace APIFinal.Repository;

public class UserRepository : IUserRepository
{
    public readonly AplicationDBContext _db;

    public UserRepository(AplicationDBContext db)
    {
        _db = db;
    }
    public User? GetUser(int id)
    {
        return _db.Users.FirstOrDefault(u => u.Id == id);
    }

    public ICollection<User> GetUsers()
    {
        return _db.Users.OrderBy(u => u.UserName).ToList();
    }

    public bool IsUniqueUser(string name)
    {
        return !_db.Users.Any(u => u.UserName.ToLower().Trim() == name.ToLower().Trim());
    }

    public Task<UserLoginDto> login(UserLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }

    public Task<User> Register(CreateUserDto createUserDto)
    {
        throw new NotImplementedException();
    }
}
