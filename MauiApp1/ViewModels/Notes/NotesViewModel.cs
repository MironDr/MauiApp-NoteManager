using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.Views;
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
    private readonly IModalService _modalService;
    protected readonly IPopupService _popupService;
    private readonly NoteItemFactoryManager _factoryManager;

    private CategoryModel? _selectedCategory;
    private ListType _listType = ListType.Note;

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy != value)
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
    }
    
    public ListType ListType
    {
        get => _listType;
        set
        {
            if (_listType != value)
            {
                _listType = value;
                _ = LoadNotes(); 
            }
        }
    }

    private ObservableCollection<NoteModel> _notes = new();
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

    public IAsyncRelayCommand<NoteModel> NoteSelectedCommand { get; }
    public IAsyncRelayCommand<NoteModel> DeleteNoteCommand { get; }
    public IAsyncRelayCommand ReloadNotesCommand { get; }
    

    public NotesViewModel(
        INoteService noteService,
        IModalService modalService,
        NoteItemFactoryManager factoryManager,
        IPopupService popupService) : base()
    {
        _noteService = noteService;
        _modalService = modalService;
        _factoryManager = factoryManager;
        _popupService = popupService;

        NoteSelectedCommand = new AsyncRelayCommand<NoteModel>(OnNoteSelected!);
        DeleteNoteCommand = new AsyncRelayCommand<NoteModel>(OnNoteDeleted!);
        ReloadNotesCommand = new AsyncRelayCommand(LoadNotes);

        _noteService.NotesUpdated += OnNotesUpdated;
      
        _ = LoadNotes();
    }

    private async void OnNotesUpdated(object? sender, EventArgs e)
    {
        try
        {
            await LoadNotes();
        }
        catch (Exception e1)
        {
            Console.WriteLine(e1);
        }
    }

    public async void FilterByCategory(CategoryModel? category)
    {
        try
        {
            _selectedCategory = category;
            await LoadNotes();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    protected async Task LoadNotes()
    {
        
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Console.WriteLine("Loading notes...");

            var allNotes = await _noteService.GetNotes();

            var filtered = FilterNotes(allNotes);
            Notes = new ObservableCollection<NoteModel>(filtered);

            Console.WriteLine($"Loaded notes: {Notes.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading notes: {ex.Message}");
            await _popupService.AlertAsync("Error", "Failed to load notes", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    protected virtual IEnumerable<NoteModel> FilterNotes(IEnumerable<NoteModel> notes)
    {
        return ListType switch
        {
            ListType.Category => notes.Where(n => _selectedCategory == null || n.Category?.Id == _selectedCategory.Id),
            _ => notes
        };
    }
    
    protected virtual async Task OnNoteSelected(NoteModel note)
    {
        if (note.ProtectionProfile is { IsUnlocked: false })
        {
            if (!await PasswordPopup(note.ProtectionProfile))
                return;
        }

        var noteItemStruct = (NoteItemStruct)_factoryManager.Create(note)!;
        await _modalService.ShowModalAsyncWithParameter<NoteItemView, NoteItemStruct>(noteItemStruct);
    }

    private async Task OnNoteDeleted(NoteModel note)
    {
        bool confirm = await _popupService.AlertConfirmAsync("Warning", "Are you sure you want to delete the note?");
        if (!confirm)
            return;

        if (note.ProtectionProfile is { IsUnlocked: false })
        {
            if (!await PasswordPopup(note.ProtectionProfile))
                return;
        }

        await _noteService.DeleteNote(note);
    }

    protected async Task<bool> PasswordPopup(ProtectionProfileModel profile)
    {
        var password = await _popupService.ShowResultPopupAsyncWithParameter<PasswordPopupView, string, string?>(profile.ProfileName);

        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (!profile.TryUnlock(password))
        {
            await _popupService.AlertAsync("Error", "Incorrect password", "Ok");
            return false;
        }

        return true;
    }
    
    
}