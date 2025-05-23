using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.ProtectionProfiles;

public class NoteToProfileSelectorButtonViewModel : NoteSelectorButtonViewModel<NoteToProfileSelectorViewModel>
{
    public NoteToProfileSelectorButtonViewModel(IPopupService popupService, NoteToProfileSelectorViewModel viewModel) : base(popupService, viewModel)
    {
    }
    
    public void SelectProfile(ProtectionProfileModel profile)
    {
        ViewModel.Profile = profile;
    }

    public void SetViewModel(NoteToProfileSelectorViewModel viewModel)
    {
        ViewModel = viewModel;
    }
 
    protected override void OpenSelector()
    {
        _popupService.ShowPopupAsyncWithParameter<VerticalNotesView, NoteToProfileSelectorViewModel>(ViewModel);
    }

}