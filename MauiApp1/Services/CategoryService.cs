using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Repositories;

namespace MauiApp1.Services;


public interface ICategoryService
{
    event EventHandler CategoriesUpdated;
    List<CategoryModel> GetCategories();
    Task AddCategory(CategoryModel category);
    
    CategoryModel? GetById(int id);
    
    Task DeleteCategory(CategoryModel category);
}
public class CategoryService : ICategoryService
{
    private readonly IDatabaseRepository _repository;
    private List<CategoryModel> _categories = new();
    
    public event EventHandler CategoriesUpdated = null!;
    
    public CategoryService(IDatabaseRepository repository)
    {
        _repository = repository;
        _ = LoadCategories();
    }

    private async Task LoadCategories()
    {
        try
        {
            _categories = await _repository.GetEntitiesAsync<CategoryModel>();
            CategoriesUpdated?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task AddCategory(CategoryModel category)
    {
        bool titleExists = _categories.Any(c =>
            c.CategoryName.Equals(category.CategoryName, StringComparison.OrdinalIgnoreCase) &&
            c.Id != category.Id 
        );

        if (titleExists)
            throw new InvalidOperationException($"Category with title '{category.CategoryName}' already exists.");
        
        await _repository.SaveNewEntityAsync(category);
        await LoadCategories();
    }
    
    public List<CategoryModel> GetCategories()
    {
        return _categories;
    }
    
    public CategoryModel? GetById(int id)
    {
        return _categories.FirstOrDefault(c => c.Id == id);
    }

    public async Task DeleteCategory(CategoryModel category)
    {
        category.UnlinkAssociations();
        await _repository.DeleteEntityAsync(category);
        await LoadCategories();
    }
}