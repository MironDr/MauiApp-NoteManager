using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp1.DTOs;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Groups;

namespace MauiApp1.ViewModels;

public class ClassifierSelectorViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;
    private readonly IGroupService _groupService;

    public CreateCategoryButtonViewModel CreateCategoryButtonViewModel { get; }
    public CreateGroupButtonViewModel CreateGroupButtonViewModel { get; }

    public ICommand CategorySelectedCommand { get; }
    public ICommand GroupSelectedCommand { get; }
    public ICommand ToggleListCommand { get; }

    public ObservableCollection<CategoryModel> Categories { get; private set; } = new();
    public ObservableCollection<GroupModel> Groups { get; private set; } = new();

    public CategoryModel? SelectedCategory { get; private set; }
    public GroupModel? SelectedGroup { get; private set; }

    private bool _isListVisible;
    public bool IsListVisible
    {
        get => _isListVisible;
        set
        {
            if (value != _isListVisible)
            {
                _isListVisible = value;
                OnPropertyChanged(nameof(IsListVisible));
                OnPropertyChanged(nameof(IsCategoryListVisible));
                OnPropertyChanged(nameof(IsGroupListVisible));
            }
        }
    }

    private bool _isGroupMode;
    public bool IsGroupMode
    {
        get => _isGroupMode;
        set
        {
            if (value != _isGroupMode)
            {
                _isGroupMode = value;
                IsListVisible = false;
                OnPropertyChanged(nameof(IsGroupMode));
                OnPropertyChanged(nameof(IsCategoryMode));
                OnPropertyChanged(nameof(MainNoteNotSelected));
                UpdateList();
            }
        }
    }

    public bool IsCategoryMode => !IsGroupMode;
    public bool IsCategoryListVisible => IsCategoryMode && IsListVisible;
    public bool IsGroupListVisible => IsGroupMode && IsListVisible;

    private int? _noteMainGroupId;
    public bool IsNoteMainInGroup { get; set; }

    public bool MainNoteNotSelected =>
        (SelectedGroup?.GetMainNote() == null || SelectedGroup.Id == _noteMainGroupId) && IsGroupMode && SelectedGroup != null;

    private string _selectedName = string.Empty;
    public string SelectedName
    {
        get => _selectedName;
        set
        {
            if (_selectedName != value)
            {
                _selectedName = value;
                OnPropertyChanged(nameof(SelectedName));
            }
        }
    }

    public ClassifierSelectorViewModel(
        ICategoryService categoryService,
        IGroupService groupService,
        CreateCategoryButtonViewModel createCategoryButtonViewModel,
        CreateGroupButtonViewModel createGroupButtonViewModel)
    {
        _categoryService = categoryService;
        _groupService = groupService;
        CreateCategoryButtonViewModel = createCategoryButtonViewModel;
        CreateGroupButtonViewModel = createGroupButtonViewModel;

        CategorySelectedCommand = new Command<CategoryModel>(OnCategorySelected);
        GroupSelectedCommand = new Command<GroupModel>(OnGroupSelected);

        ToggleListCommand = new Command(() =>
        {
            UpdateList();
            IsListVisible = !IsListVisible;
        });

        UpdateList();
    }

    private void OnCategorySelected(CategoryModel category)
    {
        SelectedCategory = category;
        SelectedGroup = null;
        IsGroupMode = false;
        IsListVisible = false;

        UpdateSelectedName();
        UpdateList();
    }

    private void OnGroupSelected(GroupModel group)
    {
        SelectedGroup = group;
        SelectedCategory = null;
        IsGroupMode = true;
        IsListVisible = false;
        IsNoteMainInGroup = _noteMainGroupId != null && _noteMainGroupId == group.Id;
        UpdateSelectedName();
        OnPropertyChanged(nameof(MainNoteNotSelected));
        OnPropertyChanged(nameof(IsNoteMainInGroup));

        UpdateList();
    }

    public void SelectCategory(CategoryModel? category)
    {
        if (category != null)
            OnCategorySelected(category);
        else if(Categories.Count > 0)
            OnCategorySelected(Categories.FirstOrDefault());
    }

    public void SelectGroup(GroupModel? group, bool isMainNote)
    {
        if (group != null)
        {
            if(isMainNote)
                _noteMainGroupId = group.Id;
            else
                _noteMainGroupId = null;
            
            IsNoteMainInGroup = isMainNote;
            
            OnGroupSelected(group);
        }
    }

    private void UpdateList()
    {
        UpdateSelectedName();

        if (IsGroupMode)
        {
            Groups = new ObservableCollection<GroupModel>(
                _groupService.GetGroups().Where(g => g.Id != SelectedGroup?.Id));
            OnPropertyChanged(nameof(Groups));
        }
        else
        {
            Categories = new ObservableCollection<CategoryModel>(
                _categoryService.GetCategories().Where(c => c.Id != SelectedCategory?.Id));
            OnPropertyChanged(nameof(Categories));
        }
    }

    private void UpdateSelectedName()
    {
        SelectedName = IsGroupMode
            ? SelectedGroup?.GroupName ?? "Select Group"
            : SelectedCategory?.CategoryName ?? "Select Category";
    }
}