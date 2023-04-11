using System;
using System.Numerics;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Animations;

namespace CompositorDemos.Pages;

public static class Animator
{
    public static void SetLeftSlidingAnimation(Control element, double milliseconds)
    {
        element.Loaded += (_, _) =>
        {
            ApplySlidingPageAnimation(element, -element.Bounds.Width, 0, TimeSpan.FromMilliseconds(milliseconds));
        };
    }

    public static void SetRightSlidingAnimation(Control element, double milliseconds)
    {
        element.Loaded += (_, _) =>
        {
            ApplySlidingPageAnimation(element, 2 * element.Bounds.Width, 0, TimeSpan.FromMilliseconds(milliseconds));
        };
    }

    public static void SetTopSlidingAnimation(Control element, double milliseconds)
    {
        element.Loaded += (_, _) =>
        {
            ApplySlidingPageAnimation(element, 0, -element.Bounds.Height, TimeSpan.FromMilliseconds(milliseconds));
        };
    }

    public static void SetBottomSlidingAnimation(Control element, double milliseconds)
    {
        element.Loaded += (_, _) =>
        {
            ApplySlidingPageAnimation(element, 0, 2 * element.Bounds.Height, TimeSpan.FromMilliseconds(milliseconds));
        };
    }

    private static void ApplySlidingPageAnimation(Visual visual, double offsetX, double offsetY, TimeSpan duration)
    {
        var compositionVisual = ElementComposition.GetElementVisual(visual);
        if (compositionVisual is null)
        {
            return;
        }

        var compositor = compositionVisual.Compositor;

        var offsetAnimation = compositor.CreateVector3KeyFrameAnimation();
        offsetAnimation.InsertKeyFrame(0.0f, new Vector3((float)offsetX, (float)offsetY, 0));
        offsetAnimation.InsertKeyFrame(1.0f, new Vector3(0, 0, 0));
        offsetAnimation.Direction = PlaybackDirection.Normal;
        offsetAnimation.Duration = duration;
        offsetAnimation.IterationBehavior = AnimationIterationBehavior.Count;
        offsetAnimation.IterationCount = 1;

        compositionVisual.StartAnimation("Offset", offsetAnimation);
    }
}
