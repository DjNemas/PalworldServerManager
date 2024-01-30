using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PalWorldServerManagerShared.Helper
{
    public class RegexHelper
    {
        public static bool IsTextAllowed(string text)
        {
            var regex = new Regex("[^0-9.-]+");
            return !regex.IsMatch(text);
        }
    }
}
