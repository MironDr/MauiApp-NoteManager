using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;

namespace MauiApp1.Views.Notes;

public sealed partial class CreateNoteWithSourceButtonView : BaseView
{
    public CreateNoteWithSourceButtonView(CreateNoteWithSourceButtonViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
       
    }
    public CreateNoteWithSourceButtonView()
    {
        InitializeComponent();
        
    }
}