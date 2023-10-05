using Domain;

namespace DataAccess.Interfaces;

public interface IUserRepository
{
    public User AddUser(User user);
    public User GetUser(Guid id);
    public User GetUser(string email, string password);
    public User SoftDelete(Guid id);
    public User UpdateUser(Guid id, User user);
    public bool FindUser(string email);
}