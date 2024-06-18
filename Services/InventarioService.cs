
using NETMAUICRUDMVVM1.Models;
using SQLite;

namespace NETMAUICRUDMVVM1.Services
{
    public class InventarioService
    {
        private readonly SQLiteConnection dbConnection;

        public InventarioService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Inventario.db3");

            dbConnection = new SQLiteConnection(dbPath);
            dbConnection.CreateTable<Inventario>();

        }


        public List<Inventario> GetAll()
        {
            var res = dbConnection.Table<Inventario>().ToList();
            return res;
        }

        public int Insert(Inventario inventario)
        {
            return dbConnection.Insert(inventario);
        }

        public int Update(Inventario inventario)
        {
            return dbConnection.Update(inventario);
        }
        public int Delete(Inventario inventario)
        {
            return dbConnection.Delete(inventario);
        }
    }
}
