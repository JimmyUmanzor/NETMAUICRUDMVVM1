using SQlite;
using System.Diagnostics.CodeAnalysis;

namespace NETMAUICRUDMVVM1.Models
{
    public class Inventario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Ubicacion { get; set; }

    }
}
