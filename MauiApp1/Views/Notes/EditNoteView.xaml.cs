using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Notes;


namespace MauiApp1.Views.Notes;

public partial class EditNoteView : BaseView, IViewAddable
{
   
    
    public EditNoteView()
    {
        InitializeComponent();
    }
    
    public override void SetBindingContext(BaseViewModel baseViewModel)
    {
        EditView.SetBindingContext(baseViewModel);
        
        
    }

    public void SetDataToEdit(NoteModel noteModel)
    {
         EditView.GoToEditMode(noteModel);
    }

    public void AddView(BaseView view)
    {
       EditView.AddView(view);
    }
}