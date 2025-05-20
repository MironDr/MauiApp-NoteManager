using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp1.ViewModels.Notes;

public class CheckListViewModel : BaseViewModel
{
    public ObservableCollection<CustomCheckBoxViewModel> Boxes { get; } = new();

    public IAsyncRelayCommand AddNewBoxCommand { get; }

    private readonly List<bool> _boxesStatuses = new();
    
    
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
                foreach (var field in Boxes)
                {
                    field.IsReadOnly = _isReadOnly;
                }
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }
    }

    public CheckListViewModel()
    {
        AddNewBoxCommand = new AsyncRelayCommand(AddNewBox);
    }

    private Task AddNewBox()
    {
        _boxesStatuses.Add(false);
        Boxes.Add(new CustomCheckBoxViewModel(
            _boxesStatuses[^1],
            string.Empty,
            s => _boxesStatuses[^1] = s,
            IsReadOnly
        ));
        return Task.CompletedTask;
    }


    public void SetReadOnly()
    {
        IsReadOnly = true;
    }
  
}