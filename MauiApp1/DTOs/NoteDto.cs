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
       
    }
}

public class TextNoteDto : NoteDto
{
    public string? TextContent { get; set; }
    
    public override void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        base.CompleteNoteDtoByNoteModel(noteModel);
        var textNote = noteModel as TextNoteModel;
        TextContent = textNote.TextContent;
        
    }
}

public class AccountNoteDto : NoteDto
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public override void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        base.CompleteNoteDtoByNoteModel(noteModel);
        if (noteModel is AccountNoteModel textNote)
        {
            Login = textNote.Login;
            Password = textNote.Password;
        }
    }
}