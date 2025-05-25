using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using MauiApp1.Interfaces;
using MauiApp1.View;

namespace MauiApp1.Views;

public partial class PasswordPopupView : BaseView, IResultView<string?>, IParameterizedView<string>
{
    private TaskCompletionSource<string?> _tcs = new();

    public PasswordPopupView()
    {
        InitializeComponent();
    }

    public Task<string?> WaitForResultAsync() => _tcs.Task;

    private void OnOkClicked(object sender, EventArgs e)
    {
        _tcs.TrySetResult(PasswordEntry.Text);
   
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        _tcs.TrySetResult(null);
    }


    public void SetData(string data)
    {
        ProfileLabel.Text = data;
    }
}