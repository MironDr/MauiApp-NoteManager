using System.Windows.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Groups;

public class NoteToGroupSelectorButtonViewModel : BaseView, IEventHandler
{
    private readonly IPopupService _popupService;
    private readonly NoteToGroupSelectorViewModel _viewModel;

    public ICommand OpenSelectorCommand { get; }

    public NoteToGroupSelectorButtonViewModel(IPopupService popupService, NoteToGroupSelectorViewModel viewModel)
    {
        _viewModel = viewModel;
        _popupService = popupService;
        viewModel.OnEventInvoke += Update;
        OpenSelectorCommand = new Command(OpenSelector);
    }

    public void SelectGroup(GroupModel group)
    {
        _viewModel.Group = group;
    }

    private void Update()
    {
        OnEventInvoke?.Invoke();
    }
    
    private void OpenSelector()
    {
        _popupService.ShowPopupAsyncWithParameter<VerticalNotesView, NoteToGroupSelectorViewModel>(_viewModel);
    }


    public event Action? OnEventInvoke;
}