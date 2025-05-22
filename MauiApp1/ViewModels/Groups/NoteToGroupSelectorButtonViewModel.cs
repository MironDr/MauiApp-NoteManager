using System.Windows.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Groups;

public class NoteToGroupSelectorButtonViewModel : BaseView
{
    private readonly IPopupService _popupService;
    public NoteToGroupSelectorViewModel NoteToGroupSelectorViewModel  {get;}

    public ICommand OpenSelectorCommand { get; }

    public NoteToGroupSelectorButtonViewModel(IPopupService popupService, NoteToGroupSelectorViewModel noteToGroupSelectorViewModel)
    {
        NoteToGroupSelectorViewModel = noteToGroupSelectorViewModel;
        _popupService = popupService;
        OpenSelectorCommand = new Command(OpenSelector);
    }

    public void SelectGroup(GroupModel group)
    {
        NoteToGroupSelectorViewModel.Group = group;
    }

 
    private void OpenSelector()
    {
        _popupService.ShowPopupAsyncWithParameter<VerticalNotesView, NoteToGroupSelectorViewModel>(NoteToGroupSelectorViewModel);
    }
    
}