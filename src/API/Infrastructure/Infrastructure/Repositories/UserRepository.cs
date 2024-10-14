using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class UserRepository(
        DataContext _context
    ) : IUserRepository
{
    public bool Activate(string name)
    {
        var user = _context.Users.Single(x => x.Name == name);

        var newUser = new User
        {
            Id = user.Id,
            Name = user.Name,
            IsActive = true,
        };

        var list = _context.Users.ToList();

        list.Remove(user);
        list.Add(newUser);

        _context.Users = list;

        var isActivated = _context.Users.Single(x => x.Name == name).IsActive;
        
        return isActivated;
    }

    public bool AddUser(User user)
    {
        var count = _context.Users.Count();

        _ = _context.Users.Append(user);

        var newCount = _context.Users.Count();

        return newCount > count;
    }

    public User GetByName(string name)
    {
        return _context.Users.Single(x => x.Name == name);
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
