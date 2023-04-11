using System;
using System.Numerics;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Reactive;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Animations;

namespace CompositorDemos.Pages;

public partial class GalaxyPage : UserControl
{
    private CompositionVisual? _orbitVisual;

    public GalaxyPage()
    {
        InitializeComponent();

        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        var compositor = ElementComposition.GetElementVisual(this)?.Compositor;
        if (compositor is null)
        {
            return;
        }

        _orbitVisual = ElementComposition.GetElementVisual(Orbit);
        if (_orbitVisual is null)
        {
            return;
        }

        var orbitAnimation = compositor.CreateScalarKeyFrameAnimation();
        orbitAnimation.Duration = TimeSpan.FromSeconds(10);
        orbitAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
        orbitAnimation.InsertKeyFrame(1f, (float)(4.0 * Math.PI), new LinearEasing());
        _orbitVisual.CenterPoint = new Vector3((float)Bounds.Width / 2, (float)Bounds.Height / 2, 0);
        _orbitVisual.StartAnimation(nameof(CompositionVisual.RotationAngle), orbitAnimation);

        var planetVisual = ElementComposition.GetElementVisual(Planet);
        if (planetVisual is null)
        {
            return;
        }
        planetVisual.TransformMatrix = Matrix4x4.CreateTranslation(new Vector3(100, 0, 0));

        var satelliteVisual = ElementComposition.GetElementVisual(Satellite);
        if (satelliteVisual is null)
        {
            return;
        }
        satelliteVisual.TransformMatrix = Matrix4x4.CreateTranslation(new Vector3(30, 0, 0));
        satelliteVisual.CenterPoint = new Vector3((float)Satellite.Bounds.Width / 2, (float)Satellite.Bounds.Height / 2, 0);

        var satelliteAnimation = compositor.CreateExpressionAnimation();
        satelliteAnimation.Expression = "3 * orbitVisual.RotationAngle";
        satelliteAnimation.SetReferenceParameter("orbitVisual", _orbitVisual);
        satelliteVisual.StartAnimation(nameof(CompositionVisual.RotationAngle), satelliteAnimation);

        var starsVisual = ElementComposition.GetElementVisual(StarField);
        if (starsVisual is null)
        {
            return;
        }

        var starsAnimation = compositor.CreateExpressionAnimation();
        starsAnimation.Expression = "Max(0.3, Abs(Cos(ToDegrees(orbitVisual.RotationAngle) * 0.02)))";
        starsAnimation.SetReferenceParameter("orbitVisual", _orbitVisual);
        starsVisual.StartAnimation(nameof(CompositionVisual.Opacity), starsAnimation);

        this.GetObservable(BoundsProperty).Subscribe(new AnonymousObserver<Rect>(
            _ =>
            {
                // _orbitVisual.CenterPoint = new Vector3((float)Bounds.Width / 2, (float)Bounds.Height / 2, 0);
                // satelliteVisual.CenterPoint = new Vector3((float)Satellite.Bounds.Width / 2, (float)Satellite.Bounds.Height / 2, 0);
            }));
    }
}
