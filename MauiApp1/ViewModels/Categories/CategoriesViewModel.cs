using System.Collections.ObjectModel;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.VisualModels;


namespace MauiApp1.ViewModels.Categories;

public class CategoriesViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;


    public event EventHandler<CategoryModel?>? CategorySelected;

  
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

    private  CategoryButtonModel? _selectedCategory = null;
    
    

    public CategoriesViewModel(ICategoryService categoryService) : base()
    {
        _categoryService = categoryService;
        _categoryService.CategoriesUpdated += OnCategoriesUpdated!;
        
        LoadCategories();
    }
    
    private void LoadCategories()
    {
        _selectedCategory = null;
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
        if (_selectedCategory != null)
        {
            _selectedCategory.ChangeSelected(false);

            if (_selectedCategory.Equals(selectedCategoryModel))
            {
                _selectedCategory = null;
                SelectCategory(_selectedCategory?.Category);
                return;
            }
            
        }
        
        selectedCategoryModel.ChangeSelected(true);
        _selectedCategory = selectedCategoryModel;
        SelectCategory(_selectedCategory.Category);
        
    }
    private void SelectCategory(CategoryModel? category)
    {
        CategorySelected?.Invoke(this, category);
        
    }
    
}