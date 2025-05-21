using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Interfaces;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Notes;

public partial class CustomTypeSelectorView : BaseView, IParameterizedView<NoteTypeSelectorViewModel>
{
    public CustomTypeSelectorView()
    {
        InitializeComponent();
    }
    
    public CustomTypeSelectorView(BaseViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    public void SetData(NoteTypeSelectorViewModel data)
    {
        BindingContext = data;
    }
}