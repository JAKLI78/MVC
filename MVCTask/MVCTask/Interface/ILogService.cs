namespace MVCTask.Interface
{
    public interface ILogService
    {
        void Log(string logMessage);
        string GetLog();
    }
}