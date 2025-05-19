using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels.Notes;

public struct CustomInfoStruct
{
    public string? Title {get; private set; }
    public string? Content  {get; private set; }

    public CustomInfoStruct(string? title, string? content)
    {
        this.Title = title;
        this.Content = content;
    }
}



public class CustomInfoViewModel : BaseViewModel
{
    public ObservableCollection<CustomInfoStruct> Fields { get; }

    public CustomInfoViewModel(IEnumerable<CustomInfoStruct> fields)
    {
        Fields = new ObservableCollection<CustomInfoStruct>(fields);
    }
}