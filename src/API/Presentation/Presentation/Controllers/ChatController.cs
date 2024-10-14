using Application.ApplicationModels;
using Application.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[Controller]
[Route("api/[controller]")]
public class ChatController(
        IMessageService _messageService,
        IMapper _mapper
    ) : ControllerBase
{
    [HttpPost]
    public IActionResult SendMessage(RequestSendMessage request)
    {
        var command = _mapper.Map<SendMessageCommand>(request);

        if (_messageService.SendMessage(command))
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetMessages()
    {
        _messageService.GetMessages();

        return Ok();
    }
}
