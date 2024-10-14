using Application.Domain;

namespace Application.Abstractions;

public interface IUserRepository
{
    void AddUser(User user);
    bool IsExisting(string name);
    Guid GetIdByName(string name);
    void Activate(string name);
    bool IsActive(Guid userId);
    int GetCount();
}
