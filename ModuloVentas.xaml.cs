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
using System.Windows.Shapes;

namespace FacturacionVentasWPF
{
    /// <summary>
    /// Interaction logic for ModuloVentas.xaml
    /// </summary>
    public partial class ModuloVentas : Window
    {
        public ModuloVentas() => InitializeComponent();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           e.Cancel = true;
        }

        //Instancio un proceso
        private readonly Process procesoCerrar;
        private void Btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            procesoCerrar.Kill();
        }
    }
}
