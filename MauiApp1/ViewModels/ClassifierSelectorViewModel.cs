using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels.Groups;


namespace MauiApp1.ViewModels.Categories;

public class ClassifierSelectorViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;
    private readonly IGroupService _groupService;

    public CreateCategoryButtonViewModel CreateCategoryButtonViewModel { get; }
    public CreateGroupButtonViewModel CreateGroupButtonViewModel { get; }
    public ICommand CategorySelectedCommand { get; }
    public ICommand GroupSelectedCommand { get; }
    public ICommand ToggleListCommand { get; }
    public ICommand ResetSelectionCommand { get; }

    public ObservableCollection<CategoryModel> Categories { get; private set; } = new();
    public ObservableCollection<GroupModel> Groups { get; private set; } = new();

    public CategoryModel? SelectedCategory {get; private set; }
    public GroupModel? SelectedGroup {get; private set; }

    private bool _isListVisible;
    private bool _isGroupMode = false;

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

    public bool IsGroupMode
    {
        get => _isGroupMode;
        set
        {
           
            if (value != _isGroupMode)
            {
                _isGroupMode = value;
                IsListVisible = false;
                OnPropertyChanged(nameof(IsCategoryMode));
                OnPropertyChanged(nameof(IsGroupMode));
                UpdateList();
            }
        }
    }

    public bool IsCategoryMode => !IsGroupMode;
    
    public bool IsCategoryListVisible => IsCategoryMode && IsListVisible;
    public bool IsGroupListVisible => IsGroupMode && IsListVisible;

    private string _selectedName;
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
  
        

    public ClassifierSelectorViewModel(ICategoryService categoryService, IGroupService groupService, CreateCategoryButtonViewModel createCategoryButtonViewModel, CreateGroupButtonViewModel createGroupButtonViewModel)
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

        ResetSelectionCommand = new Command(() =>
        {
            SelectedCategory = null;
            SelectedGroup = null;
            OnPropertyChanged(nameof(SelectedName));
            UpdateList();
        });


        
        UpdateList();
    }

    private void OnCategorySelected(CategoryModel category)
    {
        SelectedCategory = category;
        IsGroupMode = false;
        SelectedGroup = null;
        IsListVisible = false;
        OnPropertyChanged(nameof(SelectedName));
        UpdateList();
    }

    private void OnGroupSelected(GroupModel group)
    {
        SelectedGroup = group;
        IsGroupMode = true;
        SelectedCategory = null;
        IsListVisible = false;
        OnPropertyChanged(nameof(SelectedName));
        UpdateList();
    }

    public void SelectCategory(CategoryModel? category)
    {
        if(category != null)
            OnCategorySelected(category);
    }
    
    public void SelectGroup(GroupModel? group)
    {
        if(group != null)
            OnGroupSelected(group);
    }

    
    private void UpdateList()
    {
        SelectedName = !IsGroupMode ? (SelectedCategory?.CategoryName ?? "Select Category") :
            (SelectedGroup?.GroupName ?? "Select Group");
        
      
        
        if (!IsGroupMode)
        {
            Categories = new ObservableCollection<CategoryModel>(
                _categoryService.GetCategories().Where(c => c.Id != SelectedCategory?.Id));
            OnPropertyChanged(nameof(Categories));
        }
        else
        {
            Groups = new ObservableCollection<GroupModel>(
                _groupService.GetGroups().Where(g => g.Id != SelectedGroup?.Id));
            OnPropertyChanged(nameof(Groups));
        }
    }
}