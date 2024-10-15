using Application.ApplicationModels;
using Application.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Presentation.Hubs;
using Presentation.PresentationModels;
using Presentation.PresentationModels.IO;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController(
        IMessageService _messageService,
        IUserService _userService,
        IStatsService _statsService,
        IHubContext<ChatHub> _chatHubContext,
        IMapper _mapper
    ) : ControllerBase
{
    [HttpPost(Route.SendMessage)]
    public IActionResult SendMessage([FromBody] RequestSendMessage request)
    {
        var message = _mapper.Map<MessageDto>(request.Message);

        _messageService.SendMessage(message);

        _chatHubContext.Clients.All.SendAsync("NewMessageNotification");

        return Ok();
    }

    [HttpGet(Route.GetMessages)]
    public ResponseGetMessages GetMessages()
    {
        var messageDtos = _messageService.GetMessages();

        var messages = _mapper.Map<IEnumerable<MessageApiModel>>(messageDtos);

        var response = new ResponseGetMessages
        {
            Messages = messages
        };

        return response;
    }

    [HttpPost(Route.Join)]
    public IActionResult Join([FromBody] RequestConnectUser request)
    {
        var user = _mapper.Map<UserDto>(request.User);

        if (_userService.AddUserToChat(user))
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet(Route.GetActiveStats)]
    public ResponseGetStats GetStats()
    {
        var statsDtos = _statsService.GetStats();
        var stats = _mapper.Map<StatsApiModel>(statsDtos);

        var response = new ResponseGetStats
        {
            Stats = stats
        };

        return response;
    }
}

internal class Route
{
    public const string SendMessage = "SendMessage";
    public const string GetMessages = "GetMessages";
    public const string GetActiveStats = "GetActiveStats";
    public const string Join = "Join";
}