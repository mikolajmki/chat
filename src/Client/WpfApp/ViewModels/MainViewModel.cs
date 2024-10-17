using Core.Abstractions;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp.Common;

namespace WpfApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly HubConnection _chatHub;

    private readonly IMessageService _messageService;
    private readonly IUserService _userService;
    private readonly IStatsService _statsService;

    private string _nameValue = string.Empty;
    private string _messageContentValue = string.Empty;
    
    private bool _isNotJoined = true;
    private bool _isMessageBoxEnabled = false;

    private ObservableCollection<Message> _messages;
    private Stats _stats;

    public ICommand JoinChatCommand => new Command(JoinChat);
    public ICommand LeaveChatCommand => new Command(LeaveChat);
    public ICommand SendMessageCommand => new Command(SendMessage);

    private IsConnectedIndicator _isConnectedIndicator;
    public IsConnectedIndicator IsConnectedIndicator { get => _isConnectedIndicator; set => SetProperty(ref _isConnectedIndicator, value); }
    public bool IsNameTbEnabled { get => _isNotJoined; set => SetProperty(ref _isNotJoined, value); }
    public bool IsMessageTbEnabled { get => _isMessageBoxEnabled; set => SetProperty(ref _isMessageBoxEnabled, value); }
    public string NameValue { get => _nameValue; set => SetProperty(ref _nameValue, value); }
    public bool IsConnected { get => _isNotJoined; set => SetProperty(ref _isNotJoined, value); }
    public ObservableCollection<Message> Messages { get => _messages; set => SetProperty(ref _messages, value); }
    public Stats Stats { get => _stats; set => SetProperty(ref _stats, value); }
    
    public string MessageContentValue { get => _messageContentValue; set => SetProperty(ref _messageContentValue, value); }

    public MainViewModel(
        IWpfConfiguration config, 
        IMessageService messageService, 
        IUserService userService,
        IStatsService statsService)
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl(config.SignalRAddress)
            .WithAutomaticReconnect()
            .Build();

        _chatHub = hubConnection;
        _messageService = messageService;
        _userService = userService;
        _statsService = statsService;
        _isConnectedIndicator = IsConnectedIndicator.Disconnected();
        _messages = [];
        _stats = new ();

        GetMessages();
        StartSignalR();
        GetStats();
    }

    private async void StartSignalR()
    {
        var GetLatestMessageAction = new Action(GetMessages);
        var GetStatsAction = new Action(GetStats);

        _chatHub.On(Notification.NewMessage, GetLatestMessageAction);
        _chatHub.On(Notification.GetStats, GetStatsAction);

        await _chatHub.StartAsync();

    }

    private async void SendMessage(object commandParameter)
    {
        var message = new Message 
        { 
            Content = _messageContentValue, 
            User = new User { Name = _nameValue } 
        };

        await _messageService.SendMessage(message);

        MessageContentValue = string.Empty;
    }

    private async void JoinChat(object commandParameter)
    {
        var joined = await _userService.JoinChat(
            new User { Name = _nameValue, ConnectionId = _chatHub.ConnectionId }
        ).ConfigureAwait(false);

        IsNameTbEnabled = !joined;
        IsMessageTbEnabled = joined;

        if (joined) IsConnectedIndicator = IsConnectedIndicator.Connected();
    }

    private async void LeaveChat(object commandParameter)
    {
        await _userService.LeaveChat(
            new User { ConnectionId = _chatHub.ConnectionId }
        ).ConfigureAwait(false);

        IsNameTbEnabled = true;
        IsMessageTbEnabled = false;
        IsConnectedIndicator = IsConnectedIndicator.Disconnected();
    }
    private async void GetMessages()
    {
        var messages = await _messageService.GetMessages().ConfigureAwait(false);

        Messages = new ObservableCollection<Message>(messages);
    }

    private async void GetStats()
    {
        var stats = await _statsService.GetStats();

        Stats = new Stats
        {
            ActiveUsers = new ObservableCollection<User>(stats.ActiveUsers),
            InActiveUsers = new ObservableCollection<User>(stats.InActiveUsers),
            IsActiveCount = stats.IsActiveCount
        };
    }
}

public static class Notification
{
    public const string GetStats = "GetStats";
    public const string NewMessage = "NewMessage";
}

public sealed record IsConnectedIndicator
{
    public string Text { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;

    public static IsConnectedIndicator Connected()
    {
        return new IsConnectedIndicator { Text = "Connected", Color = "#FF126F13" };
    }
    public static IsConnectedIndicator Disconnected()
    {
        return new IsConnectedIndicator { Text = "Disconnected", Color = "red" };
    }
}