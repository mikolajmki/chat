using Application.ApplicationModels;
using Application.Services;
using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using Presentation.PresentationModels.IO;

namespace Presentation.Hubs;

public class ChatHub(
        IUserService _userService,
        IMapper _mapper
    ) : Hub
{
    public Task Connect(RequestConnectUser request)
    {
        var connectionId = Context.ConnectionId;

        var user = _mapper.Map<UserDto>(request.User);

        user.SetConnectionId(connectionId);

        var isAdded = _userService.AddUserToChat(user);

        Clients.Client(user.ConnectionId).SendAsync(SignalRMethod.IsSuccesfullyConnected, isAdded);

        return Task.CompletedTask;
    }
    public Task Disconnect()
    {
        var connectionId = Context.ConnectionId;

        _userService.DeactivateUserByConnectionId(connectionId);

        return Task.CompletedTask;
    }

    public override Task OnConnectedAsync()
    {
        Clients.All.SendAsync(SignalRMethod.UserJoinedNotification);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;

        _userService.DeactivateUserByConnectionId(connectionId);

        return base.OnDisconnectedAsync(exception);
    }
}

public static class SignalRMethod
{
    public const string UserJoinedNotification = "UserJoinedNotification";
    public const string NewMessageNotification = "NewMessageNotification";
    public const string IsSuccesfullyConnected = "IsSuccesfullyConnected";
}