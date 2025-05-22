using System.Collections.ObjectModel;
using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels.Notes;

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
            LoadNotes();
        }
    }
    private readonly IPopupService _popupService;

    public NoteToGroupSelectorViewModel(INoteService noteService, IModalService modalService, NoteItemFactoryManager factoryManager, IPopupService popupService) : base(noteService, modalService, factoryManager)
    {
        _popupService = popupService;
    }

    protected override void LoadNotes()
    {
        Notes = new ObservableCollection<NoteModel>(
            _noteService.GetNotes().Where(n => n.Group?.Id != Group?.Id || n.Group == null)
        );
    }

    protected override async Task OnNoteSelected(NoteModel note)
    {
        note.Category = null;
        note.Group = Group;
        LoadNotes();
        OnEventInvoke?.Invoke();
        await _popupService.ClosePopupAsync();
    }

    public event Action? OnEventInvoke;
}