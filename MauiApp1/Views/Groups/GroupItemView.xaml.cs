using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Structs;
using MauiApp1.View;
using MauiApp1.ViewModels.Groups;
using MauiApp1.Views.Notes;
using Microsoft.Maui.Controls.Shapes;

namespace MauiApp1.Views.Groups;

public partial class GroupItemView : BaseView, IParameterizedView<GroupItemStruct>, IClosedEvent
{
    private GroupItemViewModel _viewModel;
    private NoteItemFactoryManager _factoryManager;
    private NoteToGroupSelectorButtonViewModel _selectorVm;
    private readonly Dictionary<NoteModel, Border> _noteBorders = new();
    private readonly Color _color;
    public GroupItemView()
    {
        InitializeComponent();
        if (Application.Current.Resources.TryGetValue("Primary", out var colorValue) && colorValue is Color color)
        {
            _color = color;
        }
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
        Reload();
    }

    private Microsoft.Maui.Controls.View CreateNoteViewTemplate()
    {
        
        
        var border = new Border
        {
            Padding = 10,
            Margin = new Thickness(0, 5),
            StrokeThickness = 1,
            Stroke = _color,
            BackgroundColor = Colors.Transparent,
            StrokeShape = new RoundRectangle { CornerRadius = 6 }
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

                _noteBorders[note] = border;

               
                UpdateNoteBorder(note, border);
            }
        };

        border.Content = noteView;
        return border;
    }

    private void Reload()
    {
        foreach (var note in _viewModel.Group.GetNotes())
        {
            if(!_viewModel.Notes.Contains(note))
                _viewModel.Notes.Add(note);
            
        }

        _viewModel.SortNotesByMainNote();
    }
    private void MinorReload()
    {
        _viewModel.SortNotesByMainNote();
        
        foreach (var note in _viewModel.Notes.ToList())
        {
            if (note.Group == null || note.Group.Id != _viewModel.Group.Id)
            {
                _viewModel.Notes.Remove(note);
            }
        }
        
        foreach (var kvp in _noteBorders)
        {
            var note = kvp.Key;
            var border = kvp.Value;

            UpdateNoteBorder(note, border);
        }
    }

    private void UpdateNoteBorder(NoteModel note, Border border)
    {
        var mainNote = _viewModel.Group.GetMainNote();
        bool isMain = note.Id == mainNote?.Id;

        border.Stroke = isMain ? Colors.Gold : _color;
        border.StrokeThickness = isMain ? 3 : 1;
    }
    
    private void FullReload()
    {
        NotesCollectionView.ItemTemplate = null; 
        NotesCollectionView.ItemTemplate = new DataTemplate(CreateNoteViewTemplate);
        
       Reload();
    }

    public void OnClosed()
    {
        if (BindingContext is GroupItemViewModel vm)
        {
            foreach (var note in vm.Group.GetNotes())
            {
                note.ProtectionProfile?.Lock();
            }
        }
    }
}