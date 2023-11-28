using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FacturacionVentasWPF
{
    /// <summary>
    /// Interaction logic for ModuloCatProd.xaml
    /// </summary>
    public partial class ModuloCatProd : Window
    {
        readonly ClaseConexionBD objConexionBD = new();
        DataTable dtCategorias = new();
       
        public ModuloCatProd()
        {
            InitializeComponent();
            //Llenar el combo box con las cateogiras de productos
            dtCategorias = ClaseConexionBD.GetBusqIdCatProd();

            for (int i = 0; i < dtCategorias.Rows.Count; i++)
            {
                Cmb_Categoria_Prod.Items.Add(dtCategorias.Rows[i][1].ToString());
                Txt_idCat.Text = (Convert.ToInt32(dtCategorias.Rows[i][0].ToString()) + 1).ToString();
            }
            if (Cmb_Categoria_Prod.Items.Count==0)
            {
                Txt_idCat.Text = "1";
            }
        }

        #region Codigo de eventos click de Botones
        private void Btn_Regresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();

        }

        private void Btn_RegistrarCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (Txt_Descripcion.Text!="" && Txt_Ubicacion.Text!="")
            {
                string resultIngreso = objConexionBD.InsertarResgistroCategorias(Convert.ToDouble(Txt_idCat.Text), Txt_Descripcion.Text, Txt_Ubicacion.Text);
                MessageBox.Show("El resultado del registro fue: " + resultIngreso);
                // string resultString = Regex.Match(resultIngreso, @"\b+" ).Value;
                string a = resultIngreso;
                string b = string.Empty;
                int val;
                for (int i = 0; i < a.Length; i++)
                {
                    if (Char.IsDigit(a[i])) b += a[i];
                }
                if (b.Length > 0)
                {
                    val = int.Parse(b);
                    Txt_idCat.Text = (val + 1).ToString();
                    Txt_Descripcion.Text = "";
                    Txt_Ubicacion.Text = "";
                    MessageBox.Show("El numero de registros es: " + val);
                }
                Cmb_Categoria_Prod.Items.Add(Txt_Descripcion.Text).ToString();  
            }
            else
            {
                MessageBox.Show("Debe llenar los campos requeridos");
            }
            

            // MessageBox.Show("Numero de registros: "+resultString);

        }



        private void Btn_Ver_Categorias_Click(object sender, RoutedEventArgs e)
        {
            dtCategorias = ClaseConexionBD.GetBusquedaCategorias();
            DG_View_Categorias.ItemsSource = dtCategorias.DefaultView;

        }

        private void Btn_Ingresar_Producto_Click(object sender, RoutedEventArgs e)
        {
            /// string cadenaSeleccionada = Cmb_Categoria_Prod.SelectedItem.ToString();

            //  MessageBox.Show("La seleccion fue: " + cadenaSeleccionada);
            if (Cmb_Categoria_Prod.SelectedIndex != -1 && Txt_Id_Producto.Text != "" && Txt_Desc_Prod.Text != "" && Txt_Exist_Prod.Text != "" && Txt_Precio_Prod.Text != "")
            {
                bool resultBusque = ClaseConexionBD.ValidarIdProductoIngresar(Convert.ToDouble(Txt_Id_Producto.Text));
                if (resultBusque == false)
                {
                    string resRegProd = objConexionBD.RegistrarNuevosProductos(Convert.ToInt32(Txt_Id_Producto.Text), Txt_Desc_Prod.Text, Convert.ToInt32(Txt_Exist_Prod.Text), Convert.ToDecimal(Txt_Precio_Prod.Text), (Convert.ToDouble(Cmb_Categoria_Prod.SelectedIndex)) + 1);
                    MessageBox.Show("Resultado del ingreso: " + resRegProd);
                    LimpiarCampos();
                }
                else
                    MessageBox.Show("Ya existe un producto en la base de datos con ese id");
            }
            else
            {
                MessageBox.Show("Debe llenar los campos en su totalidad");
            }
        }

        #endregion

        #region Bloque de codigo validaciones
        private void Txt_Id_Producto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }

        private void Txt_Exist_Prod_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }

        private void Txt_Precio_Prod_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }

        private void Txt_idCat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }
        #endregion

        public void LimpiarCampos() 
        {
            Txt_Id_Producto.Text = string.Empty;
            Txt_Descripcion.Text = string.Empty;
            Txt_Exist_Prod.Text = string.Empty; 
            Txt_Precio_Prod.Text = string.Empty;
            Txt_Desc_Prod.Text = string.Empty;
            Txt_Ubicacion.Text = string.Empty;
            Cmb_Categoria_Prod.SelectedIndex = -1;
        }

    }
}
