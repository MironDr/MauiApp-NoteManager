using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    public CategoriesViewModel CategoriesViewModel { get; }
    public NotesViewModel NotesViewModel { get; }

    public MainPageViewModel(CategoriesViewModel categoriesViewModel, NotesViewModel notesViewModel)
    {
        CategoriesViewModel = categoriesViewModel;
        NotesViewModel = notesViewModel;

        CategoriesViewModel.CategorySelected += (s, category) =>
        {
            NotesViewModel.FilterByCategory(category);
        };
    }
}