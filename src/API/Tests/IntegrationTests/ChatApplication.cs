using Application.Domain;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tests.IntegrationTests;

public class ChatApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var old = services.First(x => x.ServiceType == typeof(DataContext));
            services.Remove(old);

            var dummyContext = new DummyDataContext();

            var newContext = new DataContext
            {
                Messages = dummyContext.Messages,
                Users = dummyContext.Users,
            };

            services.AddSingleton(newContext);
        });

        return base.CreateHost(builder);
    }
}
public class DummyDataContext
{
    public static readonly Guid TestUserGuid = Guid.NewGuid();
    public List<User> Users { get; set; } = [];
    public List<Message> Messages { get; set; } = [];

    public static User User { get; } = new User
    {
        Name = "Bob",
        ConnectionId = "connection-id",
        IsActive = true,
    };

    public static Message Message { get; } = new Message
    {
        Content = "Content",
    };

    public DummyDataContext()
    {
        User.SetId(TestUserGuid);

        Users = [User];

        Message.SetUser(User);

        Messages = [Message];
    }
}
