using MauiApp1.Services;
using MauiApp1.View.Categories;
using CreateNoteView = MauiApp1.Views.Notes.CreateNoteView;

namespace MauiApp1.ViewModels.Notes;

public class CreateNoteButtonViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    
    public Command CreateNoteCommand { get; }
    
    public CreateNoteButtonViewModel(IPopupService popupService)
    {
        _popupService = popupService;
        CreateNoteCommand = new Command(CreateNote);
    }
    
    private void CreateNote()
    {
        _popupService.ShowPopupAsync<CreateNoteView>();
    }
}