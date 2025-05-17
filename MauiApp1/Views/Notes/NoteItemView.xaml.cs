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
    private readonly SubViewFactory _subViewFactory;
    

    
    public NoteItemView()
    {
        InitializeComponent();
        _subViewFactory = App.Services.GetRequiredService<SubViewFactory>();
        
    }
   

    public void SetData(NoteItemStruct data)
    {
        BindingContext = data.NoteItemView;
        EditModeView.SetBindingContext(data.NoteItemEdit);
        EditModeView.SetDataToEdit(data.NoteItemView.Note);

    
        
        
        var viewType = data.NoteItemView.Note.Type;
        var subView = _subViewFactory.GetViewForType(viewType, data.NoteItemEdit);

        if (subView != null && EditModeView is IViewAddable addable)
        {
            addable.AddView(subView);
        }
    }

    private void Switch_OnToggled(object? sender, ToggledEventArgs e)
    {
        ReadModeView.IsVisible = !ReadModeView.IsVisible;
        EditModeView.IsVisible = !ReadModeView.IsVisible;
        
    }

    public void AddView(BaseView view)
    {
       
        DynamicContentArea.Add(view);
    }
}