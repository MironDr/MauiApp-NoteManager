namespace MauiApp1.Models;

public class CategoryModel : BaseModel
{
    public int Id { get; set; }
    
    private string _name;
    
    public string Name {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
    
}