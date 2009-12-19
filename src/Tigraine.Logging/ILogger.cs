namespace Tigraine.Logging
{
    public interface ILogger
    {
        void Debug(string message, params object[] parameters);
        void Information(string message, params object[] parameters);
        void Warning(string message, params object[] parameters);
        void Error(string message, params object[] parameters);
    }
}