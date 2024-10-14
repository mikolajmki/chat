using Application.Domain;

namespace Infrastructure.Data;

public interface IDataContext
{
    public IEnumerable<Message> Messages { get; }
    public IEnumerable<User> Users { get; }
}
