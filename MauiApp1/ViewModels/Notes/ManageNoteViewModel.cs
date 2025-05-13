using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;


namespace MauiApp1.ViewModels.Notes;

public class ManageNoteViewModel : BaseViewModel
{
    private readonly INoteService _noteService;
    private readonly IPopupService _popupService;
    
    public readonly CategorySelectorViewModel CategorySelectorViewModel;

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();
    
    public NoteDto Note { get; } =  new ();
    
    public IAsyncRelayCommand SaveNoteCommand { get; }
    
    private bool _editMode = false;
    private NoteModel _noteToEdit;
    
    public ManageNoteViewModel(INoteService noteService, IPopupService popupService,  CategorySelectorViewModel selectorViewModel)
    {
        SaveNoteCommand = new AsyncRelayCommand(SaveNoteAsync);
        _noteService = noteService;
        _popupService = popupService;
        CategorySelectorViewModel = selectorViewModel;
        
        
       ReloadFields();
    }

    public void GoToEditMode(NoteModel noteModel)
    {
        _editMode = true;
        _noteToEdit = noteModel;
        Note.CompleteNoteDtoByNoteModel(noteModel);
        ReloadFields();
    }

    private void ReloadFields()
    {
        Fields.Clear();
        Fields.Add(new CustomFieldViewModel("Title", Note.Title, s => Note.Title = s!));
        Fields.Add(new CustomFieldViewModel("Description", Note.Description, s => Note.Description = s));
        CategorySelectorViewModel.SelectCategoryById(Note.Category);
 
    }
    
    private async Task SaveNoteAsync()
    {
        if (string.IsNullOrWhiteSpace(Note.Title))
        {
            return;
        }
        
        Note.Category = CategorySelectorViewModel.SelectedCategory?.Id;
        
        if(!_editMode)
            _noteService.AddNote(NoteModel.CreateNote(Note));
        else
            _noteService.AddNote(_noteToEdit.EditNote(Note));
        
        await _popupService.ClosePopupAsync();
    }
}