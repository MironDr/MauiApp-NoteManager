﻿using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;


namespace MauiApp1;

public partial class MainPage : BasePage
{
    
    private readonly IServiceProvider _serviceProvider;

    public MainPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        
        _serviceProvider = serviceProvider;


        var horizontalCategoriesView = _serviceProvider.GetRequiredService<HorizontalCategoriesView>();
        
    
        Layout.Add(horizontalCategoriesView);


    }

    
 
    
}