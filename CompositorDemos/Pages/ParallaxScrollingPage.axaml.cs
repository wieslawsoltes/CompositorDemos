using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Templates;
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
            new Thumbnail("Land 1", ""),
            new Thumbnail("Land 1", ""),
            new Thumbnail("Land 1", ""),
            new Thumbnail("Land 1", ""),
            new Thumbnail("Land 1", ""),
            new Thumbnail("Land 1", ""),
            new Thumbnail("Land 1", ""),
            new Thumbnail("Land 1", ""),
            new Thumbnail("Land 1", ""),
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
