using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;


namespace MauiApp1.ViewModels.Notes;

public class NoteItemViewModel : BaseViewModel
{

    public NoteModel Note {get;}
    private string? _categoryName;

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();
    


    private bool _isReadOnly = true;
    public bool IsEdit => !_isReadOnly;
    
    public bool IsReadOnly
    {
        get => _isReadOnly;
        set
        {
            if (_isReadOnly != value)
            {
                _isReadOnly = value;
                foreach (var field in Fields)
                {
                    field.IsReadOnly = _isReadOnly;
                }
                ReloadFields();
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }
    }
    


    public NoteItemViewModel(NoteModel data, string? categoryName)
    {
        Note = data;
        _categoryName = categoryName;
        ReloadFields();
        
    }

    private void ReloadFields()
    {
        Fields.Clear();
        Fields.Add(new CustomFieldViewModel("Title", Note.Title, s => Note.Title = s!, _isReadOnly));
        Fields.Add(new CustomFieldViewModel("Description", Note.Description, s => Note.Description = s, _isReadOnly));
        Fields.Add(new CustomFieldViewModel("CreatedAt", Note.CreatedAt.ToShortDateString(), null, _isReadOnly));
        Fields.Add(new CustomFieldViewModel("Category", _categoryName,  null, _isReadOnly));
 
    }
    
   
        
 
}