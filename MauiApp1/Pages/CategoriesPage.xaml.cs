using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.Pages;

public partial class CategoriesPage : BasePage
{
    
    private readonly IServiceProvider _serviceProvider;

    public CategoriesPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;

        var mainVm = _serviceProvider.GetRequiredService<CategoriesPageViewModel>();

        VerticalNotesView.SetBindingContext(mainVm.NotesViewModel);
        HorizontalCategoriesView.SetBindingContext(mainVm.CategoriesViewModel);

        CreateCategoryButtonView.SetBindingContext(_serviceProvider.GetRequiredService<CreateCategoryButtonViewModel>());
        
       
    }

    
 
    
}