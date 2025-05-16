using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels.Notes;

public class TextBlocksViewModel : BaseViewModel
{
    public ObservableCollection<CustomFieldViewModel> Blocks { get; } = new();

    private List<string?> _blocksContent = new();
    public TextBlocksViewModel()
    {
        _blocksContent.Add(string.Empty);
        Blocks.Add(new CustomFieldViewModel(
            "Text",
            _blocksContent[0],
            s => _blocksContent[0] = s
        ));
        
        _blocksContent.Add(string.Empty);
        Blocks.Add(new CustomFieldViewModel(
            "Text1",
            _blocksContent[1],
            s => _blocksContent[0] = s
        ));
    }
}