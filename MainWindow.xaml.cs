using System;
using System.Collections.Generic;
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
using ControlDeColegio.Models;
using ControlDeColegio.Views;

namespace ControlDeColegio
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

        public void VentanaUsuarios(object sender, RoutedEventArgs e)
        {
            UsuariosView VentanaUsuarios = new UsuariosView();
            VentanaUsuarios.ShowDialog();
        }

        public void VentanaRoles(object sender, RoutedEventArgs e)
        {
            RolView VentanaRoles = new RolView();
            VentanaRoles.ShowDialog();
        }

        // public void Saludar(object sender, RoutedEventArgs e)
        // {
        //     Student estudiante = new Student("2021001", "Juan Alberto", "De Leon Pereira", "jalberto@gmail.com", new DateTime(1980,3,30), "Masculino", "24876395");


        //     Teacher profesor = new Teacher("FK-0001","Raul Antonio", "Perez Polanco", "raperez@gmail.com", new DateTime(1972,01,01), "Masculino", "33124578");


        //     MessageBox.Show(profesor.ToString(), "Profesor");
        //     MessageBox.Show(estudiante.ToString(), "Estudiante");
        //     // MessageBox.Show("Hola Mundo !");
            
        // }
    }
}
