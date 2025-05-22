using MauiApp1.DTOs;
using MauiApp1.Utilities;

namespace MauiApp1.Models;
public enum NoteType
{
    Text,
    Account,
    Source,
    CheckList,
  
}
public class NoteModel : BaseModel
{
    public virtual NoteType Type { get; }

    private string _title = null!;
    protected NoteModel() : base() { }

    public string Title
    {
        get => _title;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Cannot be empty.", nameof(Title));
            _title = value;
        }
    }

    public string? Description { get; protected set; }
    public DateTime CreatedAt { get; protected init; }

    // ---- Group ----

    private GroupModel? _group;
    public GroupModel? Group
    {
        get => _group;
        set
        {
            if (Category != null)
                return;

            if (value != null && value.GetNotes().Count > 3)
                return;

            if (_group != null && _group.GetNotes().Contains(this))
            {
                _group.RemoveNote(this);
            }

            _group = value;

            if (_group != null)
            {
                _group.AddNoteToGroup(this);
            }
        }
    }

    // ---- Category ----

    private CategoryModel? _category;
    public CategoryModel? Category
    {
        get => _category;
        set
        {
            
            if (Group != null)
                return;

            if (_category != null && _category.GetNotes().Contains(Id))
            {
                _category.RemoveNote(this);
            }

            _category = value;

            if (_category != null && !_category.GetNotes().Contains(Id))
            {
                _category.AddNote(this);
            }
        }
    }

    // ---- Main note flag ----

    private bool _isMainInGroup;
    public bool IsMainInGroup
    {
        get => _isMainInGroup;
        set
        {
            if (_group == null || value == _isMainInGroup)
                return;

            if (value)
            {
                if (_group.GetMainNote() == null)
                    _group.AddMainNote(this);
            }
            else
            {
                if (_group.GetMainNote() != null)
                    _group.RemoveMainNote();
            }

            _isMainInGroup = value;
        }
    }

    // ---- Helpers ----

    public static NoteModel CreateNote(NoteDto noteDto)
    {
        var note = new NoteModel
        {
            Id = _idCounter++,
            Title = noteDto.Title,
            CreatedAt = DateTime.Now,
            Description = noteDto.Description
        };

        if (noteDto.Category != null)
        {
            note.Group = null;
            note.Category = noteDto.Category;
        }
        else if (noteDto.Group != null)
        {
            note.Category = null;
            note.Group = noteDto.Group;
        }

        

        return note;
    }

    public virtual NoteModel EditNote(NoteDto noteDto)
    {
        Title = noteDto.Title;
        Description = noteDto.Description;
        
        if (noteDto.Category != null)
        {
            Group = null;
            Category = noteDto.Category;
        }
        else if (noteDto.Group != null)
        {
            Category = null;
            Group = noteDto.Group;
        }else
        {
            Category = null;
            Group = null;
        }
        
        return this;
    }
    
   
}