using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class UserRepository(
        DataContext _context
    ) : IUserRepository
{
    public void Activate(Guid userId)
    {
        var user = _context.Users.Single(x => x.Id == userId);

        ToggleActive(user, true);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public bool IsActive(Guid userId)
    {
        var user = _context.Users.Single(x => x.Id == userId);

        return user.IsActive;
    }

    public bool IsExisting(string name)
    {
        var user = _context.Users.SingleOrDefault(x => x.Name == name);

        return user != null;
    }

    public Guid GetUserIdByName(string name)
    {
        return _context.Users.Single(x => x.Name == name).Id;
    }

    public void DeactivateByConnectionId(string connectionId)
    {
        var user = _context.Users.Single(x => x.ConnectionId == connectionId);

        ToggleActive(user, false);
    }

    private void ToggleActive(User user, bool isActive)
    {
        var newUser = new User
        {
            Name = user.Name,
            IsActive = isActive,
        };

        newUser.SetId(user.Id);

        _context.Users.Remove(user);
        _context.Users.Add(newUser);
    }
}
