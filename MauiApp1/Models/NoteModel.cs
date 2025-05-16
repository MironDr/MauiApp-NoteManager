using MauiApp1.DTOs;

namespace MauiApp1.Models;
public enum NoteType
{
    Text,
    Account,
  
}
public class NoteModel : BaseModel
{
    public virtual NoteType Type { get; }
    
    private string _title = null!;
    
    protected static int _idCounter = 0;

    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Cannot be empty.", nameof(Title));
            _title = value;
        }
    }
    
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; private init; }


    public int? Category {get; protected set; }


    public void AddCategory(CategoryModel category)
    {
        if(Category != category.Id)
            Category = Id;
        
        if (!category.GetNotes().Contains(Id))
        {
            category.AddNote(this);
        }

        
    }

    public void RemoveCategory(CategoryModel category)
    {
        if (category.Id == Category)
        {
            Category = null;
        }
        
        if (category.GetNotes().Contains(Id))
        {
            category.RemoveNote(this); 
        }
    }
    
    
    public static NoteModel CreateNote(NoteDto noteDto)
    {
        return new NoteModel{Id = _idCounter++, Title = noteDto.Title, CreatedAt = DateTime.Now, Description = noteDto.Description, Category = noteDto.Category};
    }

    public virtual NoteModel EditNote(NoteDto noteDto)
    {
        Title = noteDto.Title;
        Description = noteDto.Description;
        Category = noteDto.Category;
        return this;
    }

}