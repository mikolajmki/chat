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
    public void Activate(User user)
    {
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

    public bool IsExisting(string name)
    {
        var user = _context.Users.SingleOrDefault(x => x.Name == name);

        return user != null;
    }

    public User GetUserByName(string name)
    {
        return _context.Users.Single(x => x.Name == name);
    }

    public void DeactivateByConnectionId(string connectionId)
    {
        var user = _context.Users.SingleOrDefault(x => x.ConnectionId == connectionId && x.IsActive);

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
        };

        newUser.SetId(user.Id);
        newUser.SetConnectionId(user.ConnectionId);
        newUser.SetIsActive(isActive);

        _context.Users.Remove(user);
        _context.Users.Add(newUser);
    }
}
