using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.ProtectionProfiles;
using MauiApp1.Views;

namespace MauiApp1.ViewModels.Notes;

public class ProfileToNoteSelectorViewModel : BaseViewModel, IEventHandler
{
    private readonly IPopupService _popupService;
    private readonly IProtectionProfileService _protectionProfileService;
    public TaskCompletionSource<ProtectionProfileModel?>? ClosePopupTcs { get; set; }
    
    public IAsyncRelayCommand<ProtectionProfileModel> ProfileSelectedCommand { get; }
    
    private ObservableCollection<ProtectionProfileModel> _profiles = new();
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
    
    public ProfileToNoteSelectorViewModel(IPopupService popupService, IProtectionProfileService protectionProfileService)
    {
        ProfileSelectedCommand = new AsyncRelayCommand<ProtectionProfileModel>(OnProfileSelected!);
        _popupService = popupService;
        _protectionProfileService = protectionProfileService;
        Profiles = new ObservableCollection<ProtectionProfileModel>(_protectionProfileService.GetProfiles());
    }
    
    protected async Task OnProfileSelected(ProtectionProfileModel profile)
    {
        await _popupService.ClosePopupAsync();
        bool result =  await Validate(profile);

        if (!result)
        {
            ClosePopupTcs?.TrySetResult(null);
            return;
        }



        ClosePopupTcs?.TrySetResult(profile);
    }

    private async Task<bool> Validate(ProtectionProfileModel profile)
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

    public event Action? OnEventInvoke;
}