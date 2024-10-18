using Core.Abstractions;
using Core.Models;

namespace Core.Services;

internal class MessageService(
        IMessageRepository _messageRepository
    ) : IMessageService
{
    public async Task<Message> GetLatestMessage()
    {
        return await _messageRepository.GetLatestMessage();
    }

    public async Task<IEnumerable<Message>> GetMessages()
    {
        return await _messageRepository.GetMessages();
    }

    public async Task<bool> SendMessage(Message message)
    {
        return await _messageRepository.SendMessage(message);
    }
}
