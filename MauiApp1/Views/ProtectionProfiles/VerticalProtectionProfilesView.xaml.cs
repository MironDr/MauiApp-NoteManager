using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Interfaces;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.ProtectionProfiles;

namespace MauiApp1.Views.ProtectionProfiles;

public sealed partial class VerticalProtectionProfilesView : BaseView, IParameterizedView<BaseViewModel>
{
    public VerticalProtectionProfilesView()
    {
        InitializeComponent();
    }

    public void SetData(BaseViewModel data)
    {
        BindingContext = data;
    }
}