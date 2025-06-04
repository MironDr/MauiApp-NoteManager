using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Utilities;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.ViewModels.Notes.Managers;

public abstract class ManageNoteViewModel<TDto, TModel> : BaseViewModel, IEditableNoteViewModel, ICategorySelectable, IEventHandler
    where TDto : NoteDto, new()
    where TModel : NoteModel
{
    private readonly INoteService _noteService;
    private readonly IModalService _modalService;
    private readonly IPopupService _popupService;
    private readonly ProfileToNoteSelectorButtonViewModel _profileToNoteSelectorButtonViewModel;
    public ClassifierSelectorViewModel ClassifierSelectorViewModel { get; }

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();

    public TDto Note { get; } = new();
    
    public IAsyncRelayCommand SaveNoteCommand { get; }
    
    public bool IsEditMode { get; private set; } 

    protected TModel _noteToEdit;
    
    
    protected ManageNoteViewModel(INoteService noteService, IModalService modalService, IPopupService popupService, ClassifierSelectorViewModel selectorViewModel, ProfileToNoteSelectorButtonViewModel profileToNoteSelectorButtonViewModel)
    {
        _noteService = noteService;
        _modalService = modalService;
        _popupService = popupService;
        _profileToNoteSelectorButtonViewModel = profileToNoteSelectorButtonViewModel;
        ClassifierSelectorViewModel = selectorViewModel;

        SaveNoteCommand = new AsyncRelayCommand(SaveNoteAsync);

        

    }

    public void GoToEditMode(NoteModel noteModel)
    {
        IsEditMode = true;
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
        ClassifierSelectorViewModel.SelectGroup(Note.Group, Note.IsMainInGroup);
    }

   
    private async Task SaveNoteAsync()
    {
        string? result = Note.CheckRequiredFields();
        
        if (!string.IsNullOrWhiteSpace(result))
        {
            await _popupService.AlertAsync("Error", $"{result} is required", "Ok");
            return;
        }

        var allNotes = await _noteService.GetNotes();
        bool duplicateTitle = !IsEditMode
            ? allNotes.Any(n => n.Title.Equals(Note.Title, StringComparison.OrdinalIgnoreCase))
            : allNotes.Any(n => n.Title.Equals(Note.Title, StringComparison.OrdinalIgnoreCase) && n.Id != _noteToEdit.Id);

        if (duplicateTitle)
        {
            await _popupService.AlertAsync("Error", "Note title must be unique", "Ok");
            return;
        }
        
        Note.Category = ClassifierSelectorViewModel.SelectedCategory;
       
        Note.Group = ClassifierSelectorViewModel.SelectedGroup;
        
        if(Note.Group != null)
            Note.IsMainInGroup = ClassifierSelectorViewModel.IsNoteMainInGroup;

        if (Note.Group == null && Note.Category == null)
        {
            await _popupService.AlertAsync("Error", "You should select Group or Category", "Ok");
            return;
        }

        if (!IsEditMode)
        {
            bool answer = await _popupService.AlertConfirmAsync(
                "Secure",          
                "Want to set up a security profile for this note?"
            );

            if (answer)
            {
                Note.ProtectionProfile = await _profileToNoteSelectorButtonViewModel.Open();
                if(Note.ProtectionProfile == null)
                    return;
                
         
            }

            await _noteService.AddNote(CreateNoteFromDto());
            await _modalService.CloseModalAsync();
        }
        else
            await _noteService.AddNote(EditNoteFromDto());
        
        OnEventInvoke?.Invoke();
       
    }

   
    

    protected abstract TModel CreateNoteFromDto();
    protected abstract TModel EditNoteFromDto();


    public event Action? OnEventInvoke;
}