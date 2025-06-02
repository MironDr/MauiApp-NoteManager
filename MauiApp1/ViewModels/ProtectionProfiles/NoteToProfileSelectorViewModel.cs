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
            _ = LoadNotes();
        }
    }
  

    public NoteToProfileSelectorViewModel(INoteService noteService, IModalService modalService, NoteItemFactoryManager factoryManager, IPopupService popupService) : base(noteService, modalService, factoryManager, popupService)
    {
        Console.WriteLine("ZALUPA");
    }
    
    protected override IEnumerable<NoteModel> FilterNotes(IEnumerable<NoteModel> notes)
    {
        return notes.Where(n => n.ProtectionProfile?.Id != Profile?.Id || n.ProtectionProfile == null);
    }

    protected override async Task OnNoteSelected(NoteModel note)
    {
        await _popupService.ClosePopupAsync();
        
        if (note.ProtectionProfile is { IsUnlocked: false })
        {
            bool result = await PasswordPopup(note.ProtectionProfile);
               
            if (!result)
                return;
        }
        
        note.ProtectionProfile = Profile;
        await _noteService.AddNote(note);
        await LoadNotes();
        OnEventInvoke?.Invoke();
    }

    public event Action? OnEventInvoke;
}