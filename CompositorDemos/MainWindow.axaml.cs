using System;
using System.Numerics;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;

namespace CompositorDemos;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        
        Border.AttachedToVisualTree += BorderOnAttachedToVisualTree;
    }

    private void BorderOnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        var redVisual = ElementComposition.GetElementVisual(Border);
        if (redVisual is null)
        {
            return;
        }

        var compositor = redVisual.Compositor;

        var animation = compositor.CreateVector3KeyFrameAnimation();
        animation.InsertKeyFrame(1f, new Vector3(200f, 0f, 0f));
        animation.Duration = TimeSpan.FromSeconds(2);
        animation.Direction = PlaybackDirection.Alternate;
        animation.IterationCount = 10;
        redVisual.StartAnimation("Offset", animation);
    }
}
