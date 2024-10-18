using Application.Domain;

namespace Application.Abstractions;

public interface IUserRepository
{
    void AddUser(User user);
    bool IsExisting(string name);
    User GetUserByName(string name);
    void Activate(User user);
    bool IsActive(Guid userId);
    void DeactivateByConnectionId(string name);
}
