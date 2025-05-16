using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.ViewModels.Notes.Managers;

public class ManageTextNoteViewModel : ManageNoteViewModel<TextNoteDto, TextNoteModel>
{
    public TextBlocksViewModel TextBlocksViewModel;

    public ManageTextNoteViewModel(INoteService noteService, IPopupService popupService,
        CategorySelectorViewModel selectorViewModel, TextBlocksViewModel textBlocksViewModel)
        : base(noteService, popupService, selectorViewModel)
    {
        TextBlocksViewModel = textBlocksViewModel;
    }

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