using MauiApp1.Base;
using MauiApp1.Models;

namespace MauiApp1.DTOs;

public class NoteDto : BaseCommon
{
    public string Title {get; set; } = string.Empty;

    public string? Description { get; set; }
    
    public int? Category { get; set; }

    public void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        Title = noteModel.Title;
        Description = noteModel.Description;
        Category = noteModel.Category;
    }
}