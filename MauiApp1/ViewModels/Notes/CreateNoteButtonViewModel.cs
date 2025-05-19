using System.Windows.Input;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;
using CreateNoteView = MauiApp1.Views.Notes.CreateNoteView;

namespace MauiApp1.ViewModels.Notes;

public class CreateNoteButtonViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    private readonly NoteTypeSelectorViewModel _noteTypeSelectorViewModel;
    
    public ICommand CreateNoteCommand { get; }

    public CreateNoteButtonViewModel(IPopupService popupService,  NoteTypeSelectorViewModel noteTypeSelectorViewModel)
    {
        _popupService = popupService;
        _noteTypeSelectorViewModel = noteTypeSelectorViewModel;
     
        CreateNoteCommand = new Command(ShowTypeSelectorPopup);
    }

    private void ShowTypeSelectorPopup()
    {
        _popupService.ShowPopupAsyncWithParameter<CustomTypeSelectorView, NoteTypeSelectorViewModel>(_noteTypeSelectorViewModel);
    }
}