using Application.Domain;

namespace Infrastructure.Data;

public class DataContext
{
    public List<Message> Messages { get; set; } = [];
    public List<User> Users { get; set; } = [];

    public DataContext()
    {
        Seed();
    }

    private void Seed()
    {
        for (int i = 0; i < 10; i++)
        {
            var user = new User { Name = "Name" + i.ToString() };
            user.GenerateId();

            var message = new Message { Content = new string('a', 150) };

            if (i > 5) { user.SetIsActive(false); }

            message.GenerateId();
            message.SetCreatedAtNow();
            message.SetUser(user);

            Users.Add(user);
            Messages.Add(message);
        }
    }
}
