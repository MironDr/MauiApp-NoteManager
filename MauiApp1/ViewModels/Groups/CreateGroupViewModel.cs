using System.Windows.Input;
using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels.Groups;

public class CreateGroupViewModel : BaseViewModel
{
    private readonly IGroupService _groupService;
    private readonly IPopupService _popupService;
    
    public GroupDto Group { get; } =  new ();
    
    public ICommand SaveGroupCommand { get; }
    
    public bool ClosePopup { get; set; } = true;

    public event Action<GroupModel> GroupSaved;
    
    public CreateGroupViewModel(IGroupService groupService, IPopupService popupService)
    {
        SaveGroupCommand = new Command(SaveGroup);
        _groupService = groupService;
        _popupService = popupService;

    }

    private void SaveGroup()
    {
        if (string.IsNullOrWhiteSpace(Group.GroupName))
        {
            return;
        }

        GroupModel group = GroupModel.CreateGroup(Group);
        _groupService.AddGroup(group);
        GroupSaved?.Invoke(group);
        
        if (ClosePopup)
            _popupService.ClosePopupAsync();
    }
}