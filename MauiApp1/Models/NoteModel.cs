using MauiApp1.DTOs;

namespace MauiApp1.Models;

public class NoteModel : BaseModel
{
    private string _title = null!;

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
    
    public DateTime CreatedAt { get; init; }


    public int? Category {get; private set; }


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
        return new NoteModel{Id = 3, Title = noteDto.Title, CreatedAt = DateTime.Now, Description = noteDto.Description, Category = noteDto.Category};
    }

}