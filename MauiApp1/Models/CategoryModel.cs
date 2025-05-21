using MauiApp1.DTOs;

namespace MauiApp1.Models;

public class CategoryModel : BaseModel
{
       private string _categoryName = null!;
       
       protected CategoryModel() : base()
       {
       }

       public string CategoryName
       {
              get => _categoryName;
              init
              {
                     if (string.IsNullOrWhiteSpace(value))
                            throw new ArgumentException("Cannot be empty.", nameof(CategoryName));
                     _categoryName = value;
              }
       }

       private readonly List<int> _notes = new(); 
    
       public void AddNote(NoteModel note)
       {
              if (!_notes.Contains(note.Id))
              {
                     _notes.Add(note.Id);
              }
        
              if(note.Category?.Id != Id)
                     note.Category = this;
       }

       public void RemoveNote(NoteModel note)
       {
              if (_notes.Contains(note.Id))
              {
                     _notes.Remove(note.Id);
              }
        
              if (note.Category?.Id == Id)
                     note.Category = null;
              
       }

       public List<int> GetNotes()
       {
              return _notes.ToList();
       }
       
       
       public static CategoryModel CreateCategory(CategoryDto categoryDto)
       {
              return new CategoryModel{Id = _idCounter++, CategoryName = categoryDto.CategoryName};
       }

}