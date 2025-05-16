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
    public Command CreateNoteCommand { get; }
    
    public CreateNoteButtonViewModel(IPopupService popupService, NoteItemFactoryManager factoryManager)
    {
        _popupService = popupService;
        _factoryManager = factoryManager;
        CreateNoteCommand = new Command(CreateNote);
    }
    
    private void CreateNote()
    {
        var view = _factoryManager.GetEditorViewModel(NoteType.Text);
        if (view != null) _popupService.ShowPopupAsyncWithParameter<CreateNoteView, BaseViewModel>(view);
    }
}