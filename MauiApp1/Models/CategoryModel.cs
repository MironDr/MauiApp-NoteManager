using MauiApp1.DTOs;

namespace MauiApp1.Models;

public class CategoryModel : BaseModel
{
       private string _categoryName = null!;

       public CategoryModel() : base()
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

       private readonly List<NoteModel> _notes = new(); 
    
       public void AddNote(NoteModel note)
       {
              if (!_notes.Contains(note))
              {
                     _notes.Add(note);
              }
        
              if(note.Category?.Id != Id)
                     note.Category = this;
       }

       public void RemoveNote(NoteModel note)
       {
              if (_notes.Contains(note))
              {
                     _notes.Remove(note);
              }
        
              if (note.Category?.Id == Id)
                     note.Category = null;
              
       }

       private void RemoveNotes()
       { 
              foreach (var note in _notes)
                     RemoveNote(note);
       }
       
       public List<NoteModel> GetNotes()
       {
              return _notes.ToList();
       }
       
       
       public static CategoryModel CreateCategory(CategoryDto categoryDto)
       {
              return new CategoryModel{CategoryName = categoryDto.CategoryName};
       }

       public void UnlinkAssociations()
       {
              RemoveNotes();
       }

}