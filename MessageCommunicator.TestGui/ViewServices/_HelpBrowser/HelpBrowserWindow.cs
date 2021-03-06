﻿using System.Reflection;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Markdown.Avalonia;

namespace MessageCommunicator.TestGui.ViewServices
{
    public partial class HelpBrowserWindow : OwnWindow<HelpBrowserViewModel>
    {
        public HelpBrowserWindow()
        {
            AvaloniaXamlLoader.Load(this);
            
            var markdownViewer = this.FindControl<MarkdownScrollViewer>("CtrlMarkdownViewer");
            markdownViewer.Engine.BitmapLoader = new HelpBitmapLoader(
                Assembly.GetExecutingAssembly(),
                "Assets/Docs/");
        }
    }
}