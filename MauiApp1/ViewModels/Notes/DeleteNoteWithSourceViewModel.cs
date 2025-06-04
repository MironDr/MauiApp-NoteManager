using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels.Notes;

public class DeleteNoteWithSourceViewModel : BaseViewModel, IEventHandler
{
    private readonly IPopupService _popupService;
    public ObservableCollection<NoteWithSourceModel> Quotes { get; set; } = new();
    
    public ICommand DeleteQuoteCommand { get; }

    public DeleteNoteWithSourceViewModel(IPopupService popupService)
    {
        _popupService = popupService;

        DeleteQuoteCommand = new Command<NoteWithSourceModel>(DeleteNote!);
    }

    public void SetModel(NoteModel note)
    {
        if (note is TextNoteModel textNote)
        {
            Quotes = new ObservableCollection<NoteWithSourceModel>(textNote.GetNotesLinks());
            return;
        }

        if (note is SourceNoteModel sourceNote)
        {
            Quotes = new ObservableCollection<NoteWithSourceModel>(sourceNote.GetNotesLinks());
            return;
        }
    }

    private void DeleteNote(NoteWithSourceModel note)
    {
        note.Remove();
        _popupService.ClosePopupAsync();
        OnEventInvoke?.Invoke();
    }


    public event Action? OnEventInvoke;
}