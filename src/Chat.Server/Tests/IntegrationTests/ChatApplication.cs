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
    public static readonly Guid TestUserGuid = Guid.Parse("e5d906ce-51f2-44f8-a2c7-2be0f2781008");
    public static readonly Guid TestMessageGuid = Guid.Parse("c0e2957c-d2bf-4b83-9944-6bfd4c539a9e");
    public static readonly DateTime TestMessageCreatedAt = DateTime.Parse("2025/05/11 14:27:53");
    public List<User> Users { get; set; } = [];
    public List<Message> Messages { get; set; } = [];

    public User User { get; } = new User
    {
        Name = "Bob",
    };

    public Message Message { get; } = new Message
    {
        Content = "Content",
    };

    public DummyDataContext()
    {
        User.SetId(TestUserGuid);
        User.SetConnectionId("some-connection-id");
        User.SetIsActive(false);

        Users = [User];

        Message.SetUser(User);
        Message.SetId(TestMessageGuid);
        Message.SetCreatedAt(TestMessageCreatedAt);

        Messages = [Message];
    }
}
