using MauiApp1.View;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.Views.Categories;

public sealed partial class CreateCategoryView : BaseView
{
    public CreateCategoryView(CreateCategoryViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
    }

    public CreateCategoryView()
    {
        InitializeComponent();
    }
}