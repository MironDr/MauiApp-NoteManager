using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;

namespace MauiApp1.Views.Notes;

public partial class NoteView : BaseView, IParameterizedView<NoteModel>
{
    public NoteView()
    {
        InitializeComponent();
        
    }

    public void SetData(NoteModel data)
    {
        BindingContext = data;
    }
}