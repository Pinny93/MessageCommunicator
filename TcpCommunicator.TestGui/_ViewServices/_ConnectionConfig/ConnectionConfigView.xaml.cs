﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TcpCommunicator.TestGui
{
    public class ConnectionConfigView : OwnWindow<ConnectionConfigViewModel>
    {
        public ConnectionConfigView()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
