using MauiApp1.Structs;
using MauiApp1.View;

namespace MauiApp1.Interfaces;

public interface ICompositeViewModel
{
    IEnumerable<BaseView> GetEmbeddedViews();
}