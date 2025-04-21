using System.Globalization;
using GerenciadorProdutos.Data;
using GerenciadorProdutos.Entities;
using GerenciadorProdutos.Interfaces;
using GerenciadorProdutos.Service;

namespace GerenciadorProdutos {
    public class Program {
        public static void Main(string[] args) {

            /*IDataBase dataBase = new JsonDataBase("C:\\temp\\GerenciadorDeProdutos");

            Inventory inventory = dataBase.Load(new Inventory());
            SaleRecorder saleRecorder = dataBase.Load(new SaleRecorder());

            dataBase.Save(inventory);
            dataBase.Save(saleRecorder); 
            */

            Inventory inventory = new Inventory();
            SaleRecorder saleRecorder = new SaleRecorder();

            inventory.CreateCategory("Electronics");
            inventory.CreateCategory("Clothing");
            inventory.AddProductByCategory("Laptop", 10, 1500.00, inventory.Categories[0]);
            inventory.AddProductByCategory("Smartphone", 20, 800.00, inventory.Categories[0]);

            Screen.start(inventory, saleRecorder);

            Console.WriteLine(inventory.ToString());

            Console.ReadLine();

        }

    }

}








