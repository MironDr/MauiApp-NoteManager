using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Categories;
using MauiApp1.Views.Notes;


namespace MauiApp1;

public partial class CategoriesPage : BasePage
{
    
    private readonly IServiceProvider _serviceProvider;

    public CategoriesPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;

        var mainVm = _serviceProvider.GetRequiredService<CategoriesPageViewModel>();

        var horizontalCategoriesView = new HorizontalCategoriesView(
            mainVm.CategoriesViewModel);

        var verticalNotesView = new VerticalNotesView(
            mainVm.NotesViewModel);

        CreateCategoryButtonView.SetBindingContext(_serviceProvider.GetRequiredService<CreateCategoryButtonViewModel>());
        
        Layout.Add(horizontalCategoriesView);
        Layout.Add(verticalNotesView);
    }

    
 
    
}