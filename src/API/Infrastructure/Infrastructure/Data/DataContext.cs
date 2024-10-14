using Application.Domain;

namespace Infrastructure.Data;

internal class DataContext : IDataContext
{
    public IEnumerable<Message> Messages { get; set; } = [];
    public IEnumerable<User> Users { get; set; } = [];
}
