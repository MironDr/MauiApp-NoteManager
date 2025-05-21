using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.ViewModels.Notes.Managers;

public abstract class ManageNoteViewModel<TDto, TModel> : BaseViewModel, IEditableNoteViewModel, ICategorySelectable, IEventHandler
    where TDto : NoteDto, new()
    where TModel : NoteModel
{
    protected readonly INoteService _noteService;
    protected readonly IModalService _modalService;

    public ClassifierSelectorViewModel ClassifierSelectorViewModel { get; }

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();

    public TDto Note { get; } = new();

    public IAsyncRelayCommand SaveNoteCommand { get; }

    protected bool _editMode = false;
    protected TModel _noteToEdit;

    
    
    protected ManageNoteViewModel(INoteService noteService, IModalService modalService, ClassifierSelectorViewModel selectorViewModel)
    {
        _noteService = noteService;
        _modalService = modalService;
        ClassifierSelectorViewModel = selectorViewModel;

        SaveNoteCommand = new AsyncRelayCommand(SaveNoteAsync);
        
    }

    public void GoToEditMode(NoteModel noteModel)
    {
        _editMode = true;
        _noteToEdit = (TModel)noteModel;
        Note.CompleteNoteDtoByNoteModel(_noteToEdit);
        ReloadFields();
    }

    protected virtual void ReloadFields()
    {
        Fields.Clear();
        Fields.Add(new CustomFieldViewModel("Title", Note.Title, s => Note.Title = s!));
        Fields.Add(new CustomFieldViewModel("Description", Note.Description, s => Note.Description = s));
        ClassifierSelectorViewModel.SelectCategory(Note.Category);
        ClassifierSelectorViewModel.SelectGroup(Note.Group);
    }

    private async Task SaveNoteAsync()
    {
        if (string.IsNullOrWhiteSpace(Note.Title))
            return;

        Note.Category = ClassifierSelectorViewModel.SelectedCategory;
       
        Note.Group = ClassifierSelectorViewModel.SelectedGroup;
        
        
        if (!_editMode)
        {
            _noteService.AddNote(CreateNoteFromDto());
            await _modalService.CloseModalAsync();
        }
        else
            _noteService.AddNote(EditNoteFromDto());

        OnEventInvoke?.Invoke();
       
    }

    protected abstract TModel CreateNoteFromDto();
    protected abstract TModel EditNoteFromDto();


    public event Action? OnEventInvoke;
}