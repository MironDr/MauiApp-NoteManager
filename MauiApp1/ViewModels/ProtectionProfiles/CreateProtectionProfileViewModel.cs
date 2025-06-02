using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels.ProtectionProfiles;

public class CreateProtectionProfileViewModel : BaseViewModel
{
    private readonly IProtectionProfileService _profileService;
    private readonly IPopupService _popupService;
    public ProtectionProfileDto Profile { get; } =  new ();
    
    public AsyncRelayCommand SaveProfileCommand { get; }
    
    public bool ClosePopup { get; set; } = true;

    public event Action<ProtectionProfileModel>? ProfileSaved;
    
    public CreateProtectionProfileViewModel(IProtectionProfileService profileService, IPopupService popupService)
    {
        SaveProfileCommand = new AsyncRelayCommand(SaveProfile);
        _profileService = profileService;
        _popupService = popupService;

    }

    private async Task SaveProfile()
    {
        if (string.IsNullOrWhiteSpace(Profile.ProfileName))
        {
            return;
        }

        if (Profile.Password != Profile.PasswordRepeat)
        {
            return;
        }

        ProtectionProfileModel profile = ProtectionProfileModel.CreateProfile(Profile);
        await _profileService.AddProfile(profile);
        ProfileSaved?.Invoke(profile);
        
        if (ClosePopup)
            await _popupService.ClosePopupAsync();
    }
}