using Application.Domain;

namespace Infrastructure.Data;

public interface IDataContext
{
    public List<Message> Messages { get; set; }
    public List<User> Users { get; set; }
}
