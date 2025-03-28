using MauiApp1.DTOs;
using MauiApp1.Models;

namespace MauiApp1.Services;


public interface ICategoryService
{
    event EventHandler CategoriesUpdated;
    List<CategoryModel> GetCategories();
    void AddCategory(CategoryDTO categoryDTO);
}
public class CategoryService : ICategoryService
{
    private readonly List<CategoryModel> _categories = new();
    
    public event EventHandler CategoriesUpdated;
    
    public CategoryService()
    {
        LoadCategories();
    }

    private void LoadCategories()
    {
        _categories.Add(new CategoryModel{Id = 1, CategoryName = "Products"});
        _categories.Add(new CategoryModel{Id = 2, CategoryName = "Foods"});
 
    }

    public void AddCategory(CategoryDTO categoryDTO)
    {
        _categories.Add(new CategoryModel{Id = 3, CategoryName = categoryDTO.CategoryName});
        CategoriesUpdated?.Invoke(this, EventArgs.Empty);
    }
    
    public List<CategoryModel> GetCategories()
    {
        return _categories;
    }
    
    
}