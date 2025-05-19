using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.ViewModels.Notes.Managers;

public sealed class ManageAccountNoteViewModel : ManageNoteViewModel<AccountNoteDto, AccountNoteModel>
{
    public ManageAccountNoteViewModel(INoteService noteService, IModalService modalService,
        CategorySelectorViewModel selectorViewModel)
        : base(noteService, modalService, selectorViewModel)
    {
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();
        Fields.Add(new CustomFieldViewModel("Login", Note.Login, s => Note.Login = s!));
        Fields.Add(new CustomFieldViewModel("Password", Note.Password, s => Note.Password = s!));
    }

    protected override AccountNoteModel CreateNoteFromDto()
    {
        return AccountNoteModel.CreateNote(Note);
    }

    protected override AccountNoteModel EditNoteFromDto()
    {
        return (AccountNoteModel)_noteToEdit.EditNote(Note);
    }
}