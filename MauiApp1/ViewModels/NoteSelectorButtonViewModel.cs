using System.Windows.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels;

public class NoteSelectorButtonViewModel<T> : BaseViewModel where T : BaseViewModel, IEventHandler
{
    protected readonly IPopupService _popupService;
    public T ViewModel  {get; set; }

    public ICommand OpenSelectorCommand { get; }

    public NoteSelectorButtonViewModel(IPopupService popupService, T viewModel)
    {
        ViewModel = viewModel;
        _popupService = popupService;
        OpenSelectorCommand = new Command(OpenSelector);
    }
    

 
    protected virtual void OpenSelector()
    {
    }
}