using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace CrudRestFinal
{
    public class Product : INotifyPropertyChanged
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        private string _nombre { get; set; }

        [JsonProperty("precio")]
        private string _precio { get; set; }
        
        [JsonProperty("descripcion")]
        private string _descripcion { get; set; }
        
        [JsonProperty("cantidad")]
        private string _cantidad { get; set; }


        //nombre para mapeo
        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                OnPropertyChanged(); 
            }
        }

        //ageg´´o
        public string Precio
        {
            get => _precio;
            set
            {
                _precio = value;
                OnPropertyChanged(); 
            }
        }
        public string Descripcion
        {
            get => _descripcion;
            set
            {
                _descripcion = value;
                OnPropertyChanged(); 
            }
        }
       
        public string Cantidad
        {
            get => _cantidad;
            set
            {
                _cantidad = value;
                OnPropertyChanged(); 
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
