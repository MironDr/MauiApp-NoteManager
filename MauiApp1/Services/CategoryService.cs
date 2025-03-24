using MauiApp1.Models;

namespace MauiApp1.Services;


public interface ICategoryService
{
    List<CategoryModel> GetCategories();
}
public class CategoryService : ICategoryService
{
    private readonly List<CategoryModel> _categories = new List<CategoryModel>();
    
    public CategoryService()
    {
        LoadCategories();
    }

    private void LoadCategories()
    {
        _categories.Add(new CategoryModel{Id = 1, Name = "Продукты"});
        _categories.Add(new CategoryModel{Id = 2, Name = "Техника"});
    }
    
    public List<CategoryModel> GetCategories()
    {
        return _categories;
    }
    
    
}