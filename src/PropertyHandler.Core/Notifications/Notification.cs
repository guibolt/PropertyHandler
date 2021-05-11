using PropertyHandler.Core.Enums;

namespace PropertyHandler.Core.Notifications
{
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
        }

        public Notification(string message, ETypeError errorType)
        {
            Message = message;
            ErrorType = errorType;
        }

        public string Message { get; }
        public ETypeError ErrorType { get; set; } = ETypeError.BadRequest;

    }
}
