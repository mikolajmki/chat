using Application.Domain;

namespace Application.Abstractions;

public interface IUserRepository
{
    void AddUser(User user);
    bool IsExisting(string name);
    Guid GetUserIdByName(string name);
    void Activate(Guid userId);
    bool IsActive(Guid userId);
    void DeactivateByConnectionId(string connectionId);
    void RemoveFromChatByConnectionId(string id);
}
