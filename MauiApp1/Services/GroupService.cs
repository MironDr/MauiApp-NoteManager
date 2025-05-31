using MauiApp1.DTOs;
using MauiApp1.Models;

namespace MauiApp1.Services;


public interface IGroupService
{
    event EventHandler GroupsUpdated;
    List<GroupModel> GetGroups();
    void AddGroup(GroupModel group);
    GroupModel? GetById(int id);
    
    void DeleteGroup(GroupModel group);
}

public class GroupService : IGroupService
{
    
    private readonly List<GroupModel> _groups = new();
    
    public event EventHandler GroupsUpdated = null!;
    
    public GroupService()
    {
        LoadGroups();
    }

    private void LoadGroups()
    {
        //_groups.Add(GroupModel.CreateGroup(new GroupDto{GroupName = "Group1"}));
        //_groups.Add(GroupModel.CreateGroup(new GroupDto{GroupName = "Group2"}));
    }

    public void AddGroup(GroupModel group)
    {
        _groups.Add(group);
        GroupsUpdated?.Invoke(this, EventArgs.Empty);
    }
    
    public List<GroupModel> GetGroups()
    {
        return _groups;
    }
    
    public GroupModel? GetById(int id)
    {
        return _groups.FirstOrDefault(c => c.Id == id);
    }

    public void DeleteGroup(GroupModel group)
    {
        group.UnlinkAssociations();
        _groups.Remove(group);
        GroupsUpdated?.Invoke(this, EventArgs.Empty);
    }
}
