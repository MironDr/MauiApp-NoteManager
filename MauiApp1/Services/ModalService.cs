using MauiApp1.Interfaces;
using MauiApp1.Pages;
using MauiApp1.View;

namespace MauiApp1.Services;

public interface IModalService
{
    Task ShowModalAsync<TView>() where TView : BaseView;
    Task ShowModalAsyncWithParameter<TView, TParameter>(TParameter parameter) where TView : BaseView;
    Task CloseModalAsync();
    void AddViewToModal(BaseView view, bool switchMode = false);
    
    BaseView? GetViewFromModal();
}

public class ModalService : IModalService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Stack<ContentPage?> _modalPages = new();
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

        _modalPages.Push(new ModalContainerPage(view));
        

        await Shell.Current.Navigation.PushModalAsync(_modalPages.Peek());
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
       

        _modalPages.Push(new ModalContainerPage(view));

        await Shell.Current.Navigation.PushModalAsync(_modalPages.Peek());
        
        _isBusy = false;
    }

    public async Task CloseModalAsync()
    {
        if (_modalPages.Count == 0)
            return;

        await Shell.Current.Navigation.PopModalAsync();
        
        var modal = _modalPages.Pop() as ModalContainerPage;

        modal?.Close();
    }

    public void AddViewToModal(BaseView view, bool switchMode = false)
    {
        if (_modalPages.Peek()?.Content is IViewAddable addableView)
        {
            
            addableView.AddView(view);
        }
    }

    public BaseView? GetViewFromModal()
    {
        return _modalPages.Peek()?.Content as BaseView;
    }
}