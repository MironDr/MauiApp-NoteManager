using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.ViewModels.ProtectionProfiles;
using MauiApp1.Views.ElementsViews;
using MauiApp1.Views.ProtectionProfiles;

namespace MauiApp1.Pages;

public partial class ProfilesPage : BasePage
{
    private readonly IServiceProvider _serviceProvider;

    public ProfilesPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        
        
        VerticalProtectionProfilesView.SetBindingContext(_serviceProvider.GetRequiredService<ProtectionProfilesViewModel>());
        
        BottomContainer.Content = _serviceProvider.GetRequiredService<CustomBottomBarView>();
       
    }
}