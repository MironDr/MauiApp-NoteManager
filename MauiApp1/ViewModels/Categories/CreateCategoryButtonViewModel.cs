using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.Views.Categories;


namespace MauiApp1.ViewModels.Categories;

public class CreateCategoryButtonViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    
    public Command CreateCategoryCommand { get; }
    
    public CreateCategoryButtonViewModel(IPopupService popupService)
    {
        _popupService = popupService;
        CreateCategoryCommand = new Command(CreateCategory);
    }
    
    private void CreateCategory()
    {
        _popupService.ShowPopupAsync<CreateCategoryView>();
    }
}