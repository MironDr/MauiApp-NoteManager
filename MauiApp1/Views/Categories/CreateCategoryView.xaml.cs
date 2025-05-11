using MauiApp1.ViewModels.Categories;

namespace MauiApp1.View.Categories;

public partial class CreateCategoryView : BaseView
{
    public CreateCategoryView(CreateCategoryViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
    }
}