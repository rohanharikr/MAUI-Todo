using Microsoft.Extensions.Logging;
using TODOApp.ViewModels;
using TODOApp.Views;

namespace TODOApp;

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

		builder.Services.AddSingleton<TaskViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<Completed>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

