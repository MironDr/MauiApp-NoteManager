namespace MauiApp1.ViewModels;

public class CustomFieldViewModel : BaseViewModel
{
    public string Label { get; set; } 
    
    private string? _value;
    public string? Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
                OnValueChanged?.Invoke(value); 
            }
        }
    }
    
    private bool _isReadOnly;

    public bool IsReadOnly
    {
        get => _isReadOnly;
        set
        {
            if (_isReadOnly != value)
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
    
    public bool IsNonEmptyField => !IsReadOnly || !string.IsNullOrWhiteSpace(_value);

    private Action<string?>? OnValueChanged { get; set; }

    public CustomFieldViewModel(string label, string? initialValue, Action<string?>? onValueChanged, bool isReadOnly = false)
    {
        Label = label;
        Value = initialValue;
        
        if(onValueChanged != null)
            OnValueChanged = onValueChanged;
        
        IsReadOnly = isReadOnly;
    }
}