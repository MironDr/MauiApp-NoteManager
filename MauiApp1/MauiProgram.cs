using MauiApp1.Services;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MauiApp1.ViewModels;
using MauiApp1.ViewModels.Categories;
using MauiApp1.ViewModels.Notes;
using MauiApp1.Views.Categories;
using MauiApp1.Views.Notes;
using PopupService = MauiApp1.Services.PopupService;


namespace MauiApp1;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();
       
        
        
        //Register Services
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<INoteService, NoteService>();
        builder.Services.AddSingleton<IPopupService, PopupService>();
        
        //Register ViewModels
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
            builder.Services.AddTransient<ManageNoteViewModel>();
            builder.Services.AddTransient<NoteItemViewModel>();
        
        //Register Views
            //Category
            builder.Services.AddTransient<CreateCategoryButtonView>();
            builder.Services.AddTransient<CreateCategoryView>();
            builder.Services.AddTransient<HorizontalCategoriesView>();
            builder.Services.AddTransient<CategorySelectorView>();
            //Note
            builder.Services.AddTransient<VerticalNotesView>();
            builder.Services.AddTransient<CreateNoteButtonView>();
            builder.Services.AddTransient<CreateNoteView>();
            builder.Services.AddTransient<NoteItemView>();
        
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}