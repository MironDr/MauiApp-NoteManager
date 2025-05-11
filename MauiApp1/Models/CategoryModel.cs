using MauiApp1.DTOs;

namespace MauiApp1.Models;

public class CategoryModel : BaseModel
{
       private string _categoryName = null!;

       public string CategoryName
       {
              get => _categoryName;
              set
              {
                     if (string.IsNullOrWhiteSpace(value))
                            throw new ArgumentException("Cannot be empty.", nameof(CategoryName));
                     _categoryName = value;
              }
       }

       private readonly List<NoteModel> _notes = new(); 
    
       public void AddNote(NoteModel note)
       {
              if (!_notes.Contains(note))
              {
                     _notes.Add(note);
              }
        
              if(note.Category != this)
                     note.Category = this;
       }

       public void RemoveNote(NoteModel note)
       {
              if (_notes.Contains(note))
              {
                     _notes.Remove(note);
              }
        
              if (note.Category == this)
              {
                     note.Category = null;
              }
       }

       public List<NoteModel> GetNotes()
       {
              return _notes.ToList();
       }
       
       
       public static CategoryModel CreateCategory(CategoryDto categoryDto)
       {
              return new CategoryModel{Id = 3, CategoryName = categoryDto.CategoryName};
       }

}