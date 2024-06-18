
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using NETMAUICRUDMVVM1.Models;
using NETMAUICRUDMVVM1.Services;
using NETMAUICRUDMVVM1.Views;
using CommunityToolkit.Mvvm.Input;

namespace NETMAUICRUDMVVM1.ViewModels
{
    public partial class InventarioMainViewModel : ObservableObject
    {

        //Se crea una coleccion para ver los datos
        [ObservableProperty]
        private ObservableCollection<Inventario> inventarioCollection = new ObservableCollection<Inventario>();

        private readonly InventarioService inventarioService;

        // constructor
        public InventarioMainViewModel()
        {
            inventarioService = new InventarioService();
        }

        public void GetAll()
        {
            var getAll = inventarioService.GetAll();

            // Validando si la lista tiene registros
            if (getAll?.Count() > 0)
            {
                InventarioCollection.Clear();
                foreach (var inventario in getAll)
                {
                    InventarioCollection.Add(inventario);
                }

            }
        }

        [RelayCommand]
        private async Task goToAddInventarioForm()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new AddInventarioForm());
        }

        [RelayCommand]
        private async Task SelectInventario(Inventario inventario)
        {
            try
            {
                string res = await App.Current!.MainPage!.DisplayActionSheet("Opciones", "Cancelar", null, "Actualizar", "Eliminar");
                switch (res)
                {
                    case "Actualizar":
                        await App.Current!.MainPage.Navigation.PushAsync(new AddInventarioForm(inventario));
                        break;
                    case "Eliminar":
                        bool respuesta = await App.Current!.MainPage!.DisplayAlert("ELIMINAR INVENTARIO", "¿Desea eliminar el registro del Inventario?", "Si", "No");
                        if (respuesta)
                        {
                            int del = inventarioService.Delete(inventario);
                            if (del > 0)
                            {
                                InventarioCollection.Remove(inventario);

                            }
                        }

                        break;
                }

            }
            catch (Exception ex)
            {
                Alerta("Error", ex.Message);
            }
        }
        private void Alerta(String Titulo, String Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }
    }
}
