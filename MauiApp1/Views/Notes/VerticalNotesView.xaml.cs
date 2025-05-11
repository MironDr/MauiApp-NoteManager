using MauiApp1.View;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Notes;

public partial class VerticalNotesView : BaseView
{
    public VerticalNotesView(NotesViewModel viewModel, CreateNoteButtonView createNoteView)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
        MainLayout.Add(createNoteView);
    }
}