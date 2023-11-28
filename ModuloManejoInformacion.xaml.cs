using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static Microsoft.SqlServer.Management.Sdk.Sfc.OrderBy;

namespace FacturacionVentasWPF
{
    /// <summary>
    /// Interaction logic for ModuloManejoInformacion.xaml
    /// </summary>
    /// 
    //xCczxc

    public partial class ModuloManejoInformacion : Window
    {
        public ModuloManejoInformacion()
        {
            InitializeComponent();
            Txt_IdSearch.IsEnabled = false;
        }
      //  private readonly Process process;
        public DataSet dataSetSear = new();
        public DataTable dtBsuquedaC = new();

        //instancio un objeto 
        readonly ClaseConexionBD objCxn = new();

        #region EVENTOS CLICK EN LOS BOTONES
        private void Btn_RegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (Txt_id.Text != "" && Txt_Identificacion.Text != "" && Txt_Direccion.Text != "" && Txt_Correo.Text != "" && Txt_CapCredito.Text != "" && Txt_NombreCliente.Text != "" && Cmb_TipoId.SelectedIndex != -1)
            {
                try //try-catch para manejar las excepciones y errores
                {
                    string tipoDoc = "";
                    switch (Cmb_TipoId.SelectedIndex)
                    {
                        case 0:
                            tipoDoc = "Cedula de Ciudadania";
                            break;
                        case 1:
                            tipoDoc = "Cedula de Extranjeria";
                            break;
                        case 2:
                            tipoDoc = "Pasaporte";
                            break;
                        default:
                            MessageBox.Show("Seleccione un tipo de documento");
                            break;
                    }

                    string strIngresoDatos = objCxn.InsertarResgistrosClientes(Convert.ToDouble(Txt_id.Text), Convert.ToInt32(Txt_CapCredito.Text), Txt_Correo.Text, Txt_Direccion.Text, Convert.ToDouble(Txt_Identificacion.Text), Txt_NombreCliente.Text, Convert.ToDouble(Txt_Telefono.Text), tipoDoc);
                    if (strIngresoDatos == "ingresados")
                    {
                        MessageBox.Show("Los datos fueron guardados correctamente");

                    }
                    else
                    {
                        MessageBox.Show($"{strIngresoDatos}");
                    }
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Existen Campos sin seleccionar o sin llenar");
            }
        }
        private void Btn_BackToPpal_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ventanaPrincipal = new();
            this.Close();
            ventanaPrincipal.Show();
        }
        private void Btn_BusquedaCliente_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {

                if (Rbtn_Id.IsChecked == true)
                {
                    dtBsuquedaC = ClaseConexionBD.GetBusquedaCliente(1, Convert.ToDouble(Txt_IdSearch.Text));
                    if (dtBsuquedaC.Rows.Count > 0)
                    {
                        DgViewClientes.ItemsSource = dtBsuquedaC.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontro nada en la busqueda");
                    }
                }
                if (Rbtn_Identificacion.IsChecked == true)
                {
                    dtBsuquedaC = ClaseConexionBD.GetBusquedaCliente(2, Convert.ToDouble(Txt_IdSearch.Text));
                    if (dtBsuquedaC.Rows.Count > 0)
                    {
                        DgViewClientes.ItemsSource = dtBsuquedaC.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontro nada en la busqueda");
                    }

                }
                if (Rbtn_TodosRegistros.IsChecked == true)
                {
                    dtBsuquedaC = ClaseConexionBD.GetBusquedaCliente(3, 3);
                    DgViewClientes.ItemsSource = dtBsuquedaC.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);

            }

        }
        #endregion

        #region EVENTOS DE CERRADO
        private void Btn_BackToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            this.Close();
            mainWindow.Show();



            //System.Environment.Exit(1); //cierra toda la app
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }
        private void Btn_CerrarApp_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion

        #region VALIDACIONES DE NUMEROS O LETRAS EN DIFERENTES CAMPOS
        //validacion solo letras en el campo nombre
        private void Txt_NombreCliente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)

                e.Handled = false;

            else e.Handled = true;

            //propiedad en tiempo de ejecucion para escribir el nombre en mayusculas
            Txt_NombreCliente.CharacterCasing = CharacterCasing.Upper;
        }

        //validacion solo numero en el campo identificacion
        private void Txt_Identificacion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }

        private void Txt_Telefono_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Txt_Telefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }

        private void Txt_CapCredito_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }

        private void Txt_id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }
        private void Txt_IdSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57) e.Handled = false;

            else e.Handled = true;
        }

        #endregion

        #region Seleccion de parametros de busqueda de registros de clientes
        private void Rbtn_BusqId_Click(object sender, RoutedEventArgs e)
        {

            Txt_IdSearch.IsEnabled = true;
        }

        private void Rbtn_BusqIdentificacion_Click(object sender, RoutedEventArgs e)
        {
            Txt_IdSearch.IsEnabled = true;
        }

        private void Rbtn_TodosRegistros_Click(object sender, RoutedEventArgs e)
        {
            Txt_IdSearch.IsEnabled = false;
        }
        private void Rbtn_Pasaporte_Click(object sender, RoutedEventArgs e)
        {
            Txt_IdSearch.IsEnabled = true;
        }
        #endregion

        //Metodos Auxiliares
        //Metodo para limpiar campos
        public void LimpiarCampos()
        {
            Txt_id.Text = string.Empty;
            Txt_Identificacion.Text = string.Empty;
            Txt_Correo.Text = string.Empty;
            Txt_CapCredito.Text = string.Empty;
            Txt_Direccion.Text = string.Empty;
            Txt_Telefono.Text = string.Empty;
            Txt_NombreCliente.Text = string.Empty;
            Cmb_TipoId.SelectedIndex = -1;
        }

        private void Btn_BorrarRegistro_Click(object sender, RoutedEventArgs e)
        {

            dtBsuquedaC = ClaseConexionBD.GetBusquedaCliente(2, Convert.ToDouble(Txt_IdBorrar.Text));
            if (dtBsuquedaC.Rows.Count > 0)
            {
                string strNombreBorrar = dtBsuquedaC.Rows[0]["nombre_completo"].ToString();
                var Result = MessageBox.Show("Desea eliminar a: " + strNombreBorrar + " de la base de datos", "Registro Encontrado", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result == MessageBoxResult.Yes)
                {
                    bool resElimReg = objCxn.BorrarRegistro(Convert.ToDouble(Txt_IdBorrar.Text));
                    if (resElimReg == true)
                    {
                        MessageBox.Show("Registro Borrado");
                    }
                }
                else if (Result == MessageBoxResult.No)
                {
                    MessageBox.Show("Monicaga  lindo");
                    //Environment.Exit(0);
                }
            }
        }
    }
}
