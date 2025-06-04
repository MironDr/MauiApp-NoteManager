using MauiApp1.Models;
using MauiApp1.Repositories;

namespace MauiApp1.Services;

public interface INoteWithSourceService
{
    event EventHandler NotesWithSourceUpdated;
    
    List<NoteWithSourceModel> GetAll();
    NoteWithSourceModel? GetById(int id);
    Task Add(NoteWithSourceModel noteWithSource);
    Task Delete(NoteWithSourceModel noteWithSource);
}

public class NoteWithSourceService : INoteWithSourceService
{
    private readonly IDatabaseRepository _repository;
    private List<NoteWithSourceModel> _notesWithSource = new();

    public event EventHandler NotesWithSourceUpdated = null!;

    public NoteWithSourceService(IDatabaseRepository repository)
    {
        _repository = repository;
        _ = Load();
    }

    private async Task Load()
    {
        try
        {
            _notesWithSource = await _repository.GetEntitiesAsync<NoteWithSourceModel>();
            NotesWithSourceUpdated?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load NoteWithSource: {ex.Message}");
        }
    }

    public List<NoteWithSourceModel> GetAll()
    {
        return _notesWithSource;
    }

    public NoteWithSourceModel? GetById(int id)
    {
        return _notesWithSource.FirstOrDefault(n => n.Id == id);
    }

    public async Task Add(NoteWithSourceModel noteWithSource)
    {
        await _repository.SaveNewEntityAsync(noteWithSource);
        await Load();
    }

    public async Task Delete(NoteWithSourceModel noteWithSource)
    {
        noteWithSource.Remove(); 
        await _repository.DeleteEntityAsync(noteWithSource);
        await Load();
    }
}