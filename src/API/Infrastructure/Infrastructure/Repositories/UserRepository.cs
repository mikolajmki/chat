using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class UserRepository(
        DataContext _context
    ) : IUserRepository
{
    public void Activate(string name)
    {
        var user = _context.Users.Single(x => x.Name == name);

        var newUser = new User
        {
            Id = user.Id,
            Name = user.Name,
            IsActive = true,
        };

        _context.Users.Remove(user);
        _context.Users.Add(newUser);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public Guid GetIdByName(string name)
    {
        return _context.Users.Single(x => x.Name == name).Id;
    }

    public int GetCount()
    {
        return _context.Users.Distinct().Count();
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
}
