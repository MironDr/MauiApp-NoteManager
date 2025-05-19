using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;

namespace MauiApp1.ViewModels.Notes;

public class SpecificNoteSelectorViewModel : BaseViewModel
{
    public ICommand NoteSelectedCommand { get; }
    
    private ObservableCollection<NoteModel> _notes = new();
    private NoteModel? _selectedNote;
    private string _selectedNoteName = "Select a Note";

    private bool _isListVisible = false;

    public bool IsListVisible
    {
        get => _isListVisible;
        set
        {
            if (value != _isListVisible)
            {
                _isListVisible = value;
                OnPropertyChanged(nameof(IsListVisible));
            }
        }
    }

    public string SelectedNoteName
    {
        get => _selectedNoteName;
        set
        {
            if (_selectedNoteName != value)
            {
                _selectedNoteName = value;
                OnPropertyChanged(nameof(SelectedNoteName));
            }
        }
    } 
    public ObservableCollection<NoteModel> Notes
    {
        get => _notes;
        set
        {
            if (_notes != value)
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
    }

    public NoteModel? SelectedNote
    {
        get => _selectedNote;
        set
        {
            if (_selectedNote != value && value != null)
            {
                _selectedNote = value;
                SelectedNoteName = _selectedNote.Title;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }
    }
    
    public ICommand ToggleNoteListCommand { get; }

    public SpecificNoteSelectorViewModel(IEnumerable<NoteModel> notes, NoteModel? selectedNote = null)
    {
        SelectedNote = selectedNote;
        Notes = new ObservableCollection<NoteModel>(notes.Where(n => n.Id != SelectedNote?.Id));
        NoteSelectedCommand = new Command<NoteModel>(OnNoteSelected);
         
        ToggleNoteListCommand = new Command(() =>
        {
            IsListVisible = !IsListVisible; 
        });
    }

  
    
    public void OnNoteSelected(NoteModel note)
    {
        SelectedNote = note;
        Notes = new ObservableCollection<NoteModel>(Notes.Where(n => n.Id != SelectedNote?.Id));
        IsListVisible = false; 
    }

}