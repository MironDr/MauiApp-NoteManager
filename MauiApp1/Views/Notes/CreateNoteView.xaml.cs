using MauiApp1.View;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Categories;

namespace MauiApp1.Views.Notes;

public partial class CreateNoteView : BaseView
{
    public CreateNoteView(CreateNoteViewModel viewModel)
    {
        
        InitializeComponent();
        SetBindingContext(viewModel);
        CategorySelectorView.BindingContext = viewModel.CategorySelectorViewModel;
    }
}