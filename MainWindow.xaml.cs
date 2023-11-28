using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FacturacionVentasWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //instancias de objetos
        readonly ModuloVentas mVentas = new(); //instancio el objeto de tipo ModuloVentas para 
        readonly ModuloManejoInformacion mInfo = new();
        readonly ModuloCatProd moduloCatProd = new();
        private void Btn_ModuloVentas_Click(object sender, RoutedEventArgs e)
        {
           mVentas.Show(); //abro el objeto tipo modulo ventas
           this.Close(); //cierro la ventana actual
        }

        private void Btn_ModuloInformacion_Click(object sender, RoutedEventArgs e)
        {
            mInfo.Show(); //abro el objeto tipo modulo informacion
            this.Close(); //cierro la ventana actual
        }
        private void Btn_ModuloProdyCat_Click(object sender, RoutedEventArgs e)
        {
             moduloCatProd.Show();
            this.Close();
        }

        private void Btn_CerrarApp_Click(object sender, RoutedEventArgs e)
        {
            Process prcessClose = new();
            prcessClose.Kill();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false; 
        }


    }
}
