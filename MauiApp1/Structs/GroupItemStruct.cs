using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.ViewModels;

namespace MauiApp1.Structs;

public class GroupItemStruct
{
    public GroupModel Group { get; set; }
    
    public NoteItemFactoryManager ItemFactory { get; set; }
}