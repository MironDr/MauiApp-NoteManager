using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels.Notes;

public class CustomQuotesContainerViewModel : BaseViewModel
{
    public ObservableCollection<CustomInfoViewModel> Fields { get; } = new();
    public CreateNoteWithSourceButtonViewModel CreateButtonViewModel { get; set; }

    public CustomQuotesContainerViewModel(CreateNoteWithSourceButtonViewModel createButtonViewModel)
    {
        CreateButtonViewModel = createButtonViewModel;
    }
}