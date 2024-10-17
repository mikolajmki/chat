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

        _messages = [ new Message { Content = "Test", CreatedAt = DateTime.UtcNow, User = new User { Name = "User" } }];
        _stats = new ();

        GetMessages();
        GetLatestMessage();
        StartSignalR();
    }

    private async void StartSignalR()
    {
        _chatHub.On<string>(Notification.GetStats, GetStats);
        //_chatHub.On<string>(Notification.NewMessage, GetLatestMessage());

        await _chatHub.StartAsync();
    }
    private async void GetMessages()
    {
        var messages = await _messageService.GetMessages();

        Messages = new ObservableCollection<Message>(messages);
    }

    private async void GetLatestMessage()
    {
        while (true)
        {
            var latestLocalMessage = Messages.Last();

            await Task.Delay(2000);
            var message = await _messageService.GetLatestMessage();

            if (latestLocalMessage.Id == message.Id)
            {
                continue;
            }

            Messages.Add(message);
        }
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
        );

        IsNameTbEnabled = !joined;
        IsMessageTbEnabled = joined;
    }

    private async void LeaveChat(object commandParameter)
    {
        await _userService.LeaveChat(
            new User { ConnectionId = _chatHub.ConnectionId }
        );

        IsNameTbEnabled = true;
        IsMessageTbEnabled = false;
    }
    private async void GetStats(object commandParameter)
    {
        var stats = await _statsService.GetStats().ConfigureAwait(false);

        _stats = stats;
    }
}

public static class Notification
{
    public const string GetStats = "GetStats";
    public const string NewMessage = "NewMessage";
}