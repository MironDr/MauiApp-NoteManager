using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using MauiApp1.Views.ProtectionProfiles;

namespace MauiApp1.ViewModels.ProtectionProfiles;

public class CreateProtectionProfileButtonViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    
    public IAsyncRelayCommand CreateProfileCommand { get; }
    
    public CreateProtectionProfileButtonViewModel(IPopupService popupService)
    {
        _popupService = popupService;
        CreateProfileCommand = new AsyncRelayCommand(CreateProfile);
    }
    
    private async Task CreateProfile()
    {
       await _popupService.ShowPopupAsync<CreateProtectionProfileView>(true);
    }
}