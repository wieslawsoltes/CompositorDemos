using Avalonia;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;
using Avalonia.VisualTree;

namespace CompositorDemos.Pages;

public partial class ExpressionAnimationPage : UserControl
{
    public ExpressionAnimationPage()
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

        if (this.GetVisualRoot() is not Visual visualRoot)
        {
            return;
        }
        
        var compositionVisualWindow = ElementComposition.GetElementVisual(visualRoot);
        if (compositionVisualWindow is null)
        {
            return;
        }

        var compositor = compositionVisual.Compositor;

        var animation = compositor.CreateExpressionAnimation("this.Target.Size.X / Window.Size.X");
        animation.SetReferenceParameter("Window", compositionVisualWindow);

        compositionVisual.StartAnimation("Opacity", animation);
    }
}
