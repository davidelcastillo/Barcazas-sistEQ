using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace SistEcuaciones
{
    public partial class MainPage : ContentPage
    {
        int cont = Convert.ToUInt16(Application.Current.Properties["cont"]);
        int n = Convert.ToUInt16(Application.Current.Properties["n"]);
        int m = Convert.ToUInt16(Application.Current.Properties["m"]);
        int cont2 = Convert.ToUInt16(Application.Current.Properties["n"]);
        bool flag = (bool)Application.Current.Properties["flag"];
        double[,] matriz = new double[3, Convert.ToUInt16(Application.Current.Properties["n"]) + 1];
        double[,] matriz2;
        bool flag2 = true;

        public MainPage()
        {
            InitializeComponent();
            lblCantBrz.Text = "Numero de barcazas: "  + Convert.ToString(Application.Current.Properties["n"]);
            bool flag = (bool)Application.Current.Properties["flag"];  

            if (flag)
            {
                lblcapacidad.Text = "Ingrese los datos de la barcaza N° 4";
                lblCantBrz.Text = "Numero de barcazas: " + Convert.ToString(Application.Current.Properties["m"]); 
            }
        }

        private void btm2_Clicked(object sender, EventArgs e)
        {
            bool flag = (bool)Application.Current.Properties["flag"];

            if (txtA.Text == null)
            {
                DisplayAlert("Error", "Porfavor ingrese cantidad del tipo A.", "OK");
            }
            else if (txtB.Text ==null)
            {
                DisplayAlert("Error", "Porfavor ingrese cantidad del tipo B.", "OK");
            }
            else if (txtC.Text ==null)
            {
                DisplayAlert("Error", "Porfavor ingrese cantidad del tipo C.", "OK");
            }
            else {
                if (flag)
                {
                    lblCantBrz.Text = "Numero de barcazas: " + Convert.ToString(Application.Current.Properties["m"]); 
                    if (flag2) { ConstruirMatriz2(); }
                    flag2 = false;

                    lblcapacidad.Text = "Ingrese los datos de la barcaza N° " + (Convert.ToInt16(cont2) + 2);
                    // Ingreso de Datos faltantes en matriz 2

                    matriz2[0, cont2] = Convert.ToDouble(txtA.Text);
                    matriz2[1, cont2] = Convert.ToDouble(txtB.Text);
                    matriz2[2, cont2] = Convert.ToDouble(txtC.Text);

                    if (cont2 == m - 1)
                    {
                        string aux = JsonConvert.SerializeObject(matriz2);
                        Application.Current.Properties["matriz"] = aux;

                        Navigation.PushAsync(new Final());
                    }

                    cont2++;

                    txtA.Text = "";
                    txtB.Text = "";
                    txtC.Text = "";

                }
                else
                {

                    if (cont == 0)
                    {
                        matriz[0, n] = Convert.ToDouble(txtA.Text);
                        matriz[1, n] = Convert.ToDouble(txtB.Text);
                        matriz[2, n] = Convert.ToDouble(txtC.Text);

                    }

                    else
                    {
                        matriz[0, cont - 1] = Convert.ToDouble(txtA.Text);
                        matriz[1, cont - 1] = Convert.ToDouble(txtB.Text);
                        matriz[2, cont - 1] = Convert.ToDouble(txtC.Text);

                        if (cont == n)
                        {
                            string aux = JsonConvert.SerializeObject(matriz);
                            Application.Current.Properties["matriz"] = aux;

                            Navigation.PushAsync(new Metodo());
                        }
                    }

                    lblcapacidad.Text = "Ingrese los datos de la barcaza N° " + Convert.ToInt16(cont + 1);
                    cont++;

                    txtA.Text = "";
                    txtB.Text = "";
                    txtC.Text = "";
                }
            } 

        }

        private void ConstruirMatriz2()
        { 
         matriz2 = new double[3, m + 1];

         string aux = Convert.ToString(Application.Current.Properties["matriz"]);
         matriz = JsonConvert.DeserializeObject<double[,]>(aux);

            //RECORRER MATRIZ 1, COPIAR LOS DATOS A MATRIZ 2 
        
         for (int i = 0; i < n; i++)
         {
            matriz2[0, i] = matriz[0, i];
            matriz2[1, i] = matriz[1, i];
            matriz2[2, i] = matriz[1, i];
         }

           matriz2[0, m] = matriz[0, n];
           matriz2[1, m] = matriz[1, n];
           matriz2[2, m] = matriz[1, n];


        }
    }
}
