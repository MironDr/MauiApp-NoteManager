using MauiApp1.Base;

namespace MauiApp1.DTOs;

public class CategoryDTO : BaseCommon
{
    private string _categoryName;


    public string CategoryName
    {
        get => _categoryName;
        set
        {
            _categoryName = value;
            OnPropertyChanged(nameof(CategoryName));
        }
    }


  
}