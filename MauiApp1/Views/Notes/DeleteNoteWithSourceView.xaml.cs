
using MauiApp1.Interfaces;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Notes;

public partial class DeleteNoteWithSourceView : BaseView, IParameterizedView<DeleteNoteWithSourceViewModel>
{
    public DeleteNoteWithSourceView()
    {
        InitializeComponent();
    }

    public void SetData(DeleteNoteWithSourceViewModel data)
    {
       BindingContext = data;
    }
    
}