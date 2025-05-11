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
    
    public string? Description { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    
    private CategoryModel? _category;
    
    public CategoryModel? Category
    {
        get => _category;
        
        set 
        {
            
            if (_category != null && _category.GetNotes().Contains(this))
            {
                _category.RemoveNote(this);
            }
            
            _category = value;
              
            if (_category != null)
            {
                _category.AddNote(this);
            }
        }
    }
    
    
    public static NoteModel CreateNote(NoteDto noteDto)
    {
        return new NoteModel{Id = 3, Title = noteDto.Title, CreatedAt = DateTime.Now, Description = noteDto.Description, Category = noteDto.Category};
    }

}