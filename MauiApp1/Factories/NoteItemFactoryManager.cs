using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Factories;

public class NoteItemFactoryManager
{
    private readonly Dictionary<NoteType, INoteItemFactory> _factoryMap;

    public NoteItemFactoryManager(IEnumerable<INoteItemFactory> factories)
    {
        _factoryMap = factories.ToDictionary(f => f.NoteType);
    }

    public INoteItemFactory? GetFactory(NoteType type)
    {
        _factoryMap.TryGetValue(type, out var factory);
        return factory;
    }

    public INoteItemFactory? GetFactory(NoteModel model)
    {
        return GetFactory(model.Type);
    }
    
  
    public NoteItemStruct? Create(NoteModel model)
    {
        var factory = GetFactory(model.Type);
        return factory?.Create(model);
    }

    public BaseViewModel? GetEditorViewModel(NoteType type)
    {
        return GetFactory(type)?.GetEditorViewModel();
    }
}