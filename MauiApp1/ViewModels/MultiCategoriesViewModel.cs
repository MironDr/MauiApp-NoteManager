using System.Collections.ObjectModel;
using MauiApp1.Base;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels;

public class MultiCategoriesViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;
    
    
    private ObservableCollection<CategoryButtonModel>? _categories;

    public ObservableCollection<CategoryButtonModel> Categories
    {
        get => _categories;
        set
        {
            if (_categories != value)
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
    }

    private HashSet<CategoryButtonModel> _selectedCategories = new HashSet<CategoryButtonModel>();
    
    public MultiCategoriesViewModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        Categories = new ObservableCollection<CategoryButtonModel>(
            _categoryService.GetCategories().Select(c => new CategoryButtonModel { Category = c, Command = new Command<CategoryButtonModel>(CategoryChooseAction)})
        );
    }
    
    private void CategoryChooseAction(CategoryButtonModel selectedCategoryModel)
    {
        
        foreach (var item in _selectedCategories)
        {
         
            if (item.Equals(selectedCategoryModel))
            {
                selectedCategoryModel.ChangeSelected(false);
                _selectedCategories.Remove(selectedCategoryModel);
                return;
            }
        }
       
        
        selectedCategoryModel.ChangeSelected(true);
        _selectedCategories.Add(selectedCategoryModel);

        

    }
}