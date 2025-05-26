using MauiApp1.ViewModels.Groups;
using MauiApp1.Views.ElementsViews;
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
        
        BottomContainer.Content = _serviceProvider.GetRequiredService<CustomBottomBarView>();
        
       
    }
}