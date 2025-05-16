using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;

namespace MauiApp1.Factories;

public class SubViewFactory
{
    private readonly Dictionary<NoteType, INoteSubView> _views;

    public SubViewFactory(IEnumerable<INoteSubView> subViews)
    {
        _views = subViews.ToDictionary(v => v.Type, v => v);
        
    }

    public BaseView? GetViewForType(NoteType type, BaseViewModel parentViewModel)
    {
        return _views.TryGetValue(type, out var view)
            ? view.GetView(parentViewModel)
            : null;
    }
}