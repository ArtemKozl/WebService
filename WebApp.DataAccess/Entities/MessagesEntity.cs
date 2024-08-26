
namespace WebApp.DataAccess.Entities
{
    public class MessagesEntity
    {
        public int id { get; set; }
        public int serialnumber { get; set; }
        public string message { get; set; } = string.Empty;
        public DateTime sendtime { get; set; }
    }
}
