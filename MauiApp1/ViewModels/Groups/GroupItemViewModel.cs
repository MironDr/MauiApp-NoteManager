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
}