using MauiApp1.DTOs;
using MauiApp1.Models;

namespace MauiApp1.Services;


public interface ICategoryService
{
    event EventHandler CategoriesUpdated;
    List<CategoryModel> GetCategories();
    void AddCategory(CategoryModel category);
}
public class CategoryService : ICategoryService
{
    private readonly List<CategoryModel> _categories = new();
    
    public event EventHandler CategoriesUpdated = null!;
    
    public CategoryService()
    {
        LoadCategories();
    }

    private void LoadCategories()
    {
        _categories.Add(CategoryModel.CreateCategory(new CategoryDto()
        {
            CategoryName = "Category 1"
        }));
        _categories.Add(CategoryModel.CreateCategory(new CategoryDto()
        {
            CategoryName = "Category 2"
        }));
 
    }

    public void AddCategory(CategoryModel category)
    {
        _categories.Add(category);
        CategoriesUpdated?.Invoke(this, EventArgs.Empty);
    }
    
    public List<CategoryModel> GetCategories()
    {
        return _categories;
    }
    
    
}