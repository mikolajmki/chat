using Application.ApplicationModels;
using Application.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Presentation.PresentationModels.IO;

namespace Presentation.Hubs;

public class ChatHub(
        IUserService _userService,
        IMapper _mapper
    ) : Hub
{
    public Task JoinChat(RequestJoinChat request)
    {
        var connectionId = Context.ConnectionId;

        var user = _mapper.Map<UserDto>(request.User);

        user.SetConnectionId(connectionId);

        var isAdded = _userService.AddUserToChat(user);

        Clients.Client(user.ConnectionId).SendAsync("", isAdded);

        return Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;

        _userService.DeactivateByConnectionId(connectionId);

        Clients.All.SendAsync(Notification.GetStats);

        return base.OnDisconnectedAsync(exception);
    }
}

public static class Notification
{
    public const string GetStats = "GetStats";
    public const string NewMessage = "NewMessage";
}