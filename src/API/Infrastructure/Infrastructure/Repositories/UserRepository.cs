using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class UserRepository(
        IDataContext _context
    ) : IUserRepository
{
    public bool AddUser(User user)
    {
        var count = _context.Users.Count();

        _ = _context.Users.Append(user);

        var newCount = _context.Users.Count();

        return newCount > count;
    }

    public int GetCount()
    {
        return _context.Users.Distinct().Count();
    }

    public bool IsExisting(Guid userId)
    {
        var user = _context.Users.SingleOrDefault(x => x.Id == userId);

        return user != null;
    }
}
