using MauiApp1.Interfaces;
using MauiApp1.View;

namespace MauiApp1.Pages;

public class ModalContainerPage : BasePage
{
    public event Action? Closed;

    public ModalContainerPage(BaseView contentView)
    {
        Content = contentView;

        if (contentView is IClosedEvent closeable)
        {
            Closed += closeable.OnClosed;
        }
    }
    

    protected override bool OnBackButtonPressed()
    {
        Close();
        return base.OnBackButtonPressed(); 
    }

    public void Close()
    {
        Closed?.Invoke();
    }
}