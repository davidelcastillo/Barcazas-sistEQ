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
	public partial class Metodo : ContentPage
	{
        double[,] matriz = new double[3, Convert.ToUInt16(Application.Current.Properties["n"]) + 1];
        string aux;
        int n = Convert.ToUInt16(Application.Current.Properties["n"]);

        public Metodo ()
		{
            aux = Convert.ToString(Application.Current.Properties["matriz"]);
            matriz = JsonConvert.DeserializeObject<double[,]>(aux);
            Gauss_Seidel(matriz, n);

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

        private void Gauss_Seidel(double[,] matriz, int n)
        {
            // Verificaciones en matriz

            if (!SolucionUnica(matriz))
            {
                int filas = 3;
                int columnas = n + 1;

            }

        }

        private bool SolucionUnica(double[,] matriz)
        {
            int cont = 0;
            bool rotated = false;

            while (cont < n)
            {
                for (int i = 0; i < n; i++)
                {
                    if (matriz[i, i] == 0)
                    {
                        RotarMatriz(matriz, i);
                        rotated = true;
                    }
                }

                if (!rotated)
                {
                    break;
                }

                cont++;
            }

            for (int i = 0; i < n; i++)
            {
                if (matriz[i, i] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void RotarMatriz(double[,] matriz, int row)
        {
            for (int i = 0; i <= n; i++)
            {
                double temp = matriz[row, i];
                matriz[row, i] = matriz[(row + 1) % n, i];
                matriz[(row + 1) % n, i] = temp;
            }
        }

    }
}