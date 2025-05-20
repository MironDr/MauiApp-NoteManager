using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.View;
using MauiApp1.ViewModels;

namespace MauiApp1.Views.Notes;

public sealed partial class CheckListView : BaseView
{
    public CheckListView(BaseViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
    }
}