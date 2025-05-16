using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels;

namespace MauiApp1.Interfaces;

public interface INoteSubView
{
    BaseView GetView(BaseViewModel parentViewModel);
    
    NoteType Type { get; }
}