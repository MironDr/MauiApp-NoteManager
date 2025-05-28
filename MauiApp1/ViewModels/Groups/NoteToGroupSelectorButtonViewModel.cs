using System.Windows.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Groups;

public class NoteToGroupSelectorButtonViewModel : NoteSelectorButtonViewModel<NoteToGroupSelectorViewModel>
{
    public NoteToGroupSelectorButtonViewModel(IPopupService popupService, NoteToGroupSelectorViewModel viewModel) : base(popupService, viewModel)
    {
    }

    public void SelectGroup(GroupModel group)
    {
        ViewModel.Group = group;
    }

 
    protected override void OpenSelector()
    {
        _popupService.ShowPopupAsyncWithParameter<VerticalNotesView, NoteToGroupSelectorViewModel>(ViewModel, true);
    }
    
}