using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels.Notes;

public class CustomQuotesContainerViewModel : BaseViewModel
{
    public ObservableCollection<CustomInfoViewModel> Fields { get; } = new();
    public CreateNoteWithSourceButtonViewModel CreateButtonViewModel { get; set; }

    public DeleteNoteWithSourceButtonViewModel DeleteButtonViewModel { get; set; }
    
    public CustomQuotesContainerViewModel(CreateNoteWithSourceButtonViewModel createButtonViewModel, DeleteNoteWithSourceButtonViewModel deleteButtonViewModel)
    {
        CreateButtonViewModel = createButtonViewModel;
        DeleteButtonViewModel = deleteButtonViewModel;
    }
}