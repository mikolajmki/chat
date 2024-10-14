using Application.Domain;

namespace Application.Abstractions;

public interface IUserRepository
{
    bool AddUser(User user);
    bool IsExisting(string name);
    User GetByName(string name);
    bool Activate(string name);
    bool IsActive(Guid userId);
    int GetCount();
}
