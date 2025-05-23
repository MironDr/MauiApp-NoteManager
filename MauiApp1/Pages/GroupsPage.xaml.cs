using MauiApp1.ViewModels.Groups;
using MauiApp1.Views.Groups;

namespace MauiApp1.Pages;

public partial class GroupsPage : BasePage
{
    private readonly IServiceProvider _serviceProvider;

    public GroupsPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        
        
        VerticalGroupsView.SetBindingContext(_serviceProvider.GetRequiredService<GroupsViewModel>());
        
        CreateGroupButtonView.SetBindingContext(_serviceProvider.GetRequiredService<CreateGroupButtonViewModel>());
        
       
    }
}