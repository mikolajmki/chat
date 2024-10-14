using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[Controller]
[Route("api/[controller]")]
public class ChatController(
        IMessageService _messageService
    ) : ControllerBase
{
    public async Task<IActionResult> SendMessage(RequestSendMessage request)
    {
        request
        return Ok();
    }
}
