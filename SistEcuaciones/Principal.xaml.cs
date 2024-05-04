using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SistEcuaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {

        public Principal()
        {
            InitializeComponent();
        }

        private void btm1_Clicked (object sender, EventArgs e)
        {
            Application.Current.Properties["n"] = Convert.ToInt32(txtCantBrz.Text);
            Navigation.PushAsync(new MainPage());
        }
    }
}