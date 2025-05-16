using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;


namespace MauiApp1.ViewModels.Notes;

public abstract class ManageNoteViewModel<TDto, TModel> : BaseViewModel, IEditableNoteViewModel, ICategorySelectable
    where TDto : NoteDto, new()
    where TModel : NoteModel
{
    protected readonly INoteService _noteService;
    protected readonly IPopupService _popupService;

    public CategorySelectorViewModel CategorySelectorViewModel { get; }

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();

    public TDto Note { get; } = new();

    public IAsyncRelayCommand SaveNoteCommand { get; }

    protected bool _editMode = false;
    protected TModel _noteToEdit;

    protected ManageNoteViewModel(INoteService noteService, IPopupService popupService, CategorySelectorViewModel selectorViewModel)
    {
        _noteService = noteService;
        _popupService = popupService;
        CategorySelectorViewModel = selectorViewModel;

        SaveNoteCommand = new AsyncRelayCommand(SaveNoteAsync);
        ReloadFields();
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
        CategorySelectorViewModel.SelectCategoryById(Note.Category);
    }

    protected async Task SaveNoteAsync()
    {
        if (string.IsNullOrWhiteSpace(Note.Title))
            return;

        Note.Category = CategorySelectorViewModel.SelectedCategory?.Id;

        if (!_editMode)
            _noteService.AddNote(CreateNoteFromDto());
        else
            _noteService.AddNote(EditNoteFromDto());

        await _popupService.ClosePopupAsync();
    }

    protected abstract TModel CreateNoteFromDto();
    protected abstract TModel EditNoteFromDto();
}