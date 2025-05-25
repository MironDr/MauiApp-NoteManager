using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.View;
using MauiApp1.ViewModels.ProtectionProfiles;

namespace MauiApp1.Views.ProtectionProfiles;

public partial class ProtectionProfileItemView : BaseView, IParameterizedView<ProtectionProfileItemViewModel>, IClosedEvent
{
    public ProtectionProfileItemView()
    {
        InitializeComponent();
    }

    public void SetData(ProtectionProfileItemViewModel data)
    {
        BindingContext = data;
        SelectorButtonView.SetBindingContext(data.NoteToProfileSelectorButtonViewModel);
    }
    
    public void OnClosed()
    {
        if (BindingContext is ProtectionProfileItemViewModel vm)
        {
            vm.LockProfile();
        }
    }
}