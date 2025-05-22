using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public void SetGroup(GroupModel group)
    {
        _groupModel = group;
    }

    public void SetData(GroupItemStruct data)
    {
        _groupModel = data.Group;
        _factoryManager = data.ItemFactory;
        BindingContext = _groupModel;
        data.NoteToGroupSelectorButtonViewModel.SelectGroup(_groupModel);
        data.NoteToGroupSelectorButtonViewModel.NoteToGroupSelectorViewModel.OnEventInvoke += Reload;
        SelectorButtonView.BindingContext = data.NoteToGroupSelectorButtonViewModel;
        
        Reload();
    }

    private void Reload()
    {
        MainStack.Children.Clear();
        
        List<NoteItemStruct> _notes = new();
        
        foreach (var note in _groupModel.GetNotes().OrderBy(g => g.Id).ToList())
        {
            NoteItemStruct nis = (NoteItemStruct)_factoryManager.Create(note)!;
            _notes.Add(nis);
            if (nis.NoteItemView is IEventHandler eventHandlerView)
            {
                eventHandlerView.OnEventInvoke += Reload;
            }
            if (nis.NoteItemEdit is IEventHandler eventHandlerEdit)
            {
                eventHandlerEdit.OnEventInvoke += Reload;
            }
        }
        
        foreach (var noteStruct in _notes)
        {
            if(noteStruct.NoteItemView.Note.Group == null)
                return;
            
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