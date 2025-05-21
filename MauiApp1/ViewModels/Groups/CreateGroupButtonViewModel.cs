using System.Windows.Input;
using MauiApp1.Services;
using MauiApp1.Views.Groups;

namespace MauiApp1.ViewModels.Groups;

public class CreateGroupButtonViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;
    
    public ICommand CreateGroupCommand { get; }
    
    public CreateGroupButtonViewModel(IPopupService popupService)
    {
        _popupService = popupService;
        CreateGroupCommand = new Command(CreateGroup);
    }
    
    private void CreateGroup()
    {
        _popupService.ShowPopupAsync<CreateGroupView>();
    }
}