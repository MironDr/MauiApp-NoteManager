using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Views.Notes;

public partial class CustomEditorView : ContentView
{
    public CustomEditorView()
    {
        InitializeComponent();
    }

    private void OnEditorCompleted(object? sender, EventArgs e)
    {
        EditorField.Unfocus();  
    }

    private void OnEditorTapped(object? sender, TappedEventArgs e)
    {
        if (EditorField.IsReadOnly)
        {
            Clipboard.SetTextAsync(EditorField.Text);
        }
    }
}