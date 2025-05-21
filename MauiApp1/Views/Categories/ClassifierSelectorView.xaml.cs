using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Categories;

public partial class ClassifierSelectorView : BaseView
{
    
    
    public ClassifierSelectorView()
    {
        InitializeComponent();
    }
    
    public override void SetBindingContext(BaseViewModel baseViewModel)
    {
        if (baseViewModel is ClassifierSelectorViewModel viewModel)
        {
            BindingContext = viewModel;
            CategoryButtonView.SetBindingContext(viewModel.CreateCategoryButtonViewModel);
            GroupButtonView.SetBindingContext(viewModel.CreateGroupButtonViewModel);
        }
    }


    

    
    
    
}