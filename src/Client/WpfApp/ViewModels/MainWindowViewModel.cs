using Core.UseCases.SendMessage;
using System.Windows.Input;
using WpfApp.Common;

namespace WpfApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ISendMessageUseCase _sendMessageUseCase;

    private string _nameValue = string.Empty;
    private string _messageContentValue = string.Empty;
    public ICommand JoinChatCommand => new Command(JoinChat);
    public ICommand LeaveChatCommand => new Command(LeaveChat);
    public ICommand SendMessageCommand => new Command(SendMessage);

    public string NameValue
    {
        get { return _nameValue; }
        set
        {
            _nameValue = value;
            OnPropertyChanged();
        }
    }
    public string MessageContentValue
    {
        get { return _messageContentValue; }
        set
        {
            _messageContentValue = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel(ISendMessageUseCase getWeatherUseCase)
    {
        _sendMessageUseCase = getWeatherUseCase;
    }
    public MainWindowViewModel()
    {
    }


    private void SendMessage(object commandParameter)
    {
    }

    private void JoinChat(object commandParameter)
    {
    }

    private void LeaveChat(object commandParameter)
    {
    }
}