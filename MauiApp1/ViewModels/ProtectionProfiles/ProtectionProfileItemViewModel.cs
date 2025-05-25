using System.Collections.ObjectModel;
using MauiApp1.Models;
using MauiApp1.ViewModels.Groups;

namespace MauiApp1.ViewModels.ProtectionProfiles;

public class ProtectionProfileItemViewModel : BaseViewModel
{
    private readonly ProtectionProfileModel _profile;

    public NoteToProfileSelectorButtonViewModel NoteToProfileSelectorButtonViewModel { get; }

    public NoteToProfileSelectorViewModel NoteToProfileSelectorViewModel { get; }
    public ProtectionProfileItemViewModel(ProtectionProfileModel profile, NoteToProfileSelectorButtonViewModel noteToProfileSelectorButtonViewModel, NoteToProfileSelectorViewModel noteToProfileSelectorViewModel)
    {
        _profile = profile;
        NoteToProfileSelectorButtonViewModel = noteToProfileSelectorButtonViewModel;
        NoteToProfileSelectorViewModel = noteToProfileSelectorViewModel;
        NoteToProfileSelectorViewModel.Profile = _profile;
        NoteToProfileSelectorButtonViewModel.SetViewModel(NoteToProfileSelectorViewModel);
        NoteToProfileSelectorViewModel.OnEventInvoke += Reload;
    }
    
    public string Name => _profile.ProfileName;

    public List<NoteModel> NotesList => _profile.GetNotes().Values.ToList();


    private void Reload()
    {
        OnPropertyChanged(nameof(NotesList));
    }

    public void LockProfile()
    {
        _profile.Lock();
    }
}