using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Lista : ContentPage
	{
		public Lista ()
		{
            try
            {
                ClsCargar estudiante = new ClsCargar();
                BindingContext = estudiante.GetDafaults();
                //Combo();
                InitializeComponent();
            }
            catch (Exception ex)
            {
                DisplayAlert("Conectado!", ex.ToString(), "OK");
            }
            
		}
        List<string> iten = new List<string>();
        public void Combo()
        {
            iten = new List<string>();
            iten.Add("Arroz");
            iten.Add("Cebolla");
            iten.Add("Leche");
            iten.Add("Papa");//=
            ClsCargar estudiante = new ClsCargar();
            PicProducto.ItemsSource = iten;
            iten = new List<string>();
            iten.Add("001");
            iten.Add("002");
            iten.Add("003");
            iten.Add("004");//=
            PicProducto.SelectedItem = iten;

        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            iten = new List<string>();
            iten.Add("Arroz");
            iten.Add("Cebolla");
            iten.Add("Leche");
            iten.Add("Papa");
            ClsCargar estudiante = new ClsCargar();
            PicProducto.ItemsSource = iten;
            iten = new List<string>();
            iten.Add("001");
            iten.Add("002");
            iten.Add("003");
            iten.Add("004");
            PicProducto.SelectedItem = iten;
        }

        private void PicProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = PicProducto.SelectedIndex;
            if (indice>-1)
            {
                DisplayAlert("Conectado!", iten[indice].ToString(), "OK");
                DisplayAlert("Conectado!", PicProducto.SelectedIndex.ToString(), "OK");
                DisplayAlert("Conectado!", PicProducto.SelectedItem.ToString(), "OK");
            }
        }
    }
}