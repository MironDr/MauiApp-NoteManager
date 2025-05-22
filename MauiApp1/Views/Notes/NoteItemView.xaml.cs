using MauiApp1.DTOs;
using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;


namespace MauiApp1.Views.Notes;

public partial class NoteItemView : BaseView, IParameterizedView<NoteItemStruct>, IViewAddable
{

    private NoteModel _model;
    
    private IUpdatable _updatable;
    public NoteItemView()
    {
        InitializeComponent();
        
    }
   

    public void SetData(NoteItemStruct data)
    {
        
        BindingContext = data.NoteItemView;
        _model = data.NoteItemView.Note;
        _updatable = data.NoteItemView;

        if (data.NoteItemEdit is IEventHandler eventHandler)
        {
            eventHandler.OnEventInvoke += Switch;
            eventHandler.OnEventInvoke += _updatable.Update;
        }


        EditModeView.SetBindingContext(data.NoteItemEdit);
        EditModeView.SetDataToEdit(_model);
        
      
        
       
        
        
        if (data.NoteItemView is ICompositeViewModel compositeViewModel)
        {
            foreach (var view in compositeViewModel.GetEmbeddedViews())
                AddView(view);
        }
        
     
    }

    private void Switch_OnToggled(object? sender, ToggledEventArgs e)
    {
        ReadModeView.IsVisible = !ReadModeView.IsVisible;
        
        if(!ReadModeView.IsVisible)
            EditModeView.SetDataToEdit(_model);
        
        EditModeView.IsVisible = !ReadModeView.IsVisible;
    }

    private void Switch()
    {
        SwitchView.IsToggled = !SwitchView.IsToggled;
    }
    
    
    
    public void AddView(BaseView view)
    {
       
        DynamicContentArea.Add(view);
    }
}