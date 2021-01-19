using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace CrudRestFinal
{
    public partial class MainPage : ContentPage
    {

        private const string Url = "https://miapixamarinsv.herokuapp.com/productos"; 
        private const string Urladd = "https://miapixamarinsv.herokuapp.com/productos/guardar"; 
     
        private readonly HttpClient _client = new HttpClient(); 
        private ObservableCollection<Product> _products; 

        public MainPage()
        {
            InitializeComponent();
           
        }
        protected override async void OnAppearing()
        {
            string content = await _client.GetStringAsync(Url); 
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(content); //Deserializar json
            _products = new ObservableCollection<Product>(products); 
            MyListView.ItemsSource = _products; //llenar lista
            base.OnAppearing();
        }


        //para agregar
        async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormAdd());
        }


        //Parallel edicion
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new FormEditDelete
            {
                BindingContext = e.SelectedItem as Product
            });
        }

    }
}

