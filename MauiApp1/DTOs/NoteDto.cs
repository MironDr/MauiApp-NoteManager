using MauiApp1.Base;
using MauiApp1.Models;

namespace MauiApp1.DTOs;

public class NoteDto : BaseCommon
{

    public string Title {get; set; } = string.Empty;

    public string? Description { get; set; }
    
    public CategoryModel? Category { get; set; }
    
    public GroupModel? Group { get; set; }

    public bool IsMainInGroup { get; set; }
    
    public ProtectionProfileModel? ProtectionProfile { get; set; }
    public virtual void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        Title = noteModel.Title;
        Description = noteModel.Description;
        Category = noteModel.Category;
        Group = noteModel.Group;
        ProtectionProfile = noteModel.ProtectionProfile;
        IsMainInGroup = noteModel.IsMainInGroup;
    }
}

public class TextNoteDto : NoteDto
{
    public IEnumerable<string?> BlocksTitles { get; set; } = new List<string?>();
    public IEnumerable<string?> BlocksContent { get; set; } = new List<string?>();
    
    public override void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        base.CompleteNoteDtoByNoteModel(noteModel);
        if (noteModel is TextNoteModel textNote)
        {
            BlocksTitles = textNote.GetBlocksTitles();
            BlocksContent = textNote.GetBlocksContents();
        }
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

public class SourceNoteDto : NoteDto
{
    public ReferenceType SourceType { get; set; } 
    public string Source { get; set; } = string.Empty;
    public string? Author { get; set; } 
    public DateTime PublishedDate { get; set; }
    
    public override void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        base.CompleteNoteDtoByNoteModel(noteModel);
        if (noteModel is SourceNoteModel sourceNote)
        {
            SourceType = sourceNote.SourceType;
            Source = sourceNote.Source;
            Author = sourceNote.Author;
            PublishedDate = sourceNote.PublishedDate;
          
        }
    }
}

public class CheckListNoteDto : NoteDto
{
    public IEnumerable<string?> BoxTitles { get; set; } = new List<string>();
    public IEnumerable<bool> BoxStatuses { get; set; } = new List<bool>();
    
    public override void CompleteNoteDtoByNoteModel(NoteModel noteModel)
    {
        base.CompleteNoteDtoByNoteModel(noteModel);
        if (noteModel is CheckListNoteModel checkListNoteModel)
        {
            BoxTitles = checkListNoteModel.GetBoxTitles();
            BoxStatuses = checkListNoteModel.GetBoxStatuses();
        }
    }
}