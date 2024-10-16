using Application.Abstractions;
using Core.Models;
using Core.Repositories.IO;

namespace Core.Services;

internal class MessageService(
        IMessageRepository _messageRepository
    ) : IMessageService
{
    public async Task<Message> GetLatestMessage()
    {
        var response = await _messageRepository.GetLatestMessage();
        var message = response.LatestMessage;

        return message;
    }

    public async Task<IEnumerable<Message>> GetMessages()
    {
        var response = await _messageRepository.GetMessages();
        var messages = response.Messages;

        return messages;
    }

    public async Task SendMessage(Message message)
    {
        var request = new SendMessageRequest { Message = message };

        await _messageRepository.SendMessage(request);
    }
}
