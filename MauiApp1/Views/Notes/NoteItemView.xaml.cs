using MauiApp1.DTOs;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes;


namespace MauiApp1.Views.Notes;

public partial class NoteItemView : BaseView, IParameterizedView<NoteItemStruct>
{
    public NoteItemView()
    {
        InitializeComponent();
        
    }
   

    public void SetData(NoteItemStruct data)
    {
        BindingContext = data.NoteItemView;
        EditModeView.SetBindingContext(data.NoteItemEdit);
        EditModeView.SetDataToEdit(data.NoteItemView.Note);
    }

    private void Switch_OnToggled(object? sender, ToggledEventArgs e)
    {
        ReadModeView.IsVisible = !ReadModeView.IsVisible;
        EditModeView.IsVisible = !ReadModeView.IsVisible;
    }
}