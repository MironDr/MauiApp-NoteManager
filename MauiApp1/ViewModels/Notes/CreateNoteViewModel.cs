using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;
using MauiApp1.Views.Categories;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public class CreateNoteViewModel : BaseViewModel
{
    private readonly INoteService _noteService;
    private readonly IPopupService _popupService;
    
    public readonly CategorySelectorViewModel CategorySelectorViewModel;

    public ObservableCollection<CustomFieldViewModel> Fields { get; set; } = new();
    
    public NoteDto Note { get; } =  new ();
    
    public ICommand SaveNoteCommand { get; }
    
    public CreateNoteViewModel(INoteService noteService, IPopupService popupService,  CategorySelectorViewModel selectorViewModel)
    {
        SaveNoteCommand = new Command(SaveNote);
        _noteService = noteService;
        _popupService = popupService;
        CategorySelectorViewModel = selectorViewModel;
        
        
       ReloadFields();
    }

    private void ReloadFields()
    {
        Fields.Clear();
        Fields.Add(new CustomFieldViewModel("Title", Note.Title, s => Note.Title = s!));
        Fields.Add(new CustomFieldViewModel("Description", Note.Description, s => Note.Description = s));
 
    }

    public void SetData(NoteDto note)
    {
        
    }
    
    private void SaveNote()
    {
        if (string.IsNullOrWhiteSpace(Note.Title))
        {
            return;
        }
        
        Note.Category = CategorySelectorViewModel.SelectedCategory?.Id;
        _noteService.AddNote(NoteModel.CreateNote(Note));
        _popupService.ClosePopupAsync();
    }
}