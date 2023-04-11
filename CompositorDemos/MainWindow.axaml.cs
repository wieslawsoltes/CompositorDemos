using Avalonia;
using Avalonia.Controls;
using Avalonia.Rendering;

namespace CompositorDemos;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        Renderer.Diagnostics.DebugOverlays = RendererDebugOverlays.Fps;
        // Renderer.Diagnostics.DebugOverlays = RendererDebugOverlays.Fps | RendererDebugOverlays.LayoutTimeGraph | RendererDebugOverlays.RenderTimeGraph;
    }
}
