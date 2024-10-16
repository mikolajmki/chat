using Core.Models;
using Core.Repositories.IO;

namespace Application.Abstractions;

public interface IMessageRepository
{
    Task SendMessage(SendMessageRequest request);
    Task<GetLatestMessageResponse> GetLatestMessage();
    Task<GetMessagesResponse> GetMessages();
}
