using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.ViewModels;

public class CategoriesPageViewModel : BaseViewModel
{
    public CategoriesViewModel CategoriesViewModel { get; }
    public NotesViewModel NotesViewModel { get; }

    public CategoriesPageViewModel(CategoriesViewModel categoriesViewModel, NotesViewModel notesViewModel)
    {
        CategoriesViewModel = categoriesViewModel;
        NotesViewModel = notesViewModel;
        NotesViewModel.ListType = ListType.Category;
        
        
        CategoriesViewModel.CategorySelected += (s, category) =>
        {
            NotesViewModel.FilterByCategory(category);
        };
    }
}