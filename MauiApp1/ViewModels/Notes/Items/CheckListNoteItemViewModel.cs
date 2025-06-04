using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes.Items;

public sealed class CheckListNoteItemViewModel : NoteItemViewModel, ICompositeViewModel
{
    private readonly CheckListNoteModel _model;
    
    private readonly INoteService _noteService;
    
    private readonly CheckListViewModel _checkListViewModel;
    
    public CheckListNoteItemViewModel(NoteModel data, CheckListViewModel checkListViewModel, INoteService noteService) 
        : base(data)
    {
        if (data is not CheckListNoteModel checkListNoteModel)
            throw new ArgumentException("Type is not CheckListNoteModel.", nameof(data));
        _checkListViewModel = checkListViewModel;
        _noteService = noteService;
        _model = checkListNoteModel;
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();

        _checkListViewModel.Boxes.Clear();

        foreach (var checkBox in _model.GetCheckBoxes())
        {
            _checkListViewModel.Boxes.Add(new CustomCheckBoxViewModel(
                checkBox.Status,
                checkBox.Title,
                newStatus =>
                {
                    checkBox.Status = newStatus;
                    _model.SyncToJson();
                    _noteService.AddNote(_model);
                },
                true
            ));
        }
        
        
    }

    public IEnumerable<BaseView> GetEmbeddedViews()
    {
        List<BaseView> views = [
            new CheckListView(_checkListViewModel)
        ];
        
        return views;
        
    }
}