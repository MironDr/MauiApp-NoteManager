using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.VisualModels;


namespace MauiApp1.ViewModels.Categories;

public class CategoriesViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;
    private readonly IPopupService _popupService;
    public event EventHandler<CategoryModel?>? CategorySelected;

    
    public AsyncRelayCommand DeleteCategoryCommand { get; }
  
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
    
    

    public CategoriesViewModel(ICategoryService categoryService, IPopupService popupService) : base()
    {
        _categoryService = categoryService;
        _popupService = popupService;
        _categoryService.CategoriesUpdated += OnCategoriesUpdated!;
        DeleteCategoryCommand = new AsyncRelayCommand(OnCategoryDeleted);
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
    
    private async Task OnCategoryDeleted()
    {
        if (_selectedCategory == null)
        {
            await _popupService.AlertAsync("Warning", "Select category to delete", "Ok");
            return;
        }

        bool answer = await _popupService.AlertConfirmAsync(
            "Warning",          
            "Are you sure you want to delete this category?"
        );
        
        if(!answer)
            return;

        if (_selectedCategory.Category.GetNotes().Count > 0)
        {
            await _popupService.AlertAsync("Error", "You cannot delete non empty categories", "Ok");
            return;
        }
        
        _categoryService.DeleteCategory(_selectedCategory.Category);
        _selectedCategory = null;
        SelectCategory(_selectedCategory?.Category);
    }
    private void SelectCategory(CategoryModel? category)
    {
        CategorySelected?.Invoke(this, category);
        
    }
    
}