
namespace WebApp.Core.Models
{
    public class Messages
    {
        public const int MAX_MESSAGE_LENGHT = 128;

        private Messages(int id, int serialNumber, string message, DateTime sendTime) 
        {
            Id = id;
            SerialNumber = serialNumber;
            Message = message;
            SendTime = sendTime;
        }

        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SendTime { get; set; }


    }
}
