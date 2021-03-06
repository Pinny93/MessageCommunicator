﻿using System;
using System.Reactive.Concurrency;
using System.Runtime.InteropServices;
using System.Threading;
using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using MessageCommunicator.TestGui.Startup;
using ReactiveUI;

namespace MessageCommunicator.TestGui
{
    internal static class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            try
            {
                // Start avalonia logic
                BuildAvaloniaApp()
                    .StartWithClassicDesktopLifetime(args);
            }
            catch (Exception e)
            {
                CommonErrorHandling.Current.HandleFatalException(e);
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            RxApp.DefaultExceptionHandler = new DefaultReactiveUIExceptionHandler();

            return AppBuilder.Configure<App>()
                .SetStartupSystemSettings()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug()
                .HandleOSThemeChange();
        }
    }
}
