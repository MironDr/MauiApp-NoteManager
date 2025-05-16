using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Categories;

namespace MauiApp1.Views.Notes;

public sealed partial class CreateNoteView : BaseView, IParameterizedView<BaseViewModel>, IViewAddable
{
    private BaseViewModel? _viewModel;

    public CreateNoteView(BaseViewModel viewModel)
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
        _viewModel = baseViewModel;
        BindingContext = _viewModel;

        if (baseViewModel is ICategorySelectable selectable)
        {
            CategorySelectorView.SetBindingContext(selectable.CategorySelectorViewModel);
        }
    }

    public void GoToEditMode(NoteModel noteModel)
    {
        if (_viewModel is IEditableNoteViewModel editable)
        {
            editable.GoToEditMode(noteModel);
            SaveButton.Text = "Edit";
        }
    }


    public void SetData(BaseViewModel data)
    {
        SetBindingContext(data);
    }

    public void AddView(BaseView view)
    {
        MainLayout.Add(view);
    }
}