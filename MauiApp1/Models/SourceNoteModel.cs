using MauiApp1.DTOs;
using MauiApp1.Utilities;
using SQLite;

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

    [Ignore]
    public override NoteType Type => NoteType.Source;
    
    
    //Asocjacje z atrybutem
    private readonly List<NoteWithSourceModel> _sources = new();
    
    public void AddNote(NoteWithSourceModel ns)
    {
        if (ns.Note == null)
        {
            Console.WriteLine("Note is null");
            return;
        }
        
        if(_sources.Contains(ns))
            return;
        
        if (ns.SourceNote == this)
        {
            _sources.Add(ns);
        }
    }

    public void RemoveNote(NoteWithSourceModel ns)
    {
        if(!_sources.Contains(ns))   
            return;
        
        _sources.Remove(ns);
        
        if (ns.SourceNote == this)
        {
            ns.Remove();
        }
        
    }
    
    public List<TextNoteModel> GetTextNoteModelsLinks()
    {
        return _sources.Select(t => t.Note).ToList();
    }

    public List<NoteWithSourceModel> GetNotesLinks()
    {
        return _sources.ToList();
    }
    
    private void RemoveAllSources()
    {
        foreach (var ns in _sources.ToList())
        {
            ns.Remove();
        }
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
        
        var sourceNoteModel = GetNoteBase<SourceNoteModel>(dto);
        
        sourceNoteModel.Source = sourceDto.Source;
        sourceDto.Author = sourceDto.Author;
        sourceDto.PublishedDate = sourceDto.PublishedDate;
        sourceDto.Source = sourceDto.Source;
        
        
        return sourceNoteModel;
    }
    
    public override void UnlinkAssociations()
    {
        base.UnlinkAssociations();
        RemoveAllSources();
    }

    
}