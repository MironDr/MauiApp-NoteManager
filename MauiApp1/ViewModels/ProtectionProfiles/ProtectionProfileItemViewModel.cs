using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Groups;

namespace MauiApp1.ViewModels.ProtectionProfiles;

public class ProtectionProfileItemViewModel : BaseViewModel
{
    private readonly ProtectionProfileModel _profile;
    private readonly IPopupService _popupService;
    private readonly INoteService _noteService;
    public NoteToProfileSelectorButtonViewModel NoteToProfileSelectorButtonViewModel { get; }

    public NoteToProfileSelectorViewModel NoteToProfileSelectorViewModel { get; }

    public IAsyncRelayCommand<NoteModel> DeleteNoteCommand { get; }

    public ProtectionProfileItemViewModel(ProtectionProfileModel profile, NoteToProfileSelectorButtonViewModel noteToProfileSelectorButtonViewModel, NoteToProfileSelectorViewModel noteToProfileSelectorViewModel, IPopupService popupService, INoteService noteService)
    {
        _profile = profile;
        NoteToProfileSelectorButtonViewModel = noteToProfileSelectorButtonViewModel;
        NoteToProfileSelectorViewModel = noteToProfileSelectorViewModel;
        _popupService = popupService;
        _noteService = noteService;
        NoteToProfileSelectorViewModel.Profile = _profile;
        NoteToProfileSelectorButtonViewModel.SetViewModel(NoteToProfileSelectorViewModel);
        NoteToProfileSelectorViewModel.OnEventInvoke += Reload;
        DeleteNoteCommand = new AsyncRelayCommand<NoteModel>(DeleteNote!);
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

    private async Task DeleteNote(NoteModel note)
    {
        bool answer = await _popupService.AlertConfirmAsync(
            "Warning",          
            "Are you sure you want to delete this note from this profile?"
        );
        
        if(!answer)
            return;

        if (!_profile.IsUnlocked)
            throw new Exception("You do not have permission to delete this profile.");
        
        note.ProtectionProfile = null;
        await _noteService.AddNote(note);
        Reload();
        
    }
    
    
}