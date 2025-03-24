using System.ComponentModel;
using System.Windows.Input;


namespace MauiApp1.Models;

public class CategoryButtonModel : BaseModel
{
    
    public CategoryModel Category { get; set; }
    public ICommand Command { get; set; }
    
    private Style _style = (Style)Application.Current.Resources["ButtonCategoryNotSelected"];

    public Style Style
    {
        get => _style;
        set
        {
            if (_style != value)
            {
                _style = value;
                OnPropertyChanged(nameof(Style));
            }
        }
    }
    
    public void ChangeSelected(bool selected)
    {
        
        Style = selected ? (Style)Application.Current.Resources["ButtonCategorySelected"] : (Style)Application.Current.Resources["ButtonCategoryNotSelected"];
       

    }

    
    
    
}