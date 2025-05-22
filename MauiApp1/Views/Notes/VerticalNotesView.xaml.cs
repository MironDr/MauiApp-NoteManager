using MauiApp1.Interfaces;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Notes;

public sealed partial class VerticalNotesView : BaseView, IParameterizedView<NotesViewModel>
{
    public VerticalNotesView(NotesViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
    }

    public void SetData(NotesViewModel data)
    {
        SetBindingContext(data);
    }
}