using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Groups;


namespace MauiApp1.ViewModels.Groups;

public class GroupsViewModel : BaseViewModel
{
    private readonly IGroupService _groupService;
    
    private readonly NoteItemFactoryManager _factoryManager;
    
    private readonly NoteToGroupSelectorButtonViewModel _groupSelectorButtonViewModel;
    
    private readonly IModalService _modalService;
    
    private ObservableCollection<GroupModel> _groups = new();
    
    public AsyncRelayCommand<GroupModel> GroupSelectedCommand { get; }

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
    
    
    public GroupsViewModel(IGroupService groupService, IModalService modalService,  NoteItemFactoryManager factoryManager, NoteToGroupSelectorButtonViewModel noteToGroupSelectorButtonViewModel) : base()
    {
        _groupService = groupService;
        _factoryManager = factoryManager;
        _groupSelectorButtonViewModel = noteToGroupSelectorButtonViewModel;
        _modalService = modalService;
        
        _groupService.GroupsUpdated += OnGroupsUpdated!;
        
        GroupSelectedCommand = new AsyncRelayCommand<GroupModel>(OnGroupSelected!);
        
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
        GroupItemStruct groupItemStruct = new GroupItemStruct
        {
            Group = group,
            ItemFactory = _factoryManager,
            NoteToGroupSelectorButtonViewModel = _groupSelectorButtonViewModel
        };
        
        
        await _modalService.ShowModalAsyncWithParameter<GroupItemView, GroupItemStruct>(groupItemStruct);
        
    }

    
}