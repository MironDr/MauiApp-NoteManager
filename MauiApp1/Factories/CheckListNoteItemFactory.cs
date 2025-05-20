using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.ViewModels.Notes.Items;
using MauiApp1.ViewModels.Notes.Managers;

namespace MauiApp1.Factories;

public class CheckListNoteItemFactory : INoteItemFactory
{
    public NoteType NoteType => NoteType.CheckList;

    private readonly INoteService _noteService;
    private readonly IModalService _modalService;
    private readonly ICategoryService _categoryService;
    private readonly IServiceProvider _serviceProvider;

    public CheckListNoteItemFactory(
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
        var model = (CheckListNoteModel)note;

        string? categoryName = model.Category is { } categoryId
            ? _categoryService.GetById(categoryId)?.CategoryName
            : null;

        var manager = GetEditorViewModel() as ManageCheckListNoteViewModel;
        manager!.GoToEditMode(model);

        CheckListViewModel checkListViewModel = _serviceProvider.GetRequiredService<CheckListViewModel>();
        checkListViewModel.SetReadOnly();
        
        return new NoteItemStruct
        {
            NoteItemView = new CheckListNoteItemViewModel(model, categoryName, checkListViewModel),
            NoteItemEdit = manager
        };
    }

    public BaseViewModel GetEditorViewModel()
    {
        CheckListViewModel checkListViewModel = _serviceProvider.GetRequiredService<CheckListViewModel>();
        CategorySelectorViewModel categorySelectorViewModel = _serviceProvider.GetRequiredService<CategorySelectorViewModel>();
        
        return new ManageCheckListNoteViewModel(_noteService, _modalService, categorySelectorViewModel, checkListViewModel);
    }
}