using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;


namespace MauiApp1.ViewModels.Notes;

public class NotesViewModel : BaseViewModel
{
    private readonly INoteService _noteService;
    
    private readonly IPopupService _popupService;
    
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
    
    
    public NotesViewModel(INoteService noteService, IPopupService popupService) : base()
    {
        _noteService = noteService;
        _popupService = popupService;
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
      
        Notes = new ObservableCollection<NoteModel>(_noteService.GetNotes());
    
    }
    
    private void OnNoteSelected(NoteModel note)
    {
        _popupService.ShowPopupAsyncWithParameter<NoteView, NoteModel>(note);
    }
    
}