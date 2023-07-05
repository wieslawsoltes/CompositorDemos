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
        RendererDiagnostics.DebugOverlays = RendererDebugOverlays.Fps;
        // RendererDiagnostics.DebugOverlays = RendererDebugOverlays.Fps | RendererDebugOverlays.LayoutTimeGraph | RendererDebugOverlays.RenderTimeGraph;
    }
}
