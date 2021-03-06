using ControlDeColegio.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace ControlDeColegio.ModelView
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Usuarios> usuarios {get; set;}
        
        public UsuarioViewModel()
        {
            this.usuarios = new ObservableCollection<Usuarios>();
            this.usuarios.Add(new Usuarios(1,"etumax", true, "Edwin Rolando", "Tumax Chaclan", "etumax@gmail.com"));
            this.usuarios.Add(new Usuarios(2,"nperez", true, "Nancy Elizabeth", "Perez Carcamo", "nperez@gmail.com"));
            this.usuarios.Add(new Usuarios(3,"caquino", true, "Cesar Agusto", "Aquino Gaitan", "caquino@gmail.com"));
        }   

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

    }
}