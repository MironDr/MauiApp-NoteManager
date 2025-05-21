using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Factories;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public class NoteTypeSelectorViewModel : BaseViewModel
{
    
    public bool ShowSelectedInfo => false;
    
    private readonly IPopupService _popupService;
    private readonly IModalService _modalService;
    private readonly NoteItemFactoryManager _factoryManager;
    
    public ObservableCollection<NoteType> Types { get; }
    public AsyncRelayCommand<NoteType> SelectTypeCommand { get; }

    public NoteTypeSelectorViewModel(
        IPopupService popupService,
        IModalService modalService,
        NoteItemFactoryManager factoryManager)
    {
        _popupService = popupService;
        _modalService = modalService;
        _factoryManager = factoryManager;


        Types = new ObservableCollection<NoteType>(Enum.GetValues<NoteType>());
        SelectTypeCommand = new AsyncRelayCommand<NoteType>(OnNoteTypeSelected);
    }

    private async Task OnNoteTypeSelected(NoteType selectedType)
    {
        
        
        var editorVm = _factoryManager.GetEditorViewModel(selectedType);
       
        if (editorVm != null)
        {
            await _popupService.ClosePopupAsync();
      
            await _modalService.ShowModalAsyncWithParameter<CreateNoteView, BaseViewModel>(editorVm);
           
        }
    }
}