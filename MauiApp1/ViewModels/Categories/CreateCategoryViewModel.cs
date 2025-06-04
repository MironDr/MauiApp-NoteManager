using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels.Categories;

public class CreateCategoryViewModel : BaseViewModel
{
    
    private readonly ICategoryService _categoryService;
    private readonly IPopupService _popupService;
    public CategoryDto Category { get; } =  new ();
    
    public AsyncRelayCommand SaveCategoryCommand { get; }
    
    public bool ClosePopup { get; set; } = true;
    
    
    public CreateCategoryViewModel(ICategoryService categoryService, IPopupService popupService)
    {
        SaveCategoryCommand = new AsyncRelayCommand(SaveCategory);
        _categoryService = categoryService;
        _popupService = popupService;

    }

    private async Task SaveCategory()
    {
        if (string.IsNullOrWhiteSpace(Category.CategoryName))
        {
            return;
        }

        var allCategories = _categoryService.GetCategories();
        bool duplicateTitle = allCategories.Any(c =>
            c.CategoryName.Equals(Category.CategoryName, StringComparison.OrdinalIgnoreCase));
            

        if (duplicateTitle)
        {
            await _popupService.ClosePopupAsync();
            await _popupService.AlertAsync("Error", "Category title must be unique", "Ok");
            return;
        }
        
        CategoryModel category = CategoryModel.CreateCategory(Category);
        await _categoryService.AddCategory(category);
        
        if (ClosePopup)
          await _popupService.ClosePopupAsync();
    }
}