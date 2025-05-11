using System.Collections.ObjectModel;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.View.Categories;
using MauiApp1.VisualModels;

namespace MauiApp1.ViewModels.Categories;

public class MultiCategoriesViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;


    private ObservableCollection<CategoryButtonModel> _categories = new();

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

    private readonly HashSet<CategoryButtonModel> _selectedCategories = new();
    
    

    public MultiCategoriesViewModel(ICategoryService categoryService) : base()
    {
        _categoryService = categoryService;
        _categoryService.CategoriesUpdated += OnCategoriesUpdated!;
        
        LoadCategories();
    }
    
    private void LoadCategories()
    {
        _selectedCategories.Clear();
        Categories = new ObservableCollection<CategoryButtonModel>(
            _categoryService.GetCategories().Select(c => new CategoryButtonModel { Category = c, Command = new Command<CategoryButtonModel>(CategoryChooseAction)})
        );
    
    }
    
    private void OnCategoriesUpdated(object sender, EventArgs e)
    {
        LoadCategories();
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