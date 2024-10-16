using Core.Abstractions;

namespace WpfApp.Configuration;

internal class WpfConfiguration : IWpfConfiguration
{
    public string ChatControllerAddress { get => "https://localhost:7062/api/Chat/"; }
    public string SignalRAddress { get => "https://localhost:7062/api/ChatHub"; }
}
