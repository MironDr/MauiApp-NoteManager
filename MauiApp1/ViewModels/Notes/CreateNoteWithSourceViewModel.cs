using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.ViewModels.Notes;

public sealed class CreateNoteWithSourceViewModel : BaseViewModel
{
    private readonly INoteService _noteService;
    private readonly IModalService _modalService;
    private readonly INoteWithSourceService _noteWithSourceService;
    
    public SpecificNoteSelectorViewModel SourceNoteSelectorViewModel;
    public SpecificNoteSelectorViewModel TextNoteSelectorViewModel;
    
    private SourceNoteModel? _sourceNote;
    public SourceNoteModel? SourceNote
    {
        get => _sourceNote;
        set
        {
            if (_sourceNote != value)
            {
                _sourceNote = value;
                OnPropertyChanged(nameof(SourceNote));
                SourceNoteSelectorViewModel.SelectedNote = _sourceNote;
            }
        }
    }

    private TextNoteModel? _textNote;
    public TextNoteModel? TextNote
    {
        get => _textNote;
        set
        {
            if (_textNote != value)
            {
                _textNote = value;
                OnPropertyChanged(nameof(TextNote));
                TextNoteSelectorViewModel.SelectedNote = _textNote;
            }
        }
    }

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();

    private NoteWithSourceDto Note { get; set; }

    public IAsyncRelayCommand SaveNoteCommand { get; }
    
    public event Action? OnModelCreated;
    

    public CreateNoteWithSourceViewModel(INoteService noteService, IModalService modalService, INoteWithSourceService noteWithSourceService)
    {
        _noteService = noteService;
        _modalService = modalService;
        _noteWithSourceService = noteWithSourceService;


        SaveNoteCommand = new AsyncRelayCommand(SaveNoteAsync);

        _ = Load();
    }


    private async Task Load()
    {
        var newNotes = await _noteService.GetNotes();
        
        SourceNoteSelectorViewModel = new SpecificNoteSelectorViewModel(newNotes.OfType<SourceNoteModel>(), SourceNote)
        {
            SelectedNoteName = "Select a Source Note"
        };
        TextNoteSelectorViewModel = new SpecificNoteSelectorViewModel(newNotes.OfType<TextNoteModel>(), TextNote)
        {
            SelectedNoteName = "Select a Text Note"
        };
       
        ClearViewModel();
    }
    
    private void ClearViewModel()
    {
        Note = new NoteWithSourceDto();
        
        ReloadFields();
    }

  

    private void ReloadFields()
    {
        Fields.Clear();
        Fields.Add(new CustomFieldViewModel("Quote", Note.Quote, s => Note.Quote = s!));
        Fields.Add(new CustomFieldViewModel("Comment", Note.Comment, s => Note.Comment = s));
        
    }

    private async Task SaveNoteAsync()
    {
        Note.SourceNote = SourceNoteSelectorViewModel.SelectedNote as SourceNoteModel;
        Note.Note = TextNoteSelectorViewModel.SelectedNote as TextNoteModel;
        
        
        if (Note.SourceNote == null || Note.Note == null || Note.Quote == null)
            return;
        
        var ns = NoteWithSourceModel.Create(Note);
        
        await _noteService.AddNote(ns.SourceNote!);
        await _noteService.AddNote(ns.Note!);
        await _noteWithSourceService.Add(ns);
        
        OnModelCreated?.Invoke();
        ClearViewModel();
        await _modalService.CloseModalAsync();
    }


   
}