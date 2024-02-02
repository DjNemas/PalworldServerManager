using PalWorldServerManagerShared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PalWorldServerManagerShared.Extensions
{
    public static class MessageExtension
    {
        public static string ToJsonString(this Message message)
            => JsonSerializer.Serialize(message);
    }
}
