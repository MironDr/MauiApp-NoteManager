using MauiApp1.Base;
using MauiApp1.Models;

namespace MauiApp1.DTOs;

public class NoteDto : BaseCommon
{
    public string Title {get; set; } = string.Empty;

    public string? Description { get; set; }
    
    public int? Category { get; set; }

    public virtual void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        Title = noteModel.Title;
        Description = noteModel.Description;
        Category = noteModel.Category;
        Console.WriteLine("NoteDto.CompleteNoteDtoByNoteModel");
    }
}

public class TextNoteDto : NoteDto
{
    public string? TextContent { get; set; }
    
    public override void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        base.CompleteNoteDtoByNoteModel(noteModel);
        Console.WriteLine("TextNoteDto.CompleteNoteDtoByNoteModel");
        var textNote = noteModel as TextNoteModel;
        TextContent = textNote.TextContent;
        
    }
}