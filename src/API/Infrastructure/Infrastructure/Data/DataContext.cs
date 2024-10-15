using Application.Domain;

namespace Infrastructure.Data;

public class DataContext
{
    public List<Message> Messages { get; set; } = [];
    public List<User> Users { get; set; } = [];
}
