using MauiApp1.Services;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MauiApp1.Factories;
using MauiApp1.Interfaces;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.ViewModels.Notes.Items;
using MauiApp1.ViewModels.Notes.Managers;
using MauiApp1.Views.Categories;
using MauiApp1.Views.Notes;
using PopupService = MauiApp1.Services.PopupService;


namespace MauiApp1;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp(serviceProvider => new App(serviceProvider))
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit();
       
        
        
        //Register Services
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<INoteService, NoteService>();
        builder.Services.AddSingleton<IPopupService, PopupService>();
        builder.Services.AddSingleton<IModalService, ModalService>();
        
        
        //Register ViewModels
        builder.Services.AddTransient<BaseViewModel>();
             
        //Page
        builder.Services.AddTransient<MainPageViewModel>();
        //Category
        builder.Services.AddTransient<CategoriesViewModel>();
        builder.Services.AddTransient<CreateCategoryViewModel>();
        builder.Services.AddTransient<CreateCategoryButtonViewModel>();
        builder.Services.AddTransient<CategorySelectorViewModel>();
        //Note
        builder.Services.AddTransient<NotesViewModel>();
        builder.Services.AddTransient<CreateNoteButtonViewModel>();
        builder.Services.AddTransient<ManageTextNoteViewModel>();
        builder.Services.AddTransient<ManageAccountNoteViewModel>();
        builder.Services.AddTransient<ManageSourceNoteViewModel>();
        builder.Services.AddTransient<NoteItemViewModel>();
        builder.Services.AddTransient<TextNoteItemViewModel>();
        builder.Services.AddTransient<SourceNoteItemViewModel>();
        builder.Services.AddTransient<CheckListNoteItemViewModel>();
        builder.Services.AddTransient<NoteTypeSelectorViewModel>();
        builder.Services.AddTransient<TextBlocksViewModel>();
        builder.Services.AddTransient<CreateNoteWithSourceViewModel>();
        builder.Services.AddTransient<CreateNoteWithSourceButtonViewModel>();
        builder.Services.AddTransient<SpecificNoteSelectorViewModel>();
        builder.Services.AddTransient<CustomInfoViewModel>();
        builder.Services.AddTransient<CustomQuotesContainerViewModel>();
        builder.Services.AddTransient<CustomCheckBoxViewModel>();
        builder.Services.AddTransient<CheckListViewModel>();
        
        
        //Register Views
        //Category
        builder.Services.AddTransient<CreateCategoryButtonView>();
        builder.Services.AddTransient<CreateCategoryView>();
        builder.Services.AddTransient<HorizontalCategoriesView>();
        builder.Services.AddTransient<CategorySelectorView>();
        //Note
        builder.Services.AddTransient<VerticalNotesView>();
        builder.Services.AddTransient<CreateNoteButtonView>();
        builder.Services.AddTransient<CreateNoteWithSourceButtonView>();
        builder.Services.AddTransient<CreateNoteView>();
        builder.Services.AddTransient<NoteItemView>();
        builder.Services.AddTransient<CustomTypeSelectorView>();
        builder.Services.AddTransient<TextBlocksView>();
        builder.Services.AddTransient<CreateNoteWithSourceView>();
        builder.Services.AddTransient<SpecificNoteSelectorView>();
        builder.Services.AddTransient<CustomQuotesContainerView>();
        builder.Services.AddTransient<CustomInfoView>();
        builder.Services.AddTransient<CheckListView>();
        builder.Services.AddTransient<CustomCheckBoxView>();
        
        //Factories
        builder.Services.AddSingleton<INoteItemFactory, TextNoteItemFactory>();
        builder.Services.AddSingleton<INoteItemFactory, AccountNoteItemFactory>();
        builder.Services.AddSingleton<INoteItemFactory, SourceNoteItemFactory>();
        builder.Services.AddSingleton<INoteItemFactory, CheckListNoteItemFactory>();
        builder.Services.AddSingleton<NoteItemFactoryManager>();

        
        
     
        
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}