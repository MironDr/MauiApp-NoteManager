using MauiApp1.DTOs;
using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;


namespace MauiApp1.Views.Notes;

public partial class NoteItemView : BaseView, IParameterizedView<NoteItemStruct>, IViewAddable, IClosedEvent
{
    private NoteModel _model;
    private IUpdatable _updatable;

    public NoteItemView()
    {
        InitializeComponent();
    }

    public void SetData(NoteItemStruct data)
    {
        _model = data.NoteItemView.Note;
        _updatable = data.NoteItemView;

        BindingContext = data.NoteItemView;

        if (data.NoteItemEdit is IEventHandler eventHandler)
        {
            eventHandler.OnEventInvoke += Switch;
            eventHandler.OnEventInvoke += _updatable.Update;
        }

        EditModeView.SetBindingContext(data.NoteItemEdit);
        EditModeView.GoToEditMode(_model);

        if (data.NoteItemView is ICompositeViewModel compositeViewModel)
        {
            DynamicContentArea.Clear();
            foreach (var view in compositeViewModel.GetEmbeddedViews())
                AddView(view);
        }

        UpdateVisibilityStates();
    }

    private void Switch_OnToggled(object? sender, ToggledEventArgs e)
    {
        UpdateVisibilityStates();
    }

    private void Switch()
    {
        SwitchView.IsToggled = !SwitchView.IsToggled;
    }

    private void UpdateVisibilityStates()
    {
        ReadModeView.IsVisible = !SwitchView.IsToggled;
        EditModeView.IsVisible = SwitchView.IsToggled;

        if (EditModeView.IsVisible)
            EditModeView.GoToEditMode(_model);
    }

    public void AddView(BaseView view)
    {
        DynamicContentArea.Add(view);
    }


    public void OnClosed()
    {
        _model.ProtectionProfile?.Lock();
    }
}