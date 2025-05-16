using MauiApp1.Models;

namespace MauiApp1.Interfaces;

public interface IEditableNoteViewModel
{
    void GoToEditMode(NoteModel noteModel);
}