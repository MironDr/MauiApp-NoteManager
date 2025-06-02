using MauiApp1.Models;
using SQLite;

namespace MauiApp1.Repositories;

public interface IDatabaseRepository
{
    Task<List<T>> GetEntitiesAsync<T>()  where T : new();

    Task<int> ReplaceEntityAsync<T>(T entity) where T : BaseModel;

    Task<int> SaveNewEntityAsync<T>(T entity) where T : BaseModel;

    Task<int> DeleteEntityAsync<T>(T entity);
}

public class DatabaseRepository : IDatabaseRepository
{
    private SQLiteAsyncConnection _database;


    public DatabaseRepository()
    {
        if (_database != null)
            return;

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.sqlite");
        _database = new SQLiteAsyncConnection(dbPath);
        
        _database.CreateTableAsync<CategoryModel>();
        _database.CreateTableAsync<GroupModel>();
        _database.CreateTableAsync<ProtectionProfileModel>();
        _database.CreateTableAsync<TextNoteModel>();
        _database.CreateTableAsync<AccountNoteModel>();
        _database.CreateTableAsync<SourceNoteModel>();
        _database.CreateTableAsync<CheckListNoteModel>();
    }
    
    public Task<List<T>> GetEntitiesAsync<T>() where T : new()
    {
        return _database.Table<T>().ToListAsync();
    }
    
    public Task<int> SaveNewEntityAsync<T>(T entity) where T : BaseModel
    {
       return _database.InsertAsync(entity);
    } 
    
    public Task<int> ReplaceEntityAsync<T>(T entity) where T : BaseModel
    {
        return _database.InsertOrReplaceAsync(entity);
    } 
    
    public Task<int> DeleteEntityAsync<T>(T entity)
    {
        return _database.DeleteAsync(entity);
    } 
}