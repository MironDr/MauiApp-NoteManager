using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels.Categories;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes.Managers;

public sealed class ManageTextNoteViewModel : ManageNoteViewModel<TextNoteDto, TextNoteModel>, ICompositeViewModel
{
    private readonly TextBlocksViewModel _textBlocksViewModel;

    public ManageTextNoteViewModel(INoteService noteService, IModalService modalService,
        ClassifierSelectorViewModel selectorViewModel, TextBlocksViewModel textBlocksViewModel)
        : base(noteService, modalService, selectorViewModel)
    {
        _textBlocksViewModel = textBlocksViewModel;
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();
        
        _textBlocksViewModel.Blocks.Clear();
        
        for (int i = 0; i < Note.BlocksTitles.Count(); i++)
        {
            _textBlocksViewModel.Blocks.Add(new CustomFieldViewModel(
                Note.BlocksTitles.ElementAt(i),
                Note.BlocksContent.ElementAt(i),
                null
            ));
        }
        
    }


    protected override TextNoteModel CreateNoteFromDto()
    {
        CompleteNote();

        return TextNoteModel.CreateNote(Note);
    }

    protected override TextNoteModel EditNoteFromDto()
    {
        CompleteNote();

        return (TextNoteModel)_noteToEdit.EditNote(Note);
    }


    private void CompleteNote()
    {
        Note.BlocksContent = _textBlocksViewModel.Blocks.Select(b => b.Value);
        Note.BlocksTitles = _textBlocksViewModel.Blocks.Select(b => b.Label);
    }

    public IEnumerable<BaseView> GetEmbeddedViews()
    {
        List<BaseView> views = [
            new TextBlocksView(_textBlocksViewModel)
        ];
        
        return views;
        
    }
}