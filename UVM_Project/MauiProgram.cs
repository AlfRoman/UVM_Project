using Microsoft.Extensions.Logging;
using UVM_Project.DataAccess;
using UVM_Project.ViewModels;
using UVM_Project.Views;

namespace UVM_Project
{
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

            var dbContext = new UserDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<UserDbContext>();

            builder.Services.AddTransient<UserPage>();
            builder.Services.AddTransient<UserViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
