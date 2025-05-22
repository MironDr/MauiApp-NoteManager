using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.Views.Notes;


namespace MauiApp1.ViewModels.Notes;

public enum ListType
{
    Note,
    Category
}

public class NotesViewModel : BaseViewModel
{
    protected readonly INoteService _noteService;
    
    
    protected readonly IModalService _modalService;

    private ListType _listType = ListType.Note;
    public ListType ListType
    {
        get => _listType;
        set
        {
            if (value != _listType)
            {
                _listType = value;
                LoadNotes();
            }
        }
    }

    private readonly NoteItemFactoryManager _factoryManager;


    private CategoryModel? _selectedCategory;
    
    private ObservableCollection<NoteModel> _notes = new();
    
    public AsyncRelayCommand<NoteModel> NoteSelectedCommand { get; }

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
    
    
    public NotesViewModel(INoteService noteService, IModalService modalService, NoteItemFactoryManager factoryManager) : base()
    {
        _noteService = noteService;
        _modalService = modalService;
        _factoryManager = factoryManager;

        
        _noteService.NotesUpdated += OnNotesUpdated!;
        
        NoteSelectedCommand = new AsyncRelayCommand<NoteModel>(OnNoteSelected!);
        
        LoadNotes();
    }
    
    private void OnNotesUpdated(object sender, EventArgs e)
    {
        LoadNotes();
    }
    
    protected virtual void LoadNotes()
    {
        switch (ListType)
        {
            case ListType.Category:
                Notes = new ObservableCollection<NoteModel>(
                    _noteService.GetNotes()
                        .Where(n => n.Group == null)
                        .Where(n => _selectedCategory == null
                            ? n.Category != null
                            : n.Category?.Id == _selectedCategory.Id)
                );
                break;
            default:
                Notes = new ObservableCollection<NoteModel>(_noteService.GetNotes());
                break;

        }
        
    }
    
    protected virtual async Task OnNoteSelected(NoteModel note)
    {
        var noteItemStruct = (NoteItemStruct)_factoryManager.Create(note)!;
        
        await _modalService.ShowModalAsyncWithParameter<NoteItemView, NoteItemStruct>(noteItemStruct);
        
        
    }

    public void FilterByCategory(CategoryModel? category)
    {
       
        _selectedCategory = category;
        
        LoadNotes();
            
    }
    
}