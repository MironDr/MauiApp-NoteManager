using System.Windows.Input;
using MauiApp1.Services;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public class DeleteNoteWithSourceButtonViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    public DeleteNoteWithSourceViewModel DeleteNoteWithSourceViewModel { get; set; }

    public ICommand DeleteNoteCommand { get; }

    public DeleteNoteWithSourceButtonViewModel(IPopupService popupService)
    {
        _popupService = popupService;
        DeleteNoteWithSourceViewModel = new DeleteNoteWithSourceViewModel(popupService);
        DeleteNoteCommand = new Command(ShowView);
    }
    private void ShowView()
    {
        _popupService.ShowPopupAsyncWithParameter<DeleteNoteWithSourceView, DeleteNoteWithSourceViewModel>(DeleteNoteWithSourceViewModel, true);
    }
}