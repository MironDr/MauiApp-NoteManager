using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.ViewModels;

namespace MauiApp1.Interfaces;

public interface INoteItemFactory
{
    NoteType NoteType { get; }

    NoteItemStruct Create(NoteModel note);
    BaseViewModel GetEditorViewModel();
}