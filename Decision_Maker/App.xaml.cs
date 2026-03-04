using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Decision_Maker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeSupabase();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new MainPage()));
        }

        private async void InitializeSupabase()
        {
            await SupabaseService.InitAsync();
        }
    }
}