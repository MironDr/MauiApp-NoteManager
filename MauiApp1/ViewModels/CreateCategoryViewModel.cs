using MauiApp1.DTOs;
using MauiApp1.Services;
using MauiApp1.Utilities;

namespace MauiApp1.ViewModels;

public class CreateCategoryViewModel : BaseViewModel
{
    
    private readonly ICategoryService _categoryService;
    private readonly IPopupService _popupService;
    public CategoryDTO Category { get; set; } =  new ();
    
    public Command SaveCategoryCommand { get; }
    
    public CreateCategoryViewModel(ICategoryService categoryService, IPopupService popupService)
    {
        SaveCategoryCommand = new Command(SaveCategory);
        _categoryService = categoryService;
        _popupService = popupService;
    }

    private void SaveCategory()
    {
        _categoryService.AddCategory(Category);
        _popupService.ClosePopupAsync();
    }
}