using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.ViewModels.Notes;

public class ManageTextNoteViewModel : ManageNoteViewModel<TextNoteDto, TextNoteModel>
{
    public ManageTextNoteViewModel(INoteService noteService, IPopupService popupService, CategorySelectorViewModel selectorViewModel)
        : base(noteService, popupService, selectorViewModel) { }

    protected override void ReloadFields()
    {
        base.ReloadFields();
        Fields.Add(new CustomFieldViewModel("Text Content", Note.TextContent, s => Note.TextContent = s!));
    }

    protected override TextNoteModel CreateNoteFromDto()
    {
        return TextNoteModel.CreateNote(Note);
    }

    protected override TextNoteModel EditNoteFromDto()
    {
        return (TextNoteModel)_noteToEdit.EditNote(Note);
    }
}