using Loger.Enums;
using Loger.Interfaces;
using Loger.LogServices;

namespace Loger;

public class LogService<T>
{
    private readonly string? NameSpace;
    private List<ActionLoger> _logers = new List<ActionLoger>()
    {
        new FileService<T>(),
        new ConsoleService(),
    };

    public LogService() => NameSpace = typeof(T).FullName;

    public async Task LogAction(string message, LogType logType = LogType.Addition, short trace = 100, Exception exception = null)
    {
        string logMessage = CastMessage(message, logType, trace, exception);
        foreach (var item in _logers)
        {
            item.Log(logMessage);
        }
    }

    private string CastMessage(string message, LogType logType = LogType.Inforamation, short trace = 000, Exception exception = null)
    {
        string logMessage = "";
        if (logType == LogType.Addition)
            logMessage += $"{DateTime.Now} | {trace} |\t\t\t-> {message}";
        else
            logMessage = exception == null ?
                $"{DateTime.Now} | {trace} | {logType} | {NameSpace} | {message}" :
                $"{DateTime.Now} | {trace} | {logType} | {NameSpace} | {message} | {exception}";

        return logMessage;
    }
}
