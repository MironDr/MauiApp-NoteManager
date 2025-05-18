using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Notes;
using MauiApp1.ViewModels.Notes.Managers;

namespace MauiApp1.Views.Notes;

public partial class TextBlocksView : BaseView, INoteSubView
{
    public NoteType Type => NoteType.Text;
    
    public TextBlocksView()
    {
        InitializeComponent();
    }

    public TextBlocksView(TextBlocksViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    public BaseView GetView(BaseViewModel parentViewModel)
    {
        if (parentViewModel is ManageTextNoteViewModel vm)
        {
            var view = this;
            view.SetBindingContext(vm.TextBlocksViewModel);
            return view;
        }

        throw new ArgumentException("Incorrect type ViewModel");
    }

   
}