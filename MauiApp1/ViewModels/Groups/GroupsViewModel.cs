using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views;
using MauiApp1.Views.Groups;


namespace MauiApp1.ViewModels.Groups;

public class GroupsViewModel : BaseViewModel
{
    private readonly IGroupService _groupService;
    
    private readonly NoteItemFactoryManager _factoryManager;
    
    private readonly NoteToGroupSelectorButtonViewModel _groupSelectorButtonViewModel;
    
    private readonly IModalService _modalService;
    private readonly IPopupService _popupService;
    
    
    private ObservableCollection<GroupModel> _groups = new();
    
    public AsyncRelayCommand<GroupModel> GroupSelectedCommand { get; }
    public AsyncRelayCommand<GroupModel> DeleteGroupCommand { get; }

    public ObservableCollection<GroupModel> Groups
    {
        get => _groups;
        set
        {
            if (_groups != value)
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }
    }
    
    
    public GroupsViewModel(IGroupService groupService, IModalService modalService,  NoteItemFactoryManager factoryManager, NoteToGroupSelectorButtonViewModel noteToGroupSelectorButtonViewModel, IPopupService popupService) : base()
    {
        _groupService = groupService;
        _factoryManager = factoryManager;
        _groupSelectorButtonViewModel = noteToGroupSelectorButtonViewModel;
        _popupService = popupService;
        _modalService = modalService;
        
        _groupService.GroupsUpdated += OnGroupsUpdated!;
        
        GroupSelectedCommand = new AsyncRelayCommand<GroupModel>(OnGroupSelected!);
        DeleteGroupCommand = new AsyncRelayCommand<GroupModel>(OnGroupDeleted!);
        LoadGroups();
    }
    
    private void OnGroupsUpdated(object sender, EventArgs e)
    {
        LoadGroups();
    }
    
    private void LoadGroups()
    {
        Groups = new ObservableCollection<GroupModel>(_groupService.GetGroups());
    }
    
    private async Task OnGroupSelected(GroupModel group)
    {
        foreach (var note in group.GetNotes())
        {
            if (note.ProtectionProfile is { IsUnlocked: false })
            {
               bool result = await PasswordPopup(note.ProtectionProfile);
               
               if (!result)
                   return;
            }

        }
        
        
        GroupItemStruct groupItemStruct = new GroupItemStruct
        {
            Group = group,
            ItemFactory = _factoryManager,
            NoteToGroupSelectorButtonViewModel = _groupSelectorButtonViewModel
        };
        
        await _modalService.ShowModalAsyncWithParameter<GroupItemView, GroupItemStruct>(groupItemStruct);
        
    }
    
    private async Task OnGroupDeleted(GroupModel group)
    {
        bool answer = await _popupService.AlertConfirmAsync(
            "Warning",          
            "Are you sure you want to delete the group?"
        );
        
        if(!answer)
            return;

        if (group.GetNotes().Count > 0)
        {
            await _popupService.AlertAsync("Error", "There are notes inside this group, to remove them, assign them to another group or category", "Ok");
            return;
        }
        
        _groupService.DeleteGroup(group);
    }
    
    protected async Task<bool> PasswordPopup(ProtectionProfileModel profile)
    {
        var password = await _popupService.ShowResultPopupAsyncWithParameter<PasswordPopupView,string, string?>(profile.ProfileName);

        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (!profile.TryUnlock(password))
        {
            await _popupService.AlertAsync("Error", "Incorrect password", "Ok");
            return false;
        }
        return true;
    }

    
}