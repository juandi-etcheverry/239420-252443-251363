using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class UserRepository : IUserRepository
{
    private readonly DbContext _context;

    public UserRepository(DbContext context)
    {
        _context = context;
    }

    public User AddUser(User user)
    {
        _context.Set<User>().Add(user);
        _context.SaveChanges();
        return user;
    }

    public User GetUser(Guid id)
    {
        var user = _context.Set<User>().Find(id);
        if (user == null) throw new ArgumentException($"User with id {id} not found");
        return user;
    }

    public User GetUser(string email, string password)
    {
        var user = _context.Set<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        if (user is null) throw new UnauthorizedAccessException($"User with email {email} not found");
        return user;
    }

    public User SoftDelete(Guid id)
    {
        var user = _context.Set<User>().Find(id);
        if (user == null) throw new ArgumentException($"User with id {id} not found");
        user.IsDeleted = true;
        _context.SaveChanges();
        return user;
    }

    public User UpdateUser(Guid id, User user)
    {
        var userToModify = GetUser(id);
        userToModify.Address = user.Address;
        userToModify.Email = user.Email;
        userToModify.Role = user.Role;
        _context.Set<User>().Update(userToModify);
        _context.SaveChanges();
        return userToModify;
    }

    public bool FindUser(string email)
    {
        var user = _context.Set<User>().Where(u => u.Email == email).FirstOrDefault();
        if (user == null) return false;
        return true;
    }

    public IList<User> GetAllUsers()
    {
        return _context.Set<User>().Where(user => !user.IsDeleted).ToList();
    }
}