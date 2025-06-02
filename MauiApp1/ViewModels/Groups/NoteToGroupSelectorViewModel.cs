using System.Collections.ObjectModel;
using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.ViewModels.Groups;

public class NoteToGroupSelectorViewModel : NotesViewModel, IEventHandler
{
    private GroupModel? _group;

    public GroupModel? Group
    {
        get => _group;
        set
        {
            if (_group?.Id == value?.Id)
                return;
            
            _group = value;
            _ = LoadNotes();
        }
    }
    

    public NoteToGroupSelectorViewModel(INoteService noteService, IModalService modalService, NoteItemFactoryManager factoryManager, IPopupService popupService) : base(noteService, modalService, factoryManager, popupService)
    {
        Console.WriteLine("HUI");
    }
    
    protected override IEnumerable<NoteModel> FilterNotes(IEnumerable<NoteModel> notes)
    {
        return notes.Where(n => n.Group?.Id != Group?.Id || n.Group == null);
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
        
        note.Category = null;
        note.Group = Group;
        await _noteService.AddNote(note);
        await LoadNotes();
        OnEventInvoke?.Invoke();
        
    }

    public event Action? OnEventInvoke;
}