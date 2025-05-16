using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Factories;

public class AccountNoteItemFactory: INoteItemFactory
{
    public NoteType NoteType => NoteType.Account;

    private readonly INoteService _noteService;
    private readonly IPopupService _popupService;
    private readonly ICategoryService _categoryService;
    private readonly CategorySelectorViewModel _categorySelector;

    public AccountNoteItemFactory(
        INoteService noteService,
        IPopupService popupService,
        ICategoryService categoryService,
        CategorySelectorViewModel categorySelector)
    {
        _noteService = noteService;
        _popupService = popupService;
        _categoryService = categoryService;
        _categorySelector = categorySelector;
    }

    public NoteItemStruct Create(NoteModel note)
    {
        var model = (AccountNoteModel)note;

        string? categoryName = model.Category is { } categoryId
            ? _categoryService.GetCategories().FirstOrDefault(c => c.Id == categoryId)?.CategoryName
            : null;

        var manager = GetEditorViewModel() as ManageAccountNoteViewModel;
        manager!.GoToEditMode(model);

        return new NoteItemStruct
        {
            NoteItemView = new AccountNoteItemViewModel(model, categoryName),
            NoteItemEdit = manager
        };
    }

    public BaseViewModel GetEditorViewModel()
    {
        return new ManageAccountNoteViewModel(_noteService, _popupService, _categorySelector);
    }
}