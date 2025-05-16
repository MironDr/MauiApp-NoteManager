using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.Views.Notes;


namespace MauiApp1.ViewModels.Notes;

public class NotesViewModel : BaseViewModel
{
    private readonly INoteService _noteService;
    
    
    private readonly IPopupService _popupService;
    
    private readonly NoteItemFactoryManager _factoryManager;


    private CategoryModel? _selectedCategory;
    
    private ObservableCollection<NoteModel> _notes = new();
    
    public ICommand NoteSelectedCommand { get; }

    public ObservableCollection<NoteModel> Notes
    {
        get => _notes;
        set
        {
            if (_notes != value)
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
    }
    
    
    public NotesViewModel(INoteService noteService, IPopupService popupService, NoteItemFactoryManager factoryManager) : base()
    {
        _noteService = noteService;
        _popupService = popupService;
        _factoryManager = factoryManager;

        _noteService.NotesUpdated += OnNotesUpdated!;
        
        NoteSelectedCommand = new Command<NoteModel>(OnNoteSelected);
        
        LoadNotes();
    }
    
    private void OnNotesUpdated(object sender, EventArgs e)
    {
        LoadNotes();
    }
    
    private void LoadNotes()
    {
      
        if (_selectedCategory == null)
        {
            Notes = new ObservableCollection<NoteModel>(_noteService.GetNotes());

            return;
        }
        
        Notes = new ObservableCollection<NoteModel>(_noteService.GetNotes().Where(n => n.Category == _selectedCategory.Id));
        
    }
    
    private void OnNoteSelected(NoteModel note)
    {
        NoteItemStruct noteItemStruct = (NoteItemStruct)_factoryManager.Create(note)!;
        _popupService.ShowPopupAsyncWithParameter<NoteItemView, NoteItemStruct>(noteItemStruct);
    }

    public void FilterByCategory(CategoryModel? category)
    {
       
        _selectedCategory = category;
        
        LoadNotes();
            
    }
    
}