namespace MauiApp1.ViewModels.Notes;

public class CustomCheckBoxViewModel : BaseViewModel
{
    private string? _title;
    public string? Title
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
                
            }
        }
    }
    
    private bool _status;
    public bool Status
    {
        get => _status;
        set
        {
            if (_status != value)
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
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
                OnPropertyChanged(nameof(Status));
                
            }
        }
    }
    
    public bool IsNonEmptyField => !IsReadOnly || !string.IsNullOrWhiteSpace(_title);

    private Action<bool>? OnValueChanged { get; set; }

    public CustomCheckBoxViewModel(bool initialStatus, string? title, Action<bool>? onValueChanged, bool isReadOnly = false)
    {
        Title = title;
        Status = initialStatus;
        
        if(onValueChanged != null)
            OnValueChanged = onValueChanged;
        
        IsReadOnly = isReadOnly;
    }
}