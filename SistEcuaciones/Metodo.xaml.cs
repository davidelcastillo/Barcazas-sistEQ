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
	public partial class Metodo : ContentPage
	{
		public Metodo ()
		{
			InitializeComponent ();
		}

        private void btm3_Clicked(object sender, EventArgs e)
        {
			bool flag = (bool)Application.Current.Properties["flag"];
			flag = true;
            Application.Current.Properties["flag"] = flag;

            Application.Current.Properties["m"] = Convert.ToInt32(txtCantExtra.Text) + Convert.ToUInt16(Application.Current.Properties["n"]);

            Navigation.PushAsync(new MainPage());
        }
    }
}