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
    public bool Connect(RequestConnectUser request)
    {
        var connectionId = Context.ConnectionId;

        var user = _mapper.Map<UserDto>(request.User);

        user.SetConnectionId(connectionId);

        var isAdded = _userService.AddUserToChat(user);

        return isAdded;
    }

    public override Task OnConnectedAsync()
    {
        Clients.All.SendAsync("UserJoinedNotification");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;

        _userService.DeactivateUserByConnectionId(connectionId);

        return base.OnDisconnectedAsync(exception);
    }
}