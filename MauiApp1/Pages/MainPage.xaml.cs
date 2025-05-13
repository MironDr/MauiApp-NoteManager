using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.Views.Categories;
using MauiApp1.Views.Notes;


namespace MauiApp1;

public partial class MainPage : BasePage
{
    
    private readonly IServiceProvider _serviceProvider;

    public MainPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;

        var mainVm = _serviceProvider.GetRequiredService<MainPageViewModel>();

        var horizontalCategoriesView = new HorizontalCategoriesView(
            mainVm.CategoriesViewModel,
            _serviceProvider.GetRequiredService<CreateCategoryButtonView>());

        var verticalNotesView = new VerticalNotesView(
            mainVm.NotesViewModel,
            _serviceProvider.GetRequiredService<CreateNoteButtonView>());

        Layout.Add(horizontalCategoriesView);
        Layout.Add(verticalNotesView);
    }

    
 
    
}