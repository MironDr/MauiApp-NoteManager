using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.ViewModels.Notes.Managers;

namespace MauiApp1.Factories;

public class TextNoteItemFactory : INoteItemFactory
{
    public NoteType NoteType => NoteType.Text;

    private readonly INoteService _noteService;
    private readonly IModalService _modalService;
    private readonly ICategoryService _categoryService;
    private readonly CategorySelectorViewModel _categorySelector;
    private readonly TextBlocksViewModel _textBlocksViewModelEdit;
    private readonly TextBlocksViewModel _textBlocksViewModelView;

    public TextNoteItemFactory(
        INoteService noteService,
        IModalService modalService,
        ICategoryService categoryService,
        CategorySelectorViewModel categorySelector,
        TextBlocksViewModel textBlocksViewModelEdit,
        TextBlocksViewModel textBlocksViewModelView)
    {
        _noteService = noteService;
        _modalService = modalService;
        _categoryService = categoryService;
        _categorySelector = categorySelector;
        _textBlocksViewModelEdit = textBlocksViewModelEdit;
        _textBlocksViewModelView = textBlocksViewModelView;
    }

    public NoteItemStruct Create(NoteModel note)
    {
        var model = (TextNoteModel)note;

        string? categoryName = model.Category is { } categoryId
            ? _categoryService.GetCategories().FirstOrDefault(c => c.Id == categoryId)?.CategoryName
            : null;

        var manager = GetEditorViewModel() as ManageTextNoteViewModel;
        manager!.GoToEditMode(model);

        _textBlocksViewModelView.SetReadOnly();
        
        return new NoteItemStruct
        {
            NoteItemView = new TextNoteItemViewModel(model, categoryName, _textBlocksViewModelView),
            NoteItemEdit = manager
        };
    }

    public BaseViewModel GetEditorViewModel()
    {
        return new ManageTextNoteViewModel(_noteService, _modalService, _categorySelector, _textBlocksViewModelEdit);
    }
}