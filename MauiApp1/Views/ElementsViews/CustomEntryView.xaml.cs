using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.View;

namespace MauiApp1.Views.Notes;

public partial class CustomEntryView : BaseView
{
    public CustomEntryView()
    {
        InitializeComponent();
    }
    
    private void OnEntryCompleted(object sender, EventArgs e)
    {
        EntryField.Unfocus();  
    }

    private void OnEntryTapped(object sender, EventArgs e)
    {
        if (EntryField.IsReadOnly)
        {
            Clipboard.SetTextAsync(EntryField.Text);
        }
    }
}