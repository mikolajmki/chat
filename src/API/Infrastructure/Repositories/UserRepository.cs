using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

internal class UserRepository(
        DataContext _context,
        ILogger<UserRepository> _logger
    ) : IUserRepository
{
    public void Activate(Guid userId)
    {
        var user = _context.Users.Single(x => x.Id == userId);

        _logger.LogInformation(
            @"User with name {userName} of connectionId {connectionId} reconnected",
            user.Name,
            user.ConnectionId
        );

        ToggleActive(user, true);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);

        _logger.LogInformation(
            @"User with name {userName} of connectionId {connectionId} connected",
            user.Name,
            user.ConnectionId
        );
    }

    public void RemoveFromChatByConnectionId(string id)
    {
        throw new NotImplementedException();
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
        var user = _context.Users.SingleOrDefault(x => x.ConnectionId == connectionId);

        if (user != null)
        {
            ToggleActive(user, false);

            _logger.LogInformation(
                @"User with name {userName} of connectionId {connectionId} disconnected",
                user.Name,
                user.ConnectionId
            );
        }
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
