using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrudRestFinal
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormEditDelete : ContentPage
    {

        private const string UrlDelete = "https://miapixamarinsv.herokuapp.com/productos/borrar/"; //Eliminar
        private const string UrlUpdate = "https://miapixamarinsv.herokuapp.com/productos/actualizar"; //actualizar

        private readonly HttpClient _client = new HttpClient(); 



        public FormEditDelete(bool isNew = false)
        {
            InitializeComponent();
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {



            if (await validarFormulario())
            { 
            var newId = Convert.ToInt32(id.Text);

            Product product = new Product { Id = newId , Nombre = nombre.Text, Precio = precio.Text, Descripcion = descripcion.Text, Cantidad = cantidad.Text };
            
            string content = JsonConvert.SerializeObject(product);
            await _client.PutAsync(UrlUpdate, new StringContent(content, Encoding.UTF8, "application/json"));


                await DisplayAlert("Exito", "Datos validados y actualizados correctamente", "OK");

                await Navigation.PopAsync();

            
            }


                
        }


        private async Task<bool> validarFormulario()
        {
            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (String.IsNullOrWhiteSpace(nombre.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del nombre es obligatorio.", "OK");
                return false;
            }
            else
            {
                //Valida si se ingresan caracteres especiales
                string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
                bool resultado = Regex.IsMatch(nombre.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
                    return false;
                }
            }


            if (String.IsNullOrWhiteSpace(precio.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del precio es obligatorio.", "OK");
                return false;
            }
            else
            {

                var newprecio = precio.Text;
                bool resultado = Regex.IsMatch(newprecio, @"^-*[0-9,\.]+$");
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "Solo se aceptan números", "OK");
                    return false;
                }
            }



            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (String.IsNullOrWhiteSpace(descripcion.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del descripcion es obligatorio.", "OK");
                return false;
            }
            else
            {
                //Valida si se ingresan caracteres especiales
                string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
                bool resultado = Regex.IsMatch(descripcion.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
                    return false;
                }
            }


            if (String.IsNullOrWhiteSpace(cantidad.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del cantidad es obligatorio.", "OK");
                return false;
            }
            else
            {
                //Valida que solo se ingresen numeros
                if (!cantidad.Text.ToCharArray().All(Char.IsDigit))
                {
                    await this.DisplayAlert("Advertencia", "El formato de cantidad es incorrecto, solo se aceptan numeros enteros.", "OK");
                    return false;
                }
            }


            return true;

        }







        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var newId = Convert.ToInt32(id.Text);


            await _client.DeleteAsync(UrlDelete + newId );

            await DisplayAlert("Operación Exitosa", $"Producto: {this.nombre.Text} Eliminado", "OK");



            await Navigation.PopAsync();
        }





        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }

}