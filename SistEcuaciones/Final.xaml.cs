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
            double[,] matriz2 = { { 50.0, 20.0, 40.0, 40.0, 20.0, 4500.0 }, { 30, 50, 30, 20, 60, 4400 }, { 40, 50, 60, 10, 30, 5800 } };

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Ejecucion (matriz2);

            
        }

        private double[] ResolverSistema2(double[,] matriz)
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);

            // Convertir la matriz a una forma triangular superior
            for (int i = 0; i < filas - 1; i++)
            {
                // Pivote para la columna actual
                double pivote = matriz[i, i];

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
                double indept = matriz[i, columnas - 1];
                double suma = 0;

                for (int j = i + 1; j < columnas - 1; j++)
                {
                    if (j == 3 || j == 4)
                    {
                        suma += matriz[i, j] * 9;
                    }
                    else
                    {
                        suma += matriz[i, j] * soluciones[j];
                    }
                }
                soluciones[i] = (matriz[i, columnas - 1] - suma) / matriz[i, i];
            }

            for (int i = 0; i < filas; i++)
            {
                for (int j = i; j < columnas; j++)
                {
                    Console.Write(matriz[i, j].ToString().PadLeft(2, ' ') + " ");
                }
                Console.WriteLine();
            }

            return soluciones;
        }

        private void Ejecucion(double[,]matriz)
        {
            double[] rdo = ResolverSistema2(matriz);

            Console.WriteLine("Resultado:");
            for (int i = 0; i < rdo.Length; i++)
            {
                Console.WriteLine("x{0} = {1}", i + 1, Math.Ceiling(rdo[i]));
            }
            Console.WriteLine("x4 = x5 = 9");

            int[] aux = new int[rdo.Length];
            for (int i = 0;i < aux.Length; i++)
            {
                aux[i] = Convert.ToInt16( Math.Ceiling(rdo[i]));
            }
            // Mostrar resultado
            txtA.Text = Convert.ToString(aux[0]);
            txtB.Text = Convert.ToString(aux[1]);
            txtC.Text = Convert.ToString(aux[2]);
            txtD.Text = "9";
            txtE.Text = "9";
        }


    }
}