using Application.Domain;

namespace Infrastructure.Data;

internal class DataContext : IDataContext
{
    public List<Message> Messages { get; set; } = [];
    public List<User> Users { get; set; } = [];
}
