using MauiApp1.Interfaces;
using MauiApp1.View;

namespace MauiApp1.Services;

public interface IModalService
{
    Task ShowModalAsync<TView>() where TView : BaseView;
    Task ShowModalAsyncWithParameter<TView, TParameter>(TParameter parameter) where TView : BaseView;
    Task CloseModalAsync();
    void AddViewToModal(BaseView view, bool switchMode = false);
}

public class ModalService : IModalService
{
    private readonly IServiceProvider _serviceProvider;
    private ContentPage? _modalPage;
    private bool _isBusy = false;

    public ModalService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ShowModalAsync<TView>() where TView : BaseView
    {
        if (_isBusy)
            return;

        _isBusy = true;

        var view = _serviceProvider.GetRequiredService<TView>();

        _modalPage = new ContentPage
        {
            Content = view
        };

        await Shell.Current.Navigation.PushModalAsync(_modalPage);
        _isBusy = false;
    }

    public async Task ShowModalAsyncWithParameter<TView, TParameter>(TParameter parameter) where TView : BaseView
    {
        if (_isBusy)
            return;

        _isBusy = true;

        var view = _serviceProvider.GetRequiredService<TView>();

        if (view is IParameterizedView<TParameter> parameterizedView)
        {
            parameterizedView.SetData(parameter);
        }

        _modalPage = new ContentPage
        {
            Content = view
        };

        await Shell.Current.Navigation.PushModalAsync(_modalPage);
        _isBusy = false;
    }

    public async Task CloseModalAsync()
    {
        if (_modalPage == null)
            return;

        await Shell.Current.Navigation.PopModalAsync();
        _modalPage = null;
    }

    public void AddViewToModal(BaseView view, bool switchMode = false)
    {
        if (_modalPage?.Content is IViewAddable addableView)
        {
            
            addableView.AddView(view);
        }
    }
}