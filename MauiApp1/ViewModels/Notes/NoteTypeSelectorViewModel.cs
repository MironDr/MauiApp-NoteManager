using System.Collections.ObjectModel;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public class NoteTypeSelectorViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    private readonly NoteItemFactoryManager _factoryManager;
    private readonly SubViewFactory _factorySubView;

    public ObservableCollection<NoteType> NoteTypes { get; }
    public Command<NoteType> SelectNoteTypeCommand { get; }

    public NoteTypeSelectorViewModel(IPopupService popupService, NoteItemFactoryManager factoryManager, SubViewFactory factorySubView)
    {
        _popupService = popupService;
        _factoryManager = factoryManager;
        _factorySubView = factorySubView;
     

        NoteTypes = new ObservableCollection<NoteType>(Enum.GetValues<NoteType>());
        SelectNoteTypeCommand = new Command<NoteType>(OnNoteTypeSelected);
    }

    private void OnNoteTypeSelected(NoteType selectedType)
    {
        var editorVm = _factoryManager.GetEditorViewModel(selectedType);
        if (editorVm != null)
        {
            _popupService.ClosePopupAsync(); 
            _popupService.ShowPopupAsyncWithParameter<CreateNoteView, BaseViewModel>(editorVm);
            var view = _factorySubView.GetViewForType(selectedType, editorVm);
            if (view != null) _popupService.AddViewToPopup(view);
        }
    }
}