using MauiApp1.Interfaces;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Notes;

public partial class CreateNoteWithSourceView : BaseView, IParameterizedView<CreateNoteWithSourceViewModel>
{
    public CreateNoteWithSourceView()
    {
        InitializeComponent();
    }
    

    public void SetData(CreateNoteWithSourceViewModel data)
    {
        SetBindingContext(data);
        SourceNotesView.SetBindingContext(data.SourceNoteSelectorViewModel);
        TextNotesView.SetBindingContext(data.TextNoteSelectorViewModel);
    }
}