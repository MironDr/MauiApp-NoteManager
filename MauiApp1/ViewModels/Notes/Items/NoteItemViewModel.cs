using System.Collections.ObjectModel;
using MauiApp1.Interfaces;
using MauiApp1.Models;

namespace MauiApp1.ViewModels.Notes.Items;

public class NoteItemViewModel : BaseViewModel, IUpdatable
{

    public NoteModel Note {get; private set;}
    private string? _categoryName;
    private string? _groupName;
    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();
    
    
    public NoteItemViewModel(NoteModel data)
    {
        Note = data;
        
    }

    protected virtual void ReloadFields()
    {
        Fields.Clear();
        InitializeNames();
        Fields.Add(new CustomFieldViewModel("Title", Note.Title, null, true));
        Fields.Add(new CustomFieldViewModel("Description", Note.Description, null, true));
        Fields.Add(new CustomFieldViewModel("CreatedAt", Note.CreatedAt.ToShortDateString(), null, true));
        Fields.Add(new CustomFieldViewModel("Category", _categoryName,  null, true));
        Fields.Add(new CustomFieldViewModel("Group", _groupName,  null, true));
    }


    private void InitializeNames()
    {
        _categoryName = Note.Category?.CategoryName;
        _groupName = Note.Group?.GroupName;
        if(Note is { IsMainInGroup: true, Group: not null })
            _groupName = "[MAIN] " + _groupName;
    }
    
    public void Update()
    {
        ReloadFields();
    }
}