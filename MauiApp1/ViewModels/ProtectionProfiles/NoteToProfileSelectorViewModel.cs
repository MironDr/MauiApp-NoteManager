using System.Collections.ObjectModel;
using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.ViewModels.ProtectionProfiles;

public class NoteToProfileSelectorViewModel : NotesViewModel, IEventHandler
{
    private ProtectionProfileModel? _profile;

    public ProtectionProfileModel? Profile
    {
        get => _profile;
        set
        {
            if (_profile?.Id == value?.Id)
                return;
            
            _profile = value;
            LoadNotes();
        }
    }
    private readonly IPopupService _popupService;

    public NoteToProfileSelectorViewModel(INoteService noteService, IModalService modalService, NoteItemFactoryManager factoryManager, IPopupService popupService) : base(noteService, modalService, factoryManager)
    {
        _popupService = popupService;
    }

    protected override void LoadNotes()
    {
        Notes = new ObservableCollection<NoteModel>(
            _noteService.GetNotes().Where(n => n.ProtectionProfile?.Id != Profile?.Id || n.ProtectionProfile == null)
        );
    }

    protected override async Task OnNoteSelected(NoteModel note)
    {
        note.ProtectionProfile = Profile;
        LoadNotes();
        OnEventInvoke?.Invoke();
        await _popupService.ClosePopupAsync();
    }

    public event Action? OnEventInvoke;
}