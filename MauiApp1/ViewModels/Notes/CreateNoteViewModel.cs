using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;
using MauiApp1.Views.Categories;

namespace MauiApp1.ViewModels.Notes;

public class CreateNoteViewModel : BaseViewModel
{
    private readonly INoteService _noteService;
    private readonly IPopupService _popupService;
    
    public readonly CategorySelectorViewModel CategorySelectorViewModel;
    public NoteDto Note { get; } =  new ();
    
    public Command SaveNoteCommand { get; }
    
    public CreateNoteViewModel(INoteService noteService, IPopupService popupService,  CategorySelectorViewModel selectorViewModel)
    {
        SaveNoteCommand = new Command(SaveNote);
        _noteService = noteService;
        _popupService = popupService;
        CategorySelectorViewModel = selectorViewModel;
    }

    private void SaveNote()
    {
        if (string.IsNullOrWhiteSpace(Note.Title))
        {
            return;
        }
        
        Note.Category = CategorySelectorViewModel.SelectedCategory;
        _noteService.AddNote(NoteModel.CreateNote(Note));
        _popupService.ClosePopupAsync();
    }
}