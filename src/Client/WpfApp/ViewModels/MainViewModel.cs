using Core.Abstractions;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Input;
using WpfApp.Common;

namespace WpfApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly HubConnection _chatHub;

    private readonly IMessageService _messageService;
    private readonly IStatsService _statsService;

    private string _nameValue = string.Empty;
    private string _messageContentValue = string.Empty;
    private bool _isConnected = false;

    private List<Message> _messages;
    private Stats _stats;

    public ICommand JoinChatCommand => new Command(JoinChat);
    public ICommand LeaveChatCommand => new Command(LeaveChat);
    public ICommand SendMessageCommand => new Command(SendMessage);

    public string NameValue { get => _nameValue; set => SetProperty(ref _nameValue, value); }
    public bool IsConnected { get => _isConnected; set => SetProperty(ref _isConnected, value); }
    public List<Message> Messages { get => _messages; set => SetProperty(ref _messages, value); }
    public Stats Stats { get => _stats; set => SetProperty(ref _stats, value); }
    
    public string MessageContentValue { get => _messageContentValue; set => SetProperty(ref _messageContentValue, value); }

    public MainViewModel(IWpfConfiguration config, IMessageService messageService, IStatsService statsService)
    {

        var hubConnection = new HubConnectionBuilder()
            .WithUrl(config.SignalRAddress)
            .WithAutomaticReconnect()
            .Build();

        _chatHub = hubConnection;
        _messageService = messageService;
        _statsService = statsService;

        _messages = [];
        _stats = new ();

        _chatHub.On<string>(SignalRMethod.UserJoinedNotification, GetStats);
        _chatHub.On(SignalRMethod.IsSuccesfullyConnected, Connect);
        _chatHub.On(SignalRMethod.Disconnect, Disconnect);
        _chatHub.On<string>(SignalRMethod.NewMessageNotification, GetLatestMessage);

        StartSignalR();
    }

    private async void StartSignalR()
    {
        await _chatHub.StartAsync();
    }

    private async void GetStats(object commandParameter)
    {
        var stats = await _statsService.GetStats();

        _stats = stats;
    }
    private async void GetLatestMessage(object commandParameter)
    {
        var message = await _messageService.GetLatestMessage();

        _messages.Add(message);
    }
    private async void SendMessage(object commandParameter)
    {
        var message = new Message 
        { 
            Content = _messageContentValue, 
            User = new User { Name = _nameValue } 
        };

        await _messageService.SendMessage(message);
    }

    private async void JoinChat(object commandParameter)
    {
        var request = new { User = new { Name = _nameValue } };

        await _chatHub.InvokeAsync(SignalRMethod.Connect, request);
    }

    private async void LeaveChat(object commandParameter)
    {
        await _chatHub.InvokeAsync(SignalRMethod.Disconnect);
    }

    private void Connect()
    {
        _isConnected = true;
    }
    private void Disconnect()
    {
        _isConnected = false;
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