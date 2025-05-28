using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Structs;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.ViewModels.Notes.Managers;

namespace MauiApp1.Factories;

public class SourceNoteItemFactory : INoteItemFactory
{
    public NoteType NoteType => NoteType.Source;

    private readonly INoteService _noteService;
    private readonly IModalService _modalService;
    private readonly IPopupService _popupService;
    private readonly IServiceProvider _serviceProvider;


    public SourceNoteItemFactory(
        INoteService noteService,
        IModalService modalService,
        IPopupService popupService,
        IServiceProvider serviceProvider)
    {
        _noteService = noteService;
        _modalService = modalService;
        _popupService  = popupService;
        _serviceProvider = serviceProvider;

    }

    public NoteItemStruct Create(NoteModel note)
    {
        var model = (SourceNoteModel)note;
        
        var manager = GetEditorViewModel() as ManageSourceNoteViewModel;
        manager!.GoToEditMode(model);

        CustomQuotesContainerViewModel customQuotesContainerViewModel = _serviceProvider.GetRequiredService<CustomQuotesContainerViewModel>();
        
        return new NoteItemStruct
        {
            NoteItemView = new SourceNoteItemViewModel(model, customQuotesContainerViewModel),
            NoteItemEdit = manager
        };
    }

    public BaseViewModel GetEditorViewModel()
    {
        ClassifierSelectorViewModel classifierSelectorViewModel = _serviceProvider.GetRequiredService<ClassifierSelectorViewModel>();
        ProfileToNoteSelectorButtonViewModel profileToNoteSelectorButtonViewModel = _serviceProvider.GetRequiredService<ProfileToNoteSelectorButtonViewModel>();
        return new ManageSourceNoteViewModel(_noteService, _modalService, _popupService, classifierSelectorViewModel, profileToNoteSelectorButtonViewModel);
    }
}