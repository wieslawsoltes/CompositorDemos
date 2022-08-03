﻿using System;
using Avalonia;

namespace CompositorDemos;

class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .With(new Win32PlatformOptions { UseCompositor = true })
            .With(new X11PlatformOptions { UseCompositor = true })
            .With(new AvaloniaNativePlatformOptions { UseCompositor = true })
            .LogToTrace();
}
