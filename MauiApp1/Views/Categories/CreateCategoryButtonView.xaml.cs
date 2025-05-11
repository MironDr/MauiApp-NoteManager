using MauiApp1.View;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.Views.Categories;

public partial class CreateCategoryButtonView : BaseView
{
    public CreateCategoryButtonView(CreateCategoryButtonViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
    }
}

