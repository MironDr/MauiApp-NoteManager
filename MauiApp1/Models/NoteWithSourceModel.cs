using MauiApp1.DTOs;
using SQLite;

namespace MauiApp1.Models;

public class NoteWithSourceModel : BaseModel
{

   
    public int NoteId { get; set; }
    public int SourceNoteId { get; set; }

  
    [Ignore]
    public TextNoteModel? Note { get; private set; }

    [Ignore]
    public SourceNoteModel? SourceNote { get; private set; }

    public string? Quote { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public static NoteWithSourceModel Create(NoteWithSourceDto dto)
    {
        if (dto.SourceNote == null || dto.Note == null || dto.Quote == null)
            throw new Exception("Source and note and quote must not be null");

        var model = new NoteWithSourceModel
        {
            SourceNote = dto.SourceNote,
            Note = dto.Note,
            SourceNoteId = dto.SourceNote.Id,
            NoteId = dto.Note.Id,
            Quote = dto.Quote,
            Comment = dto.Comment,
            CreatedAt = DateTime.Now
        };

        dto.SourceNote.AddNote(model);
        dto.Note.AddSourceLink(model);

        return model;
    }

    public void Remove()
    {
        SourceNote?.RemoveNote(this);
        Note?.RemoveSourceLink(this);
        Note = null;
        SourceNote = null;
    }
    
    public void SetAssociations(SourceNoteModel source, TextNoteModel note)
    {
        SourceNote = source;
        Note = note;
        
        source.AddNote(this);
        note.AddSourceLink(this);
    }

  
   
}