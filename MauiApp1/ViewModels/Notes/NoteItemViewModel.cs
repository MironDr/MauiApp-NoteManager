using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;


namespace MauiApp1.ViewModels.Notes;

public class NoteItemViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    private NoteModel _note;
    private string? _categoryName;

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();
    


    private bool _isReadOnly = true;

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
    


    public NoteItemViewModel(NoteModel data, string? categoryName, IPopupService popupService)
    {
        _note = data;
        _categoryName = categoryName;
        _popupService = popupService;
        ReloadFields();

     
    }

    private void ReloadFields()
    {
        Fields.Clear();
        Fields.Add(new CustomFieldViewModel("Title", _note.Title, s => _note.Title = s!, _isReadOnly));
        Fields.Add(new CustomFieldViewModel("Description", _note.Description, s => _note.Description = s, _isReadOnly));
        Fields.Add(new CustomFieldViewModel("CreatedAt", _note.CreatedAt.ToShortDateString(), null, _isReadOnly));
        Fields.Add(new CustomFieldViewModel("Category", _categoryName,  null, _isReadOnly));
 
    }
    
   
        
 
}