using System;
using System.Numerics;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;

namespace CompositorDemos.Pages;

public partial class Vector3KeyFrameAnimationPage : UserControl
{
    public Vector3KeyFrameAnimationPage()
    {
        InitializeComponent();

        Border.AttachedToVisualTree += BorderOnAttachedToVisualTree;
    }

    private void BorderOnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        var compositionVisual = ElementComposition.GetElementVisual(Border);
        if (compositionVisual is null)
        {
            return;
        }

        var compositor = compositionVisual.Compositor;

        var animation = compositor.CreateVector3KeyFrameAnimation();
        animation.InsertKeyFrame(1f, new Vector3(200f, 0f, 0f));
        animation.Duration = TimeSpan.FromSeconds(2);
        animation.Direction = PlaybackDirection.Alternate;
        animation.IterationCount = int.MaxValue;

        compositionVisual.StartAnimation("Offset", animation);
    }
}
