using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Groups;

namespace MauiApp1.Structs;

public class GroupItemStruct
{
    public GroupModel Group { get; set; }
    
    public NoteItemFactoryManager ItemFactory { get; set; }
    
    public NoteToGroupSelectorButtonViewModel NoteToGroupSelectorButtonViewModel { get; set; }
}