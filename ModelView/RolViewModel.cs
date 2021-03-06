using System.Collections.ObjectModel;
using System.ComponentModel;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class RolViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Rol> roles {get; set;}
        public RolViewModel()
        {
            roles = new ObservableCollection<Rol>();
            roles.Add(new Rol(1, "ROLE_ADMIN"));
            roles.Add(new Rol(2, "ROLE_USER"));
            roles.Add(new Rol(3, "ROLE_SUPERV"));
        } 

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}