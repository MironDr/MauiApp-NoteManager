using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.View;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Groups;
using MauiApp1.ViewModels.Notes;
using MauiApp1.ViewModels.ProtectionProfiles;

namespace MauiApp1.Views.ElementsViews;

public partial class CustomBottomBarView : BaseView
{
    public CustomBottomBarView(CreateCategoryButtonViewModel createCategoryButtonViewModel, CreateGroupButtonViewModel createGroupButtonViewModel, CreateNoteButtonViewModel createNoteButtonViewModel, CreateProtectionProfileButtonViewModel createProtectionProfileButtonViewModel)
    {
        InitializeComponent();
        CategoryButton.BindingContext = createCategoryButtonViewModel;
        NoteButton.BindingContext = createNoteButtonViewModel;
        GroupButton.BindingContext = createGroupButtonViewModel;
        ProfileButton.BindingContext = createProtectionProfileButtonViewModel;
    }
}