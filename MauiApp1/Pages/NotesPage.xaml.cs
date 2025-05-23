using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Notes;

namespace MauiApp1.Pages;

public partial class NotesPage : BasePage
{
    private readonly IServiceProvider _serviceProvider;

    public NotesPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        
        
        VerticalNotesView.SetBindingContext(_serviceProvider.GetRequiredService<NotesViewModel>());
        
        CreateNoteButtonView.SetBindingContext(_serviceProvider.GetRequiredService<CreateNoteButtonViewModel>());
        
    }
}