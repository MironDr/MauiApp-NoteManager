using MauiApp1.View;
using MauiApp1.View.Categories;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.Views.Categories;

public partial class HorizontalCategoriesView : BaseView
{
    public HorizontalCategoriesView(MultiCategoriesViewModel viewModel, CreateCategoryButtonView createCategoryView)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
        MainLayout.Add(createCategoryView);
    }
    
    

}
