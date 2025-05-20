using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;


namespace MauiApp1.ViewModels.Notes;

public class NoteItemViewModel : BaseViewModel, IEditableNoteViewModel
{

    public NoteModel Note {get; private set;}
    private string? _categoryName;

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();
    


    
    


    public NoteItemViewModel(NoteModel data, string? categoryName)
    {
        Note = data;
        _categoryName = categoryName;
    }

    protected virtual void ReloadFields()
    {
        Fields.Clear();
        Fields.Add(new CustomFieldViewModel("Title", Note.Title, null, true));
        Fields.Add(new CustomFieldViewModel("Description", Note.Description, null, true));
        Fields.Add(new CustomFieldViewModel("CreatedAt", Note.CreatedAt.ToShortDateString(), null, true));
        Fields.Add(new CustomFieldViewModel("Category", _categoryName,  null, true));
 
    }


    public void GoToEditMode(NoteModel noteModel)
    {
        Note = noteModel;
        ReloadFields();
    }
}