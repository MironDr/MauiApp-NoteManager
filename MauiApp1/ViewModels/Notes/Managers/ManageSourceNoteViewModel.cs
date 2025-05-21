using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels.Categories;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes.Managers;

public sealed class ManageSourceNoteViewModel : ManageNoteViewModel<SourceNoteDto, SourceNoteModel>, ICompositeViewModel
{

    private readonly CustomDatePickerViewModel _datePicker;
    private readonly SourceTypeSelectorViewModel _sourceTypeSelector;
    public ManageSourceNoteViewModel(INoteService noteService, IModalService modalService,
        ClassifierSelectorViewModel selectorViewModel)
        : base(noteService, modalService, selectorViewModel)
    {
        _datePicker = new CustomDatePickerViewModel(d => Note.PublishedDate = d);
        _sourceTypeSelector = new SourceTypeSelectorViewModel(s => Note.SourceType = s);
        ReloadFields();
        
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();
        Fields.Add(new CustomFieldViewModel("Source", Note.Source, s => Note.Source = s!));
        Fields.Add(new CustomFieldViewModel("Author", Note.Author, s => Note.Author = s!));
        _datePicker.SelectedDate = Note.PublishedDate;
        _sourceTypeSelector.SelectedType = Note.SourceType;
    }


    protected override SourceNoteModel CreateNoteFromDto()
    {
        return SourceNoteModel.CreateNote(Note);
    }

    protected override SourceNoteModel EditNoteFromDto()
    {
        return (SourceNoteModel)_noteToEdit.EditNote(Note);
    }

    public IEnumerable<BaseView> GetEmbeddedViews()
    {
        List<BaseView> views = [
            new CustomDatePickerView(_datePicker),
            new CustomTypeSelectorView(_sourceTypeSelector)
        ];
        
        return views;
    }
  

   
}