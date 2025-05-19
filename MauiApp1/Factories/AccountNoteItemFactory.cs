using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.ViewModels.Notes.Managers;

namespace MauiApp1.Factories;

public class AccountNoteItemFactory: INoteItemFactory
{
    public NoteType NoteType => NoteType.Account;

    private readonly INoteService _noteService;
    private readonly IModalService _modalService;
    private readonly ICategoryService _categoryService;
    private readonly IServiceProvider _serviceProvider;

    public AccountNoteItemFactory(
        INoteService noteService,
        IModalService modalService,
        ICategoryService categoryService,
        IServiceProvider serviceProvider)
    {
        _noteService = noteService;
        _modalService = modalService;
        _categoryService = categoryService;
        _serviceProvider = serviceProvider;
    }

    public NoteItemStruct Create(NoteModel note)
    {
        var model = (AccountNoteModel)note;

        string? categoryName = model.Category is { } categoryId
            ? _categoryService.GetById(categoryId)?.CategoryName
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
        CategorySelectorViewModel categorySelectorViewModel = _serviceProvider.GetRequiredService<CategorySelectorViewModel>();
        return new ManageAccountNoteViewModel(_noteService, _modalService, categorySelectorViewModel);
    }
}