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
    private readonly IPopupService _popupService;
    private readonly IServiceProvider _serviceProvider;


    public CheckListNoteItemFactory(
        INoteService noteService,
        IModalService modalService,
        IServiceProvider serviceProvider, IPopupService popupService)
    {
        _noteService = noteService;
        _modalService = modalService;
        _serviceProvider = serviceProvider;
        _popupService = popupService;

    }

    public NoteItemStruct Create(NoteModel note)
    {
        var model = (CheckListNoteModel)note;

       

        var manager = GetEditorViewModel() as ManageCheckListNoteViewModel;
        manager!.GoToEditMode(model);

        CheckListViewModel checkListViewModel = _serviceProvider.GetRequiredService<CheckListViewModel>();
        checkListViewModel.SetReadOnly();
        
        return new NoteItemStruct
        {
            NoteItemView = new CheckListNoteItemViewModel(model, checkListViewModel),
            NoteItemEdit = manager
        };
    }

    public BaseViewModel GetEditorViewModel()
    {
        CheckListViewModel checkListViewModel = _serviceProvider.GetRequiredService<CheckListViewModel>();
        ClassifierSelectorViewModel classifierSelectorViewModel = _serviceProvider.GetRequiredService<ClassifierSelectorViewModel>();
        ProfileToNoteSelectorButtonViewModel profileToNoteSelectorButtonViewModel = _serviceProvider.GetRequiredService<ProfileToNoteSelectorButtonViewModel>();
        return new ManageCheckListNoteViewModel(_noteService, _modalService, _popupService, classifierSelectorViewModel, checkListViewModel, profileToNoteSelectorButtonViewModel);
    }
}