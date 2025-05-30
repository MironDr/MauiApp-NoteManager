using System.Collections.ObjectModel;
using MauiApp1.Models;


namespace MauiApp1.ViewModels.Groups;

public class GroupItemViewModel : BaseViewModel
{
    public ObservableCollection<NoteModel> Notes { get; } = new();

    public GroupModel Group { get; }

    public GroupItemViewModel(GroupModel group)
    {
        Group = group;

        foreach (var note in group.GetNotes().OrderBy(n => n.Id))
            Notes.Add(note);
    }
    
    public void SortNotesByMainNote()
    {
        var main = Group.GetMainNote();
    
        var sortedNotes = Notes
            .OrderByDescending(n => n.Id == main?.Id)
            .ThenBy(n => n.Id)
            .ToList();

        for (int i = 0; i < sortedNotes.Count; i++)
        {
            var currentIndex = Notes.IndexOf(sortedNotes[i]);
            if (currentIndex != i)
                Notes.Move(currentIndex, i);
        }
    }
}