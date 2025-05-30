using MauiApp1.View;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.ElementsViews;

public partial class TextBlocksView : BaseView
{
    
    public TextBlocksView()
    {
        InitializeComponent();
    }

    public TextBlocksView(TextBlocksViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

   

   
}