using MauiApp1.ViewModels;

namespace MauiApp1.View;

public class BaseView : ContentView
{
    protected void SetBindingContext(BaseViewModel viewModel)
    {
        BindingContext = viewModel;
    }
}