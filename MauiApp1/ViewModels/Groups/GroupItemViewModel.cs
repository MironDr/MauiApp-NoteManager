using System.Collections.ObjectModel;
using MauiApp1.Structs;

namespace MauiApp1.ViewModels.Groups;

public class GroupItemViewModel : BaseViewModel
{
    public string GroupName => _struct.Group.GroupName;
    public ObservableCollection<NoteItemStruct> Notes { get; } = new();
    public NoteToGroupSelectorButtonViewModel SelectorButtonViewModel { get; private set; }

    private GroupItemStruct _struct;

    public GroupItemViewModel(GroupItemStruct data)
    {
        _struct = data;
        SelectorButtonViewModel = data.NoteToGroupSelectorButtonViewModel;

       
        Notes.Clear();
        foreach (var note in data.Group.GetNotes().OrderBy(n => n.Id))
        {
            Notes.Add((NoteItemStruct)data.ItemFactory.Create(note)!);
        }

        OnPropertyChanged(nameof(GroupName));
        OnPropertyChanged(nameof(SelectorButtonViewModel));
    }
}