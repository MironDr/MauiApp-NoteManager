using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public class NoteTypeSelectorViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    private readonly IModalService _modalService;
    private readonly NoteItemFactoryManager _factoryManager;
    private readonly SubViewFactory _factorySubView;

    public ObservableCollection<NoteType> NoteTypes { get; }
    public AsyncRelayCommand<NoteType> SelectNoteTypeCommand { get; }

    public NoteTypeSelectorViewModel(
        IPopupService popupService,
        IModalService modalService,
        NoteItemFactoryManager factoryManager,
        SubViewFactory factorySubView)
    {
        _popupService = popupService;
        _modalService = modalService;
        _factoryManager = factoryManager;
        _factorySubView = factorySubView;

        NoteTypes = new ObservableCollection<NoteType>(Enum.GetValues<NoteType>());
        SelectNoteTypeCommand = new AsyncRelayCommand<NoteType>(OnNoteTypeSelected);
    }

    private async Task OnNoteTypeSelected(NoteType selectedType)
    {
        var editorVm = _factoryManager.GetEditorViewModel(selectedType);
        if (editorVm != null)
        {
            await _popupService.ClosePopupAsync();
            await _modalService.ShowModalAsyncWithParameter<CreateNoteView, BaseViewModel>(editorVm);

            var view = _factorySubView.GetViewForType(selectedType, editorVm);
            
            if (view != null)
            {
                _modalService.AddViewToModal(view);
            }
        }
    }
}