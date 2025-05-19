namespace MauiApp1.ViewModels;

public class CustomDatePickerViewModel : BaseViewModel
{
    private DateTime _selectedDate = DateTime.Today;

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            _selectedDate = value;
            OnPropertyChanged(nameof(SelectedDate));
            OnValueChanged?.Invoke(value);
        }
    }

    private Action<DateTime>? OnValueChanged { get; set; }
    public CustomDatePickerViewModel(Action<DateTime>? onValueChanged)
    {
        if(onValueChanged != null)
            OnValueChanged = onValueChanged;
    }
}