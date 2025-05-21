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
    private readonly IServiceProvider _serviceProvider;

    public TextNoteItemFactory(
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
        var model = (TextNoteModel)note;
        

        var manager = GetEditorViewModel() as ManageTextNoteViewModel;
        manager!.GoToEditMode(model);

        TextBlocksViewModel textBlocksViewModelForView = _serviceProvider.GetRequiredService<TextBlocksViewModel>();
        CustomQuotesContainerViewModel customQuotesContainerViewModel = _serviceProvider.GetRequiredService<CustomQuotesContainerViewModel>();
        textBlocksViewModelForView.SetReadOnly();
        
        return new NoteItemStruct
        {
            NoteItemView = new TextNoteItemViewModel(model, textBlocksViewModelForView, customQuotesContainerViewModel),
            NoteItemEdit = manager
        };
    }

    public BaseViewModel GetEditorViewModel()
    {
        TextBlocksViewModel textBlocksViewModelForEdit = _serviceProvider.GetRequiredService<TextBlocksViewModel>();
        ClassifierSelectorViewModel classifierSelectorViewModel = _serviceProvider.GetRequiredService<ClassifierSelectorViewModel>();
        
        return new ManageTextNoteViewModel(_noteService, _modalService, classifierSelectorViewModel, textBlocksViewModelForEdit);
    }
}