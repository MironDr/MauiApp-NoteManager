using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.View;
using MauiApp1.ViewModels.Groups;

namespace MauiApp1.Views.Groups;

public sealed partial class VerticalGroupsView : BaseView
{
    public VerticalGroupsView(GroupsViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
    }
}