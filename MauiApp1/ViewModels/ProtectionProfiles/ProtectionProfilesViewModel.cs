using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.Views;
using MauiApp1.Views.Groups;
using MauiApp1.Views.ProtectionProfiles;

namespace MauiApp1.ViewModels.ProtectionProfiles;

public class ProtectionProfilesViewModel : BaseViewModel
{
    private readonly IProtectionProfileService _profileService;
    private readonly INoteService _noteService;
    private readonly NoteToProfileSelectorButtonViewModel _noteToProfileSelectorButtonViewModel;
    private readonly NoteToProfileSelectorViewModel _noteToProfileSelectorViewModel;
    
    private readonly IModalService _modalService;
    private readonly IPopupService _popupService;
    
    private ObservableCollection<ProtectionProfileModel> _profiles = new();
    
    public IAsyncRelayCommand<ProtectionProfileModel> ProfileSelectedCommand { get; }

    public IAsyncRelayCommand<ProtectionProfileModel> DeleteProfileCommand { get; }
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
        NoteToProfileSelectorButtonViewModel noteToProfileSelectorButtonViewModel, NoteToProfileSelectorViewModel noteToProfileSelectorViewModel, IPopupService popupService, INoteService noteService) : base()
    {
        _profileService = profileService;
        _modalService = modalService;
        _noteToProfileSelectorButtonViewModel = noteToProfileSelectorButtonViewModel;
        _noteToProfileSelectorViewModel = noteToProfileSelectorViewModel;
        _popupService = popupService;
        _noteService = noteService;
        _profileService.ProfilesUpdated += OnProfilesUpdated!;
        
        ProfileSelectedCommand = new AsyncRelayCommand<ProtectionProfileModel>(OnProfileSelected!);
        
        DeleteProfileCommand = new AsyncRelayCommand<ProtectionProfileModel>(OnProfileDeleted!);
        
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
    
    protected virtual async Task OnProfileSelected(ProtectionProfileModel profile)
    {
        bool result =  await Validate(profile);

        if(!result)
            return;
        
        var viewModel = new ProtectionProfileItemViewModel(profile, _noteToProfileSelectorButtonViewModel, _noteToProfileSelectorViewModel, _popupService, _noteService);
        
        await _modalService.ShowModalAsyncWithParameter<ProtectionProfileItemView, ProtectionProfileItemViewModel>(viewModel);
    }

    protected async Task<bool> Validate(ProtectionProfileModel profile)
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
    
    private async Task OnProfileDeleted(ProtectionProfileModel profile)
    {
        bool answer = await _popupService.AlertConfirmAsync(
            "Warning",          
            "Are you sure you want to delete this protection profile?"
        );
        
        if(!answer)
            return;

        if (profile.GetNotes().Count != 0)
        {
            await _popupService.AlertAsync("Error", "You cannot delete non empty profile", "Ok");
            return;
        }
        
        
        if (profile is { IsUnlocked: false })
        {
            bool result =  await Validate(profile);

            if(!result)
                return;
           
        }
        
        await _profileService.DeleteProfile(profile);
    }

}