using Application.Domain;

namespace Infrastructure.Data;

public class DataContext
{
    public List<Message> Messages { get; set; } = [];
    public List<User> Users { get; set; } = [];

    public DataContext()
    {
        for (int i = 0; i < 10; i++)
        {
            var user = new User { Name = "Name" };
            user.GenerateId();

            var message = new Message { Content = "Content" };

            message.GenerateId();
            message.SetDateTimeNow();
            message.SetUser(user);

            Users.Add(user);
            Messages.Add(message);
        }
    }
}
