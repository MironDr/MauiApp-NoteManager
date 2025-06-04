using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Repositories;

namespace MauiApp1.Services;


public interface IGroupService
{
    event EventHandler GroupsUpdated;
    List<GroupModel> GetGroups();
    Task AddGroup(GroupModel group);
    GroupModel? GetById(int id);
    
    Task DeleteGroup(GroupModel group);
}

public class GroupService : IGroupService
{
    private readonly IDatabaseRepository _repository;
    private List<GroupModel> _groups = new();
    
    public event EventHandler GroupsUpdated = null!;
    
    public GroupService(IDatabaseRepository repository)
    {
        _repository = repository;
        _ = LoadGroups();
    }

    private async Task LoadGroups()
    {
        try
        {
            _groups = await _repository.GetEntitiesAsync<GroupModel>();
            GroupsUpdated?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task AddGroup(GroupModel group)
    {
        bool titleExists = _groups.Any(g =>
                g.GroupName.Equals(group.GroupName, StringComparison.OrdinalIgnoreCase) &&
                g.Id != group.Id 
        );

        if (titleExists)
            throw new InvalidOperationException($"Group with title '{group.GroupName}' already exists.");
        
        await _repository.SaveNewEntityAsync(group);
        await LoadGroups();
    }
    
    public List<GroupModel> GetGroups()
    {
        return _groups;
    }
    
    public GroupModel? GetById(int id)
    {
        return _groups.FirstOrDefault(c => c.Id == id);
    }

    public async Task DeleteGroup(GroupModel group)
    {
        group.UnlinkAssociations();
        await _repository.DeleteEntityAsync(group);
        await LoadGroups();
    }
}
