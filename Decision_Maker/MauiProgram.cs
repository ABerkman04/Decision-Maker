using Microsoft.Extensions.Logging;
using Supabase;

namespace Decision_Maker
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

    public static class SupabaseService
    {
        public static Client? Client { get; private set; }

        public static async Task InitAsync()
        {
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            Client = new Client(
                "https://jdpqmgzohnerlvsireow.supabase.co",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImpkcHFtZ3pvaG5lcmx2c2lyZW93Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzI1Njc5NzksImV4cCI6MjA4ODE0Mzk3OX0.Jy5kZZOpEMTqyxKjT-fnEOIDHgz281STDNTkuim14ug",
                options
            );

            await Client.InitializeAsync();
        }
    }
}
