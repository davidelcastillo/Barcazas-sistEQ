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
            InitializeComponent();

            aux = Convert.ToString(Application.Current.Properties["matriz"]);
            matriz = JsonConvert.DeserializeObject<double[,]>(aux);

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Ejecucion(matriz);
   
		}

        private void btm3_Clicked(object sender, EventArgs e)
        {
            if (txtCantExtra.Text != null)
            {
                bool flag = (bool)Application.Current.Properties["flag"];
                flag = true;
                Application.Current.Properties["flag"] = flag;

                Application.Current.Properties["m"] = Convert.ToInt32(txtCantExtra.Text) + Convert.ToUInt16(Application.Current.Properties["n"]);

                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Error", "No ha ingresado cantidad de barcazas extras.", "OK");
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

        // Método de resolución sistema por elimnación de Gauss
        private double[] ResolverSistema(double[,] matriz)
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);

            // Convertir la matriz a una forma triangular superior
            for (int i = 0; i < filas - 1; i++)
            {
                // Pivote para la columna actual
                double pivote = matriz[i, i];

                // Si el pivote es cero, intercambiar filas
                if (pivote == 0)
                {
                    for (int j = i + 1; j < filas; j++)
                    {
                        if (matriz[j, i] != 0)
                        {
                            for (int k = 0; k < columnas; k++)
                            {
                                double temp = matriz[i, k];
                                matriz[i, k] = matriz[j, k];
                                matriz[j, k] = temp;
                            }
                            break;
                        }
                    }
                    pivote = matriz[i, i];
                }

                // Eliminación Gaussiana
                for (int k = i + 1; k < filas; k++)
                {
                    double factor = matriz[k, i] / pivote;
                    for (int j = i; j < columnas; j++)
                    {
                        matriz[k, j] -= factor * matriz[i, j];
                    }
                }
            }

            // Sustitución hacia atrás para encontrar las soluciones
            double[] soluciones = new double[filas];
            for (int i = filas - 1; i >= 0; i--)
            {
                double suma = 0;
                for (int j = i + 1; j < columnas - 1; j++)
                {
                    suma += matriz[i, j] * soluciones[j];
                }
                soluciones[i] = (matriz[i, columnas - 1] - suma) / matriz[i, i];
            }

            return soluciones;
        }
        
        private void Ejecucion(double[,] matriz) {
            double[] rdo = ResolverSistema(matriz);
            int[] aux = new int[rdo.Length];
            for (int i = 0; i < aux.Length; i++)
            {
                aux[i] = Convert.ToInt16( Math.Ceiling(rdo[i]));
            }
            // Mostrar resultado
            txtA.Text = Convert.ToString(aux[0]);
            txtB.Text = Convert.ToString(aux[1]);
            txtC.Text = Convert.ToString(aux[2]);

        }

    }
}