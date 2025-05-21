using MauiApp1.Views.Groups;

namespace MauiApp1.Pages;

public partial class GroupsPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public GroupsPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        
        var verticalGroupsList = _serviceProvider.GetRequiredService<VerticalGroupsView>();
        
        Layout.Add(verticalGroupsList);
       
    }
}