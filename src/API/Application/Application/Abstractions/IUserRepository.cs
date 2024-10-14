using Application.Domain;

namespace Application.Abstractions;

public interface IUserRepository
{
    bool AddUser(User user);
    bool IsExisting(Guid userId);
    int GetCount();
}
