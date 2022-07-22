using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Interactivity;
using Avalonia.Rendering.Composition;
using Avalonia.VisualTree;
using CompositorDemos.Models;

namespace CompositorDemos.Pages;

public partial class ParallaxScrollingPage : UserControl
{
    public ParallaxScrollingPage()
    {
        InitializeComponent();

        ThumbnailList.ItemContainerGenerator.Materialized += ItemContainerGeneratorOnMaterialized;

        ThumbnailList.Items = new List<Thumbnail>
        {
            new ("Landscape 1", "/Assets/Landscapes/Landscape-1.jpg"),
            new ("Landscape 2", "/Assets/Landscapes/Landscape-2.jpg"),
            new ("Landscape 3", "/Assets/Landscapes/Landscape-3.jpg"),
            new ("Landscape 4", "/Assets/Landscapes/Landscape-4.jpg"),
            new ("Landscape 5", "/Assets/Landscapes/Landscape-5.jpg"),
            new ("Landscape 6", "/Assets/Landscapes/Landscape-6.jpg"),
            new ("Landscape 7", "/Assets/Landscapes/Landscape-7.jpg"),
            new ("Landscape 8", "/Assets/Landscapes/Landscape-8.jpg"),
            new ("Landscape 9", "/Assets/Landscapes/Landscape-9.jpg"),
            new ("Landscape 10", "/Assets/Landscapes/Landscape-10.jpg"),
            new ("Landscape 11", "/Assets/Landscapes/Landscape-11.jpg"),
            new ("Landscape 12", "/Assets/Landscapes/Landscape-12.jpg"),
            new ("Landscape 13", "/Assets/Landscapes/Landscape-13.jpg"),
        };
    }

    private void ItemContainerGeneratorOnMaterialized(object? sender, ItemContainerEventArgs e)
    {
        var container = e.Containers[0];

        if (container.ContainerControl is not ListBoxItem lbi)
        {
            return;
        }

        lbi.Loaded += (_, _) =>
        {
            var image = lbi.GetVisualDescendants().OfType<Image>().First();
            var scrollViewer = lbi.GetVisualAncestors().OfType<ScrollViewer>().First();

            if (ElementComposition.GetElementVisual(image) is not { } imageComp) return;
            if (ElementComposition.GetElementVisual(scrollViewer) is not { }) return;
            if (this.GetVisualRoot() is not Visual visualRoot) return;

            var compositionVisualWindow = ElementComposition.GetElementVisual(visualRoot);
            if (compositionVisualWindow is null)
            {
                return;
            }

            var compositor = compositionVisualWindow.Compositor;
            
            var animation = compositor.CreateExpressionAnimation("Window.Size.Y * 0.5 * maskV3");
            animation.SetVector3Parameter("maskV3", Vector3.UnitY);
            animation.SetReferenceParameter("Window", compositionVisualWindow);
            imageComp.StartAnimation("Offset", animation);
        };
    }

    private void Page_Unloaded(object? sender, RoutedEventArgs e)
    {
        // TODO:
    }

    private void Page_Loaded(object? _, RoutedEventArgs __)
    {
        // var compositor = ElementComposition.GetElementVisual(this).Compositor;
        //
        // // Get scrollviewer
        // var myScrollViewer = ThumbnailList.GetVisualDescendants().OfType<ScrollViewer>().First();
        //
        // // Setup the expression
        // var scrollPropSet = _scrollProperties.GetSpecializedReference<ManipulationPropertySetReferenceNode>();
        // var startOffset = ExpressionValues.Constant.CreateConstantScalar("startOffset", 0.0f);
        // var parallaxValue = 0.5f;
        // var parallax = (scrollPropSet.Translation.Y + startOffset);
        // _parallaxExpression = parallax * parallaxValue - parallax;
    }
}
