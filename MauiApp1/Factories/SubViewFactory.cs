using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.Views.Notes;

namespace MauiApp1.Factories;

public class SubViewFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<NoteType, Type> _viewTypes;

    public SubViewFactory(IEnumerable<INoteSubView> subViews, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        
        _viewTypes = subViews
            .ToDictionary(v => v.Type, v => v.GetType());
        
    }

    public BaseView? GetViewForType(NoteType type, BaseViewModel parentViewModel)
    {
        if (_viewTypes.TryGetValue(type, out var viewType))
        {
            if (_serviceProvider.GetService(viewType) is INoteSubView subView)
            {
                return subView.GetView(parentViewModel);
            }
        }

        return null;
    }
}