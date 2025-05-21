using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public class SourceTypeSelectorViewModel : BaseViewModel
{
    public ObservableCollection<ReferenceType> Types { get; }
    public AsyncRelayCommand<ReferenceType> SelectTypeCommand { get; }
    
    public string TypeString { get; set; } = "HuI";
    
    public bool ShowSelectedInfo => true;
    
    
    private ReferenceType _selectedType;
    public ReferenceType SelectedType
    {
        get => _selectedType;
        set
        {
            _selectedType = value;
            OnPropertyChanged(nameof(SelectedType));
            TypeString = value.ToString();
            OnValueChanged?.Invoke(value);
        }
    }

    private Action<ReferenceType>? OnValueChanged { get; set; }
    
    public SourceTypeSelectorViewModel(Action<ReferenceType>? onValueChanged)
    {
        
        Types = new ObservableCollection<ReferenceType>(Enum.GetValues<ReferenceType>());
        SelectTypeCommand = new AsyncRelayCommand<ReferenceType>(OnSourceTypeSelected);
        if(onValueChanged != null)
            OnValueChanged = onValueChanged;
    }

    private Task OnSourceTypeSelected(ReferenceType selectedType)
    {
        SelectedType = selectedType;
        return Task.CompletedTask;
    }
    
    
    
}