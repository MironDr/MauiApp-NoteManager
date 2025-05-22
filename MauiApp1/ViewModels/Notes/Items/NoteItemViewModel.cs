using System.Collections.ObjectModel;
using MauiApp1.Interfaces;
using MauiApp1.Models;

namespace MauiApp1.ViewModels.Notes.Items;

public class NoteItemViewModel : BaseViewModel, IUpdatable
{

    public NoteModel Note {get; private set;}
    private string? _categoryName;

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();
    
    
    public NoteItemViewModel(NoteModel data)
    {
        Note = data;
        _categoryName = Note.Category?.CategoryName;
    }

    protected virtual void ReloadFields()
    {
        Fields.Clear();
        Fields.Add(new CustomFieldViewModel("Title", Note.Title, null, true));
        Fields.Add(new CustomFieldViewModel("Description", Note.Description, null, true));
        Fields.Add(new CustomFieldViewModel("CreatedAt", Note.CreatedAt.ToShortDateString(), null, true));
        Fields.Add(new CustomFieldViewModel("Category", _categoryName,  null, true));
 
    }


    
    public void Update()
    {
        _categoryName = Note.Category?.CategoryName;
        ReloadFields();
    }
}