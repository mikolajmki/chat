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

        if(_messageService.SendMessage(message))
        {
            _chatHubContext.Clients.All.SendAsync(Notification.NewMessage);
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet(Route.GetMessages)]
    public ResponseGetMessages GetMessages()
    {
        var messageDtos = _messageService.GetMessages();

        var messages = _mapper.Map<List<MessageApiModel>>(messageDtos);

        var response = new ResponseGetMessages
        {
            Messages = messages
        };

        return response;
    }

    [HttpGet(Route.GetLatestMessage)]
    public IActionResult GetLatestMessage()
    {
        var messageDto = _messageService.GetLatestMessage();

        if (messageDto != null)
        {
            var message = _mapper.Map<MessageApiModel>(messageDto);

            var response = new ResponseGetLatestMessage
            {
                LatestMessage = message
            };

            return Ok(response);
        }

        return BadRequest();
    }

    [HttpPost(Route.Join)]
    public IActionResult Join([FromBody] RequestJoinChat request)
    {
        var user = _mapper.Map<UserDto>(request.User);

        if (_userService.AddUserToChat(user))
        {
            _chatHubContext.Clients.All.SendAsync(Notification.GetStats);

            return Ok();
        }

        return BadRequest();
    }

    [HttpPut(Route.Leave)]
    public IActionResult Leave([FromBody] RequestLeaveChat request)
    {
        _userService.DeactivateByConnectionId(request.GetUserConnectionId());
        _chatHubContext.Clients.All.SendAsync(Notification.GetStats);

        return Ok();
    }

    [HttpGet(Route.GetStats)]
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
    public const string GetLatestMessage = "GetLatestMessage";
    public const string GetStats = "GetStats";
    public const string Join = "Join";
    public const string Leave = "Leave";
}