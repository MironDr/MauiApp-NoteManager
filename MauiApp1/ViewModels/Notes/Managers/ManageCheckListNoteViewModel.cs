using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels.Categories;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes.Managers;

public sealed class ManageCheckListNoteViewModel : ManageNoteViewModel<CheckListNoteDto, CheckListNoteModel>, ICompositeViewModel
{
    private readonly CheckListViewModel _checkListViewModel;
    
    
    public ManageCheckListNoteViewModel(INoteService noteService, IModalService modalService, ClassifierSelectorViewModel selectorViewModel, CheckListViewModel checkListViewModel)
        : base(noteService, modalService, selectorViewModel)
    {
        _checkListViewModel = checkListViewModel;
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();
        
        _checkListViewModel.Boxes.Clear();
        
        for (int i = 0; i < Note.BoxTitles.Count(); i++)
        {
            _checkListViewModel.Boxes.Add(new CustomCheckBoxViewModel(
                Note.BoxStatuses.ElementAt(i),
                Note.BoxTitles.ElementAt(i),
                null 
            ));
        }
        
    }


    protected override CheckListNoteModel CreateNoteFromDto()
    {
        CompleteNote();

        return CheckListNoteModel.CreateNote(Note);
    }

    protected override CheckListNoteModel EditNoteFromDto()
    {
        CompleteNote();

        return (CheckListNoteModel)_noteToEdit.EditNote(Note);
    }


    private void CompleteNote()
    {
        Note.BoxTitles = _checkListViewModel.Boxes.Select(b => b.Title);
        Note.BoxStatuses = _checkListViewModel.Boxes.Select(b => b.Status);
    }

    public IEnumerable<BaseView> GetEmbeddedViews()
    {
    
        List<BaseView> views = [
            new CheckListView(_checkListViewModel)
        ];
        
        return views;
        
    }
}