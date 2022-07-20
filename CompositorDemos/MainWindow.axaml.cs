using Avalonia;
using Avalonia.Controls;

namespace CompositorDemos;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        Renderer.DrawFps = true;
        Renderer.DrawDirtyRects = false;
    }
}
