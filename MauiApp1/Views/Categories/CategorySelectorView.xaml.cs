using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Categories;

public partial class CategorySelectorView : BaseView
{
    
    
    public CategorySelectorView()
    {
        InitializeComponent();
    }
    
    public override void SetBindingContext(BaseViewModel baseViewModel)
    {
        if (baseViewModel is CategorySelectorViewModel viewModel)
        {
            BindingContext = viewModel;
            CategoryButtonView.SetBindingContext(viewModel.CreateCategoryButtonViewModel);
        }
    }


    

    
    
    
}