using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Repositories;

namespace MauiApp1.Services;


public interface INoteService
{
    event EventHandler NotesUpdated;
    Task<List<NoteModel>> GetNotes();
    Task AddNote(NoteModel note);
    NoteModel? GetById(int id);
    Task DeleteNote(NoteModel note);
}
public class NoteService : INoteService
{
    private readonly IDatabaseRepository _repository;
    private readonly ICategoryService _categoryService;
    private readonly INoteWithSourceService _noteWithSourceService;
    private readonly IGroupService _groupService;
    private readonly IProtectionProfileService _protectionProfileService;

    private List<NoteModel> _notes = new();
    private bool _isInitialized = false;

    public event EventHandler NotesUpdated = null!;

    public NoteService(IDatabaseRepository repository, ICategoryService categoryService, IGroupService groupService, IProtectionProfileService protectionProfileService, INoteWithSourceService noteWithSourceService)
    {
        _repository = repository;
        _categoryService = categoryService;
        _groupService = groupService;
        _protectionProfileService = protectionProfileService;
        _noteWithSourceService = noteWithSourceService;
    }

    private async Task InitializeIfNeeded()
    {
        if (_isInitialized) return;

        var loadedNotes = new List<NoteModel>();
       
        loadedNotes.AddRange(await _repository.GetEntitiesAsync<AccountNoteModel>());

        var sourceNotes = await _repository.GetEntitiesAsync<SourceNoteModel>();
        
        var textNotes = await _repository.GetEntitiesAsync<TextNoteModel>();
        foreach (var note in textNotes)
            note.LoadFromJson();
        
        
        var checkListNotes = await _repository.GetEntitiesAsync<CheckListNoteModel>();
        foreach (var note in checkListNotes)
            note.LoadFromJson();
        
        
        var sourceLinks = _noteWithSourceService.GetAll();
       

        foreach (var link in sourceLinks)
        {
           
            var sourceNote = sourceNotes.FirstOrDefault(n => n.Id == link.SourceNoteId);
            var targetNote = textNotes.FirstOrDefault(n => n.Id == link.NoteId);

      
            
            
            if (sourceNote != null && targetNote != null)
            {
                link.SetAssociations(sourceNote, targetNote);
            }
        }
        
        
        loadedNotes.AddRange(checkListNotes);

        loadedNotes.AddRange(textNotes);
        
        loadedNotes.AddRange(sourceNotes);
        
        loadedNotes = loadedNotes
            .OrderBy(n => n.CreatedAt)
            .ToList();
        
        var categories = _categoryService.GetCategories();
        var groups = _groupService.GetGroups();
        var profiles = _protectionProfileService.GetProfiles();

        foreach (var note in loadedNotes)
        {
            if (note.CategoryId is not null)
                note.Category = categories.FirstOrDefault(c => c.Id == note.CategoryId);

            if (note.GroupId is not null)
            {
                note.Group = groups.FirstOrDefault(g => g.Id == note.GroupId);
                note.IsMainInGroup = note.IsMain;
            }

            if (note.ProfileId is not null)
                note.ProtectionProfile = profiles.FirstOrDefault(p => p.Id == note.ProfileId);
        }

      

        _notes = loadedNotes;
        _isInitialized = true;
        Console.WriteLine("Note initialized.");
        NotesUpdated?.Invoke(this, EventArgs.Empty);
    }

    public async Task AddNote(NoteModel note)
    {
        await InitializeIfNeeded();
        
        bool titleExists = _notes.Any(n =>
                n.Title.Equals(note.Title, StringComparison.OrdinalIgnoreCase) &&
                n.Id != note.Id
        );

        if (titleExists)
            throw new InvalidOperationException($"Note with title '{note.Title}' already exists.");

        var existing = _notes.FirstOrDefault(n => n.Id == note.Id);
        if (existing != null)
        {
            var indexOf = _notes.IndexOf(existing);
            _notes[indexOf] = note;
            await _repository.ReplaceEntityAsync(note);
        }
        else
        {
            _notes.Add(note);
            await _repository.SaveNewEntityAsync(note);
        }

    
        Console.WriteLine("Note added");
        NotesUpdated?.Invoke(this, EventArgs.Empty);
    }

    public async Task DeleteNote(NoteModel note)
    {
        await InitializeIfNeeded();

        note.UnlinkAssociations();
        await _repository.DeleteEntityAsync(note);
        _notes.Remove(note);

        Console.WriteLine("Note deleted");
        NotesUpdated?.Invoke(this, EventArgs.Empty);
    }

    public async Task<List<NoteModel>> GetNotes()
    {
        await InitializeIfNeeded();
        return _notes;
    }

    public NoteModel? GetById(int id)
    {
        return _notes.FirstOrDefault(n => n.Id == id);
    }
    
}