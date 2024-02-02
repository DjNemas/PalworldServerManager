using PalWorldServerManagerShared.Definitions;

namespace PalWorldServerManagerShared.Model
{
    public class ToServerMessage<T>
    {
        public ToServerCommand Command { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
