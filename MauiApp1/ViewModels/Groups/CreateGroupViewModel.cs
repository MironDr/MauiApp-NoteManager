using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels.Groups;

public class CreateGroupViewModel : BaseViewModel
{
    private readonly IGroupService _groupService;
    private readonly IPopupService _popupService;
    
    public GroupDto Group { get; } =  new ();
    
    public AsyncRelayCommand SaveGroupCommand { get; }
    
    public bool ClosePopup { get; set; } = true;

    public event Action<GroupModel> GroupSaved;
    
    public CreateGroupViewModel(IGroupService groupService, IPopupService popupService)
    {
        SaveGroupCommand = new AsyncRelayCommand(SaveGroup);
        _groupService = groupService;
        _popupService = popupService;

    }

    private async Task SaveGroup()
    {
        if (string.IsNullOrWhiteSpace(Group.GroupName))
        {
            return;
        }
        
        var allGroups = _groupService.GetGroups();
        bool duplicateTitle = allGroups.Any(g =>
            g.GroupName.Equals(Group.GroupName, StringComparison.OrdinalIgnoreCase));
            

        if (duplicateTitle)
        {
            await _popupService.ClosePopupAsync();
            await _popupService.AlertAsync("Error", "Group title must be unique", "Ok");
            return;
        }


        GroupModel group = GroupModel.CreateGroup(Group);
        await _groupService.AddGroup(group);
        GroupSaved?.Invoke(group);
        
        if (ClosePopup)
          await _popupService.ClosePopupAsync();
    }
}