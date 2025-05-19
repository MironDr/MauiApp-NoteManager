using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;


namespace MauiApp1.ViewModels.Categories;

public class CategorySelectorViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService; 
  
    public CreateCategoryButtonViewModel CreateCategoryButtonViewModel { get; }

    public ICommand CategorySelectedCommand { get; }
    
    
    private ObservableCollection<CategoryModel> _categories = new();
    private CategoryModel? _selectedCategory;
    private string _selectedCategoryName = "Select a Category";

    private bool _isListVisible = false;

    public bool IsListVisible
    {
        get => _isListVisible;
        set
        {
            if (value != _isListVisible)
            {
                _isListVisible = value;
                OnPropertyChanged(nameof(IsListVisible));
            }
        }
    }

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

    public CategorySelectorViewModel(ICategoryService categoryService, CreateCategoryButtonViewModel createCategoryButtonViewModel)
    {
        CategorySelectedCommand = new Command<CategoryModel>(OnCategorySelected);
        _categoryService = categoryService;
      
        CreateCategoryButtonViewModel = createCategoryButtonViewModel;
        
        
        
        Categories = new ObservableCollection<CategoryModel>(_categoryService.GetCategories().Where(c => c.Id != SelectedCategory?.Id));

        ToggleCategoryListCommand = new Command(() =>
        {
            Categories = new ObservableCollection<CategoryModel>(_categoryService.GetCategories().Where(c => c.Id != SelectedCategory?.Id));
            IsListVisible = !IsListVisible; 
        });
    }

    public void SelectCategoryById(int? categoryId)
    {
        if(categoryId != null)
            SelectedCategory = _categories.FirstOrDefault(c => c.Id == categoryId);
    }
    
    public void OnCategorySelected(CategoryModel category)
    {
        SelectedCategory = category;
        Categories = new ObservableCollection<CategoryModel>(_categoryService.GetCategories().Where(c => c.Id != SelectedCategory?.Id));
        IsListVisible = false; 
    }

   
}