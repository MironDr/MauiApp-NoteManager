using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;

namespace MauiApp1.ViewModels.Notes;

public class TextBlocksViewModel : BaseViewModel
{
    public ObservableCollection<CustomFieldViewModel> Blocks { get; } = new();

    public IAsyncRelayCommand AddNewBlockCommand { get; }

    public IAsyncRelayCommand<CustomFieldViewModel> DeleteBlockCommand { get; }
    
    private readonly List<string?> _blocksContent = new();
    
    
    private bool _isReadOnly = false;
    public bool IsEdit => !_isReadOnly;
    
    public bool IsReadOnly
    {
        get => _isReadOnly;
        set
        {
            if (_isReadOnly != value)
            {
                _isReadOnly = value;
                foreach (var field in Blocks)
                {
                    field.IsReadOnly = _isReadOnly;
                }
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }
    }

    public TextBlocksViewModel()
    {
        AddNewBlockCommand = new AsyncRelayCommand(AddNewBlock);
        DeleteBlockCommand = new AsyncRelayCommand<CustomFieldViewModel>(DeleteBlock!);
    }

    private Task AddNewBlock()
    {
        _blocksContent.Add(string.Empty);
        Blocks.Add(new CustomFieldViewModel(
            "Block",
            _blocksContent[^1],
            s => _blocksContent[^1] = s,
            IsReadOnly
        ));
        return Task.CompletedTask;
    }


    private Task DeleteBlock(CustomFieldViewModel customFieldViewModel)
    {
        Blocks.Remove(customFieldViewModel);
        return Task.CompletedTask;
    }

    public void SetReadOnly()
    {
        IsReadOnly = true;
    }
  
}