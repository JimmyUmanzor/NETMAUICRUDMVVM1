
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameKit;
using NETMAUICRUDMVVM1.Models;
using NETMAUICRUDMVVM1.Services;

namespace NETMAUICRUDMVVM1.ViewModels
{
    public partial class AddInventarioFormViewModel : ObservableObject
    {
        [ObservableProperty]
        private int idProperty;

        [ObservableProperty]
        private string nombreProperty;

        [ObservableProperty]
        private int cantidadProperty;

        [ObservableProperty]
        private double precioProperty;

        [ObservableProperty]
        private string ubicacionProperty;


        private readonly InventarioService inventarioService;

        public AddInventarioFormViewModel()
        {
            inventarioService = new InventarioService();
        }

        public AddInventarioFormViewModel(Inventario inventario)
        {
            IdProperty = inventario.Id;
            NombreProperty = inventario.Nombre;
            CantidadProperty = inventario.Cantidad;
            PrecioProperty = inventario.Precio;
            UbicacionProperty = inventario.Ubicacion;


            inventarioService = new InventarioService();
        }

        private void Alerta(String Titulo, String Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }

        [RelayCommand]
        private async Task AddUpdate()
        {
            try
            {
                Inventario inventario = new Inventario
                {
                    Id = idProperty,
                    Nombre = nombreProperty,
                    Cantidad = cantidadProperty,
                    Precio = precioProperty,
                    Ubicacion = ubicacionProperty
                };

                if (Validar(inventario))
                {
                    if (IdProperty == 0)
                    {
                        InventarioService.Insert(inventario);
                    }
                    else
                    {
                        inventarioService.Update(inventario);
                    }
                    await App.Current!.MainPage!.Navigation.PopAsync();

                }


            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

        private bool Validar(Inventario inventario)
        {
            if (inventario.Nombre == null || inventario.Nombre == "")
            {
                Alerta("ADVERTENCIA", "Ingrese el Nombre del artículo");
                return false;
            }
            else if (inventario.Cantidad == 0)
            {
                Alerta("ADVERTENCIA", "Ingrese la cantidad");
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
