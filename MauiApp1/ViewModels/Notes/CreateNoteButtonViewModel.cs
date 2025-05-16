using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;
using CreateNoteView = MauiApp1.Views.Notes.CreateNoteView;

namespace MauiApp1.ViewModels.Notes;

public class CreateNoteButtonViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    private readonly NoteItemFactoryManager _factoryManager;
    private readonly NoteTypeSelectorViewModel _noteTypeSelectorViewModel;
    
    public Command CreateNoteCommand { get; }

    public CreateNoteButtonViewModel(IPopupService popupService, NoteItemFactoryManager factoryManager, NoteTypeSelectorViewModel noteTypeSelectorViewModel)
    {
        _popupService = popupService;
        _factoryManager = factoryManager;
        _noteTypeSelectorViewModel = noteTypeSelectorViewModel;
     
        CreateNoteCommand = new Command(ShowTypeSelectorPopup);
    }

    private void ShowTypeSelectorPopup()
    {
        var selectorViewModel = _noteTypeSelectorViewModel;
        _popupService.ShowPopupAsyncWithParameter<NoteTypeSelectorView, NoteTypeSelectorViewModel>(selectorViewModel);
    }
}