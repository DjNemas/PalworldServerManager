using PalWorldServerManagerShared.Definitions;

namespace PalWorldServerManagerShared.Model
{
    public class Message
    {
        public Command Command { get; set; }
        public string? JsonData { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
