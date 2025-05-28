using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.ProtectionProfiles;

namespace MauiApp1.ViewModels.Notes;

public class ProfileToNoteSelectorViewModel : ProtectionProfilesViewModel, IEventHandler
{
    private readonly IPopupService _popupService;
    public TaskCompletionSource<ProtectionProfileModel?>? ClosePopupTcs { get; set; }
    
    public ProfileToNoteSelectorViewModel(IProtectionProfileService profileService, IModalService modalService, NoteToProfileSelectorButtonViewModel noteToProfileSelectorButtonViewModel, NoteToProfileSelectorViewModel noteToProfileSelectorViewModel, IPopupService popupService) : base(profileService, modalService, noteToProfileSelectorButtonViewModel, noteToProfileSelectorViewModel, popupService)
    {
        _popupService = popupService;
    }
    
    protected override async Task OnProfileSelected(ProtectionProfileModel profile)
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


    public event Action? OnEventInvoke;
}