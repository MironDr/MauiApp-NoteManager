using CommunityToolkit.Maui.Views;
using MauiApp1.Interfaces;
using MauiApp1.Popups;
using MauiApp1.View;

namespace MauiApp1.Services;

public interface IPopupService
{
    Task ShowPopupAsync<TView>(bool canBeClosed) where TView : BaseView;
    Task ShowPopupAsyncWithParameter<TView, TParameter>(TParameter parameter,bool canBeClosed) where TView : BaseView;
    Task ClosePopupAsync();
    void AddViewToPopup(BaseView view);
    
    Task<T?> ShowResultPopupAsync<TView, T>()
        where TView : BaseView, IResultView<T>;
    
    Task<T?> ShowResultPopupAsyncWithParameter<TView, TParameter, T>(TParameter parameter)
        where TView : BaseView, IResultView<T>, IParameterizedView<TParameter>;

    Task AlertAsync(string title, string message, string cancel = "OK");


    Task<bool> AlertConfirmAsync(string title, string message);

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

    public void AddViewToPopup(BaseView view)
    {
     
        if (_popup?.Content is IViewAddable addableView)
        {
            addableView.AddView(view);
        }
    }
    
    public async Task<T?> ShowResultPopupAsync<TView, T>()
        where TView : BaseView, IResultView<T>
    {
        if (_isBusy)
            return default;

        _isBusy = true;
        var view = _serviceProvider.GetRequiredService<TView>();

        var popup = new InstantPopup { Content = view, CanBeDismissedByTappingOutsideOfPopup = false };
        _popup = popup;

        Shell.Current.CurrentPage.ShowPopup(popup); 
        var result = await view.WaitForResultAsync();

        _isBusy = false;
        await ClosePopupAsync();
        return result;
    }
    
    public async Task<T?> ShowResultPopupAsyncWithParameter<TView, TParameter, T>(
        TParameter parameter)
        where TView : BaseView, IResultView<T>, IParameterizedView<TParameter>
    {
        if (_isBusy)
            return default;

        _isBusy = true;
        var view = _serviceProvider.GetRequiredService<TView>();

        view.SetData(parameter);

        var popup = new InstantPopup { Content = view, CanBeDismissedByTappingOutsideOfPopup = false};
        _popup = popup;

        Shell.Current.CurrentPage.ShowPopup(popup);
        var result = await view.WaitForResultAsync();

        _isBusy = false;
        await ClosePopupAsync();
        return result;
    }
    
    
    public async Task AlertAsync(string title, string message, string cancel = "OK")
    {
        await Shell.Current.CurrentPage.DisplayAlert(title, message, cancel);
    }
    
    public async Task<bool> AlertConfirmAsync(string title, string message)
    {
       return await Shell.Current.CurrentPage.DisplayAlert(title, message, "Yes","No");
    }
}
