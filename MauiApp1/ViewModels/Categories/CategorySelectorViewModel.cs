using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.Models;
using MauiApp1.Services;


namespace MauiApp1.ViewModels.Categories;

public class CategorySelectorViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;
    
    public ICommand CategorySelectedCommand { get; }
    
    private ObservableCollection<CategoryModel> _categories = new();
    private CategoryModel? _selectedCategory;
    private string _selectedCategoryName = "Select a Category";


    public string SelectedCategoryName
    {
        get => _selectedCategoryName;
        set
        {
            if (_selectedCategoryName != value)
            {
                _selectedCategoryName = value;
                OnPropertyChanged(nameof(SelectedCategoryName));
            }
        }
    } 
    public ObservableCollection<CategoryModel> Categories
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

    public CategoryModel? SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory != value && value != null)
            {
                _selectedCategory = value;
                SelectedCategoryName = _selectedCategory.CategoryName;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
    }
    
    public ICommand ToggleCategoryListCommand { get; }

    public CategorySelectorViewModel(ICategoryService categoryService)
    {
        CategorySelectedCommand = new Command<CategoryModel>(OnCategorySelected);
        _categoryService = categoryService;
        
        Categories = new();

        ToggleCategoryListCommand = new Command(() =>
        {
            if (Categories.Count > 0)
            {
                Categories.Clear();
                return;
            }

            Categories = new ObservableCollection<CategoryModel>(categoryService.GetCategories());
        });
    }

    private void OnCategorySelected(CategoryModel category)
    {
        SelectedCategory = category;
        Categories.Clear();
    }
}