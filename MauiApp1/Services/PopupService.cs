using CommunityToolkit.Maui.Views;
using MauiApp1.Interfaces;
using MauiApp1.Popups;
using MauiApp1.View;

namespace MauiApp1.Services;

public interface IPopupService
{
    Task ShowPopupAsync<TView>(bool canBeClosed = true) where TView : BaseView;
    Task ShowPopupAsyncWithParameter<TView, TParameter>(TParameter parameter,bool canBeClosed = true) where TView : BaseView;
    
    Task ClosePopupAsync();
}


public class PopupService : IPopupService
{
    private readonly IServiceProvider _serviceProvider;
    
    private Popup? _popup;
    
    private bool _isBusy = false;
    public PopupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }

    public async Task ShowPopupAsync<TView>(bool canBeClosed = true) where TView : BaseView
    {
        if(_isBusy)
            return;
        
        _isBusy = true;
        var view = _serviceProvider.GetRequiredService<TView>();
        
        
        _popup = new InstantPopup { Content = view, CanBeDismissedByTappingOutsideOfPopup = canBeClosed };
        
        await Shell.Current.CurrentPage.ShowPopupAsync(_popup);
        _isBusy = false;
    }
    
    public async Task ShowPopupAsyncWithParameter<TView, TParameter>(TParameter parameter, bool canBeClosed = true) where TView : BaseView
    {
        if(_isBusy)
            return;
        
        _isBusy = true;
        var view = _serviceProvider.GetRequiredService<TView>();
        
        if (view is IParameterizedView<TParameter> parameterizedView)
        {
            parameterizedView.SetData(parameter);
        }
        
        _popup = new InstantPopup { Content = view, CanBeDismissedByTappingOutsideOfPopup = canBeClosed };
        
        await Shell.Current.CurrentPage.ShowPopupAsync(_popup);
        _isBusy = false;
    }

    public async Task ClosePopupAsync()
    {
        if(_popup == null)
            return;

        await _popup.CloseAsync();
    }
    
    
}
