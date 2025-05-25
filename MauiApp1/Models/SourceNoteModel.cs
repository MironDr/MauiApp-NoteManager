using MauiApp1.DTOs;
using MauiApp1.Utilities;
using MP01.Models;

namespace MauiApp1.Models;

public enum ReferenceType
{
    Book,
    Pdf
}

public class SourceNoteModel : NoteModel
{
    public ReferenceType SourceType { get; private set; } 
    public string? Source { get; private set; }
    public string? Author { get; private set; }
    public DateTime PublishedDate { get; private set; }

    public override NoteType Type => NoteType.Source;
    
    
    //Asocjacje z atrybutem
    private List<NoteWithSourceModel> Sources = new();
    
    public void AddNote(NoteWithSourceModel ns)
    {
        if (ns.Note == null)
        {
            Console.WriteLine("Note is null");
            return;
        }
        
        if(Sources.Contains(ns))
            return;
        
        if (ns.SourceNote == this)
        {
            Sources.Add(ns);
        }
    }

    public void RemoveNote(NoteWithSourceModel ns)
    {
        if(!Sources.Contains(ns))   
            return;
        
        Sources.Remove(ns);
        
        if (ns.SourceNote == this)
        {
            ns.Remove();
        }
        
    }
    
    public List<TextNoteModel> GetTextNoteModelsLinks()
    {
        return Sources.Select(t => t.Note).ToList();
    }

    public List<NoteWithSourceModel> GetNotesLinks()
    {
        return Sources.ToList();
    }
    //

    
    public override NoteModel EditNote(NoteDto dto)
    { 
        base.EditNote(dto);
       
     
        var sourceDto = dto as SourceNoteDto;
        
        if (sourceDto == null)
            throw new ArgumentException();

        SourceType = sourceDto.SourceType;
        Author = sourceDto.Author;
        PublishedDate = sourceDto.PublishedDate;
        Source = sourceDto.Source;
     
        return this;
    }

    public static SourceNoteModel CreateNote(NoteDto dto)
    {
        var sourceDto = dto as SourceNoteDto;
        
        if(sourceDto is null)
            throw new ArgumentException("Invalid note type");
        
        var sourceNoteModel = (SourceNoteModel)GetNoteBase(dto, new SourceNoteModel());
        
        sourceNoteModel.Source = sourceDto.Source;
        sourceDto.Author = sourceDto.Author;
        sourceDto.PublishedDate = sourceDto.PublishedDate;
        sourceDto.Source = sourceDto.Source;
        
        
        return sourceNoteModel;
    }
    
   
   
}