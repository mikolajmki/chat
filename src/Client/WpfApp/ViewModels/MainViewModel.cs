using Core.UseCases.GetStats;
using Core.UseCases.SendMessage;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Http;
using System.Collections;
using System.Windows.Input;
using WpfApp.Common;

namespace WpfApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IHubProxy _chatHub;

    private readonly ISendMessageUseCase _sendMessageUseCase;
    private readonly IGetStatsUseCase _getStatsUseCase;

    private string _nameValue = string.Empty;
    private string _messageContentValue = string.Empty;
    private bool _isConnected = false;

    private IEnumerable messages;

    public ICommand JoinChatCommand => new Command(JoinChat);
    public ICommand LeaveChatCommand => new Command(LeaveChat);
    public ICommand SendMessageCommand => new Command(SendMessage);

    public string NameValue { get => _nameValue; set => SetProperty(ref _nameValue, value); }
    public IEnumerable Messages { get => messages; set => SetProperty(ref messages, value); }
    
    public string MessageContentValue { get => _messageContentValue; set => SetProperty(ref _messageContentValue, value); }

    public MainViewModel(IHubProxy chatHub, ISendMessageUseCase getWeatherUseCase)
    {
        _chatHub = chatHub;
        _sendMessageUseCase = getWeatherUseCase;

        _chatHub.On(SignalRMethod.UserJoinedNotification, GetStats);
        _chatHub.On(SignalRMethod.IsSuccesfullyConnected, Connect);
        _chatHub.On(SignalRMethod.Disconnect, Disconnect);
        _chatHub.On(SignalRMethod.NewMessageNotification, GetLatestMessage);
    }
    public MainViewModel()
    {

    }
    private void GetStats(object commandParameter)
    {
    }
    private void GetLatestMessage(object commandParameter)
    {
    }
    private void SendMessage(object commandParameter)
    {
    }

    private async void JoinChat(object commandParameter)
    {
        var request = new { User = new { Name = _nameValue } };

        await _chatHub.Invoke(SignalRMethod.Connect, request);
    }

    private void Connect()
    {
        _isConnected = true;
    }
    private void Disconnect()
    {
        _isConnected = false;
    }

    private async void LeaveChat (object commandParameter)
    {
        await _chatHub.Invoke(SignalRMethod.Disconnect);
    }
}

public static class SignalRMethod
{
    public const string UserJoinedNotification = "UserJoinedNotification";
    public const string IsSuccesfullyConnected = "IsSuccesfullyConnected";
    public const string NewMessageNotification = "NewMessageNotification";
    public const string Connect = "Connect";
    public const string Disconnect = "Disconnect";
}