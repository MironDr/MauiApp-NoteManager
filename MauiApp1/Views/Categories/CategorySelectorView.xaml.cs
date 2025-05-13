using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Categories;

public partial class CategorySelectorView : BaseView
{
    
    private CategorySelectorViewModel _viewModel;
    public CategorySelectorView()
    {
        InitializeComponent();
    }
    
    public override void SetBindingContext(BaseViewModel baseViewModel)
    {
        if (baseViewModel is CategorySelectorViewModel viewModel)
        {
            _viewModel = viewModel;
            BindingContext = _viewModel;
            CreateCategory.BindingContext = _viewModel.CreateCategoryViewModel;
            _viewModel.CreateCategoryViewModel.CategorySaved += CategoryCreated;
        }
    }


    private void ToggleButton_Clicked(object? sender, EventArgs e)
    {
      ToggleMode();
    }

    private void ToggleMode()
    {
        LayoutList.IsVisible = !LayoutList.IsVisible;
        LayoutCreate.IsVisible = !LayoutList.IsVisible;
        
        ToggleModeButton.Text = LayoutList.IsVisible ? "Create new" : "Back";
    }
    
    private void CategoryCreated(CategoryModel category)
    {
        ToggleMode();
        _viewModel.OnCategorySelected(category);
    }
}