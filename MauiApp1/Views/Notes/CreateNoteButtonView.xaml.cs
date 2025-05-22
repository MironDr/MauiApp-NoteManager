using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Notes;

public sealed partial class CreateNoteButtonView : BaseView
{
    public CreateNoteButtonView(CreateNoteButtonViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
       
    }
    
    public CreateNoteButtonView()
    {
        InitializeComponent();
    }
    
   
}

