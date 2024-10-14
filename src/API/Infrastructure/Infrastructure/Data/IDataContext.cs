using Application.Domain;

namespace Infrastructure.Data;

public interface IDataContext
{
    public IEnumerable<Message> Messages { get; set; }
    public IEnumerable<User> Users { get; set; }
}
