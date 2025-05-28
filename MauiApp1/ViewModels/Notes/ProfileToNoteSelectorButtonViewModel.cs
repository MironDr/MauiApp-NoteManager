using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.ProtectionProfiles;
using MauiApp1.Views.Notes;
using MauiApp1.Views.ProtectionProfiles;

namespace MauiApp1.ViewModels.Notes;

public class ProfileToNoteSelectorButtonViewModel: NoteSelectorButtonViewModel<ProfileToNoteSelectorViewModel>
{
    private TaskCompletionSource<ProtectionProfileModel?>? _tcs;
    
    public ProfileToNoteSelectorButtonViewModel(IPopupService popupService, ProfileToNoteSelectorViewModel viewModel) : base(popupService, viewModel)
    {
    }
    
    protected override void OpenSelector()
    {
        _popupService.ShowPopupAsyncWithParameter<VerticalProtectionProfilesView, ProfileToNoteSelectorViewModel>(ViewModel, false);
    }

    public async Task<ProtectionProfileModel?> Open()
    {
        if (ViewModel.Profiles.Count == 0)
        {
            await _popupService.AlertAsync("Error", "You have no profiles yet.", "Ok");
            return null;
        }

        _tcs = new TaskCompletionSource<ProtectionProfileModel?>();
        ViewModel.ClosePopupTcs = _tcs;
        OpenSelector();
        return await _tcs.Task;
    }

}