﻿using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalworldServerManagerClient.Definitions
{
    internal interface IShowPopup
    {
        void ShowErrorMessage(string title, string message);
    }
}
