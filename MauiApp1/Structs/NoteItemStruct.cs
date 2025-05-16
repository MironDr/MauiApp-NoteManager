using MauiApp1.Interfaces;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Structs;

public struct NoteItemStruct
{
    public NoteItemViewModel NoteItemView { get; set; }
    
    public IEditableNoteViewModel NoteItemEdit { get; set; }
}