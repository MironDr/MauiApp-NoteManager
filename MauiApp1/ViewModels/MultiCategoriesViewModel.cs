using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MauiApp1.Base;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Utilities;
using MauiApp1.View;
using MauiApp1.VisualModels;

namespace MauiApp1.ViewModels;

public class MultiCategoriesViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;
    private readonly IPopupService _popupService;


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

    private HashSet<CategoryButtonModel> _selectedCategories = new();
    
    
    public Command CreateCategoryCommand { get; }
    public MultiCategoriesViewModel(ICategoryService categoryService, IPopupService popupService) : base()
    {
        _categoryService = categoryService;
        _popupService = popupService;
        
        _categoryService.CategoriesUpdated += OnCategoriesUpdated;
        CreateCategoryCommand = new Command(CreateCategory);
        
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
    
    
    private void CreateCategory()
    {
        _popupService.ShowPopupAsync<CreateCategoryView>();
    }
}