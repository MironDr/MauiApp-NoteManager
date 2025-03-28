using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.ViewModels;

namespace MauiApp1.View;

public partial class CreateCategoryView : BaseView
{
    public CreateCategoryView(CreateCategoryViewModel viewModel)
    {
        InitializeComponent();
        SetBindingContext(viewModel);
    }
}