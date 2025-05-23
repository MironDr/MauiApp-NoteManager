using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.View;
using MauiApp1.Views.Notes;

namespace MauiApp1.Views.Groups;

public partial class GroupItemView : BaseView, IParameterizedView<GroupItemStruct>
{
    private GroupModel _groupModel;
    private NoteItemFactoryManager _factoryManager;
    
    public GroupItemView() 
    {
        InitializeComponent();
    }
    
    public void SetData(GroupItemStruct data)
    {
        _groupModel = data.Group;
        _factoryManager = data.ItemFactory;
        BindingContext = _groupModel;
        data.NoteToGroupSelectorButtonViewModel.SelectGroup(_groupModel);
        data.NoteToGroupSelectorButtonViewModel.ViewModel.OnEventInvoke += Reload;
        SelectorButtonView.BindingContext = data.NoteToGroupSelectorButtonViewModel;
        
        Reload();
    }

    private void Reload()
    {
        MainStack.Children.Clear();

        foreach (var note in _groupModel.GetNotes().OrderBy(n => n.Id))
        {
            var nis = _factoryManager.Create(note);
            if (nis == null)
                continue;

            var noteStruct = nis.Value;

            if (noteStruct.NoteItemView is IEventHandler handlerView)
            {
                handlerView.OnEventInvoke -= Reload;
                handlerView.OnEventInvoke += Reload;
            }

            if (noteStruct.NoteItemEdit is IEventHandler handlerEdit)
            {
                handlerEdit.OnEventInvoke -= Reload;
                handlerEdit.OnEventInvoke += Reload;
            }

            var noteView = new NoteItemView();
            noteView.SetData(noteStruct);

            var border = new Border
            {
                Padding = 10,
                Margin = new Thickness(0, 5),
                Content = noteView
            };

            MainStack.Children.Add(border);
        }
    }

    
}