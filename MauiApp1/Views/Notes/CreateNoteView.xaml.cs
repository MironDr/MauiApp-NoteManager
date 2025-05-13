using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Categories;

namespace MauiApp1.Views.Notes;

public sealed partial class CreateNoteView : BaseView
{
    private ManageNoteViewModel? _viewModel;
    public CreateNoteView(ManageNoteViewModel viewModel)
    {
        
        InitializeComponent();
        SetBindingContext(viewModel);
       
    }

    public CreateNoteView()
    {
        InitializeComponent();
    }

    public override void SetBindingContext(BaseViewModel baseViewModel)
    {
        if (baseViewModel is ManageNoteViewModel viewModel)
        {
            _viewModel = viewModel;
            BindingContext = _viewModel;
            CategorySelectorView.SetBindingContext(_viewModel.CategorySelectorViewModel);
        }
    }

    public void GoToEditMode(NoteModel noteModel)
    {
        _viewModel?.GoToEditMode(noteModel);
        SaveButton.Text = "Edit";
    }

    
}