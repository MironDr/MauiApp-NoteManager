using MauiApp1.DTOs;
using MauiApp1.Models;


namespace MP01.Models;

public class NoteWithSourceModel
{
    //Asocjacje z atrybutem
    public SourceNoteModel? SourceNote { get; private set; }
    public TextNoteModel? Note { get; private set; }

    public string? Quote { get; private set; }
    public string? Comment { get; private set; }
    public DateTime CreatedAt { get; private init; }

    private NoteWithSourceModel(SourceNoteModel sourceNote, TextNoteModel note)
    {
        SourceNote = sourceNote;
        Note = note;
        CreatedAt = DateTime.Now;
    }

    public static NoteWithSourceModel Create(NoteWithSourceDto dto)
    {
        if(dto.SourceNote == null || dto.Note == null)
            throw new Exception("Source and note must not be null");
        
        var ns = new NoteWithSourceModel(dto.SourceNote, dto.Note)
        {
            Quote = dto.Quote,
            Comment = dto.Comment,
            CreatedAt = DateTime.Now,
        };
        
        
        dto.SourceNote.AddNote(ns);
        dto.Note.AddSourceLink(ns);

        return ns;
    }

    public void Remove()
    {
        if (SourceNote != null && Note != null)
        {
            SourceNote?.RemoveNote(this);
            Note?.RemoveSourceLink(this);
            
            Note = null;
            SourceNote = null;
            
            
        }

        
    }
    
    
    //

   
}