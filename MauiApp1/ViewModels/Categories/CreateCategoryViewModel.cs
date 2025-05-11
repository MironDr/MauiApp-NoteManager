using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels.Categories;

public class CreateCategoryViewModel : BaseViewModel
{
    
    private readonly ICategoryService _categoryService;
    private readonly IPopupService _popupService;
    public CategoryDto Category { get; } =  new ();
    
    public Command SaveCategoryCommand { get; }
    
    public CreateCategoryViewModel(ICategoryService categoryService, IPopupService popupService)
    {
        SaveCategoryCommand = new Command(SaveCategory);
        _categoryService = categoryService;
        _popupService = popupService;

    }

    private void SaveCategory()
    {
        if (string.IsNullOrWhiteSpace(Category.CategoryName))
        {
            return;
        }
        
        
        _categoryService.AddCategory(CategoryModel.CreateCategory(Category));
        _popupService.ClosePopupAsync();
    }
}