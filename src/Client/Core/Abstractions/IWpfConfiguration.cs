namespace Core.Abstractions;

public interface IWpfConfiguration
{
    public string ChatControllerAddress { get; }
    public string SignalRAddress { get; }
}
