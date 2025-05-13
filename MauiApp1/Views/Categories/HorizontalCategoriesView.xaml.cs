using MauiApp1.View;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.Views.Categories;

public partial class HorizontalCategoriesView : BaseView
{
    public HorizontalCategoriesView(CategoriesViewModel viewModel, CreateCategoryButtonView createCategoryView)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
        MainLayout.Add(createCategoryView);
    }
    
    

}
