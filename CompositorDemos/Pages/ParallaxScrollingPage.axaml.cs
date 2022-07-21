using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Interactivity;
using CompositorDemos.Models;

namespace CompositorDemos.Pages;

public partial class ParallaxScrollingPage : UserControl
{
    public ParallaxScrollingPage()
    {       
        InitializeComponent();
        ThumbnailList.ItemContainerGenerator.Recycled += ItemContainerGeneratorOnRecycled;
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

    private void ItemContainerGeneratorOnRecycled(object? sender, ItemContainerEventArgs e)
    {
        
    }

    private void Page_Unloaded(object? sender, RoutedEventArgs e)
    {
        
    }

    private void Page_Loaded(object? sender, RoutedEventArgs e)
    {
        
    }
}
