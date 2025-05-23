using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.Views.Groups;
using MauiApp1.Views.ProtectionProfiles;

namespace MauiApp1.ViewModels.ProtectionProfiles;

public class ProtectionProfilesViewModel : BaseViewModel
{
    private readonly IProtectionProfileService _profileService;

    private readonly NoteToProfileSelectorButtonViewModel _noteToProfileSelectorButtonViewModel;
    private readonly NoteToProfileSelectorViewModel _noteToProfileSelectorViewModel;
    
    private readonly IModalService _modalService;
    
    private ObservableCollection<ProtectionProfileModel> _profiles = new();
    
    public IAsyncRelayCommand<ProtectionProfileModel> ProfileSelectedCommand { get; }

    public ObservableCollection<ProtectionProfileModel> Profiles
    {
        get => _profiles;
        set
        {
            if (_profiles != value)
            {
                _profiles = value;
                OnPropertyChanged(nameof(Profiles));
            }
        }
    }
    
    
    public ProtectionProfilesViewModel(IProtectionProfileService profileService, IModalService modalService, 
        NoteToProfileSelectorButtonViewModel noteToProfileSelectorButtonViewModel, NoteToProfileSelectorViewModel noteToProfileSelectorViewModel) : base()
    {
        _profileService = profileService;
        _modalService = modalService;
        _noteToProfileSelectorButtonViewModel = noteToProfileSelectorButtonViewModel;
        _noteToProfileSelectorViewModel = noteToProfileSelectorViewModel;
        _profileService.ProfilesUpdated += OnProfilesUpdated!;
        
        ProfileSelectedCommand = new AsyncRelayCommand<ProtectionProfileModel>(OnProfileSelected!);
        
        LoadProfiles();
    }
    
    private void OnProfilesUpdated(object sender, EventArgs e)
    {
        LoadProfiles();
    }
    
    private void LoadProfiles()
    {
        Profiles = new ObservableCollection<ProtectionProfileModel>(_profileService.GetProfiles());
    }
    
    private async Task OnProfileSelected(ProtectionProfileModel profile)
    {
        var viewModel = new ProtectionProfileItemViewModel(profile, _noteToProfileSelectorButtonViewModel, _noteToProfileSelectorViewModel);
        
        await _modalService.ShowModalAsyncWithParameter<ProtectionProfileItemView, ProtectionProfileItemViewModel>(viewModel);
    }

}