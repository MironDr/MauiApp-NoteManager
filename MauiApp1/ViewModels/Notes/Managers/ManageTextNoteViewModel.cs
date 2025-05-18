using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels.Categories;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes.Managers;

public class ManageTextNoteViewModel : ManageNoteViewModel<TextNoteDto, TextNoteModel>, ICompositeViewModel
{
    public TextBlocksViewModel TextBlocksViewModel;

    public ManageTextNoteViewModel(INoteService noteService, IModalService modalService,
        CategorySelectorViewModel selectorViewModel, TextBlocksViewModel textBlocksViewModel)
        : base(noteService, modalService, selectorViewModel)
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

    public IEnumerable<BaseView> GetEmbeddedViews()
    {
        List<BaseView> views = [
            new TextBlocksView(TextBlocksViewModel)
        ];
        
        return views;
        
    }
}