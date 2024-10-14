using Application.Domain;

namespace Infrastructure.Data;

internal class DataContext : IDataContext
{
    public IEnumerable<Message> Messages => [];

    public IEnumerable<User> Users => [];
}
