using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.View;
using MauiApp1.ViewModels.Groups;
using MauiApp1.Views.Notes;

namespace MauiApp1.Views.Groups;

public partial class GroupItemView : BaseView, IParameterizedView<GroupItemStruct>
{
    private GroupItemViewModel _viewModel;
    private NoteItemFactoryManager _factoryManager;
    private NoteToGroupSelectorButtonViewModel _selectorVm;

    public GroupItemView()
    {
        InitializeComponent();
    }

    public void SetData(GroupItemStruct data)
    {
        _factoryManager = data.ItemFactory;
        _selectorVm = data.NoteToGroupSelectorButtonViewModel;

        _viewModel = new GroupItemViewModel(data.Group);
        BindingContext = _viewModel;

        _selectorVm.SelectGroup(_viewModel.Group);
        _selectorVm.ViewModel.OnEventInvoke += Reload;

        SelectorButtonView.BindingContext = _selectorVm;

        NotesCollectionView.ItemTemplate = new DataTemplate(CreateNoteViewTemplate);
    }

    private Microsoft.Maui.Controls.View CreateNoteViewTemplate()
    {
        var border = new Border
        {
            Padding = 10,
            Margin = new Thickness(0, 5)
        };

        var noteView = new NoteItemView();
        
        
        noteView.BindingContextChanged += (s, e) =>
        {
            if (noteView.BindingContext is NoteModel note)
            {
                var nis = _factoryManager.Create(note);
                if (nis == null) return;

                var noteStruct = nis.Value;
                noteView.SetData(noteStruct);

                if (noteStruct.NoteItemView is IEventHandler handlerView)
                {
                    handlerView.OnEventInvoke -= FullReload;
                    handlerView.OnEventInvoke += FullReload;
                }
                
                if (noteStruct.NoteItemEdit is IEventHandler handlerEdit)
                {
                    handlerEdit.OnEventInvoke -= MinorReload;
                    handlerEdit.OnEventInvoke += MinorReload;
                }

            }
        };

        border.Content = noteView;
        return border;
    }

    private void Reload()
    {
        _viewModel.Notes.Clear();
        foreach (var note in _viewModel.Group.GetNotes().OrderBy(n => n.Id))
        {
            _viewModel.Notes.Add(note);
        }
    }
    private void MinorReload()
    {
        foreach (var note in _viewModel.Notes.ToList())
        {
            if (note.Group == null || note.Group.Id != _viewModel.Group.Id)
            {
                _viewModel.Notes.Remove(note);
            }
        }
    }
    private void FullReload()
    {
        NotesCollectionView.ItemTemplate = null;
        NotesCollectionView.ItemTemplate = new DataTemplate(CreateNoteViewTemplate);
        
       Reload();
    }
    
}