using MauiApp1.ViewModels.Categories;

namespace MauiApp1.Interfaces;

public interface ICategorySelectable
{
    CategorySelectorViewModel CategorySelectorViewModel { get; }
}