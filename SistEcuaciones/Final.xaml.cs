using Newtonsoft.Json;
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
    public partial class Final : ContentPage
    {
        double[,] matriz = new double[3, Convert.ToUInt16(Application.Current.Properties["m"]) + 1];
        string aux;
        int n = Convert.ToUInt16(Application.Current.Properties["m"]);

        public Final()
        {
            InitializeComponent();

            aux = Convert.ToString(Application.Current.Properties["matriz"]);
            matriz = JsonConvert.DeserializeObject<double[,]>(aux);

            
        }

        private void Gauss_Seidel(double[,] matriz, int n)
        {
            // 
        }
    }
}