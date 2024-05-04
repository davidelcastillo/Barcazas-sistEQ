using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace SistEcuaciones
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Properties["n"] = 0;
            Properties["cont"] = 0;
            Properties["matriz"] = "";
            Properties["flag"] = false;
            Properties["m"] = 0;

            MainPage = new NavigationPage(new Principal());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
