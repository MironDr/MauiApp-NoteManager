using MauiApp1.Services;
using MauiApp1.View;
using MauiApp1.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<ICategoryService,CategoryService>();
        builder.Services.AddTransient<MultiCategoriesViewModel>();
        builder.Services.AddTransient<HorizontalCategoriesView>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}