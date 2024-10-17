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