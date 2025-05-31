using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Notes;

public sealed partial class CustomQuotesContainerView : BaseView
{

    
    
    public CustomQuotesContainerView(CustomQuotesContainerViewModel mainVm)
    {
        InitializeComponent();
        SetBindingContext(mainVm);
        CreateButton.SetBindingContext(mainVm.CreateButtonViewModel);
        DeleteButton.SetBindingContext(mainVm.DeleteButtonViewModel);
    }
}