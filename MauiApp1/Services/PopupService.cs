using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using MauiApp1.View;

namespace MauiApp1.Utilities;

public interface IPopupService
{
    Task ShowPopupAsync<TView>() where TView : BaseView;
    Task ClosePopupAsync();
}


public class PopupService : IPopupService
{
    private readonly IServiceProvider _serviceProvider;
    
    private Popup? _popup;
    
    public PopupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }

    public async Task ShowPopupAsync<TView>() where TView : BaseView
    {
        var view = _serviceProvider.GetRequiredService<TView>();
        
        
        _popup = new Popup { Content = view };
        
        await Shell.Current.CurrentPage.ShowPopupAsync(_popup);
    }

    public async Task ClosePopupAsync()
    {
        if(_popup == null)
            return;

        await _popup.CloseAsync();
    }
    
}