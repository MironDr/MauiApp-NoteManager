using MauiApp1.DTOs;
using MauiApp1.Models;

namespace MauiApp1.Services;


public interface INoteService
{
    event EventHandler NotesUpdated;
    List<NoteModel> GetNotes();
    void AddNote(NoteModel note);
    NoteModel? GetById(int id);
}
public class NoteService : INoteService
{
    private readonly List<NoteModel> _notes = new();
    
    public event EventHandler NotesUpdated = null!;
    
    public NoteService()
    {
        LoadNotes();
    }

    private void LoadNotes()
    {
 
    }

    public void AddNote(NoteModel note)
    {
        if(_notes.FindIndex(n => n.Id == note.Id) is var index)
            if(index != -1)
                _notes[index] = note;
            else
                _notes.Add(note);
        
        
        NotesUpdated?.Invoke(this, EventArgs.Empty);
    }
    
    public List<NoteModel> GetNotes()
    {
        return _notes;
    }

    public NoteModel? GetById(int id)
    {
        return _notes.FirstOrDefault(n => n.Id == id);
    }
    
}