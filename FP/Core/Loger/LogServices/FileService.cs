using Loger.Interfaces;

namespace Loger.LogServices;

internal class FileService<T> : ActionLoger, IFileLog
{
    readonly object locker = new object();
    readonly string LogFilePath = "LogFile.txt";

    public async Task WriteLog(string message)
    {
        bool writeSucceeded = false;

        while (!writeSucceeded)
        {
            try
            {
                lock (locker)
                {
                    using (var writer = File.AppendText(LogFilePath))
                    {
                        writer.WriteLine(message);
                    }
                }

                writeSucceeded = true;
            }
            catch
            {
                await Task.Delay(100);
            }
        }
    }

    public async Task<string> ReadLogs()
    {
        lock (locker)
        {
            return "";
        }
    }

    public override void Log(string message)
    {
        _ = WriteLog(message);
    }
}
