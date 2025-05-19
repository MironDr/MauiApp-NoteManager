using System.Windows.Input;
using MauiApp1.Services;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public class CreateNoteWithSourceButtonViewModel : BaseViewModel
{
    private readonly IModalService _modalService;
    public CreateNoteWithSourceViewModel CreateNoteWithSourceViewModel { get;  }

    public ICommand CreateNoteCommand { get; }

    public CreateNoteWithSourceButtonViewModel(IModalService modalService,  CreateNoteWithSourceViewModel createNoteWithSourceViewModel)
    {
        _modalService = modalService;
        CreateNoteWithSourceViewModel = createNoteWithSourceViewModel;
     
        CreateNoteCommand = new Command(ShowView);
    }

    private void ShowView()
    {
        _modalService.ShowModalAsyncWithParameter<CreateNoteWithSourceView, CreateNoteWithSourceViewModel>(CreateNoteWithSourceViewModel);
    }
}