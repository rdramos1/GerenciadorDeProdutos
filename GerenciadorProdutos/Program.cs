using System.Globalization;
using GerenciadorProdutos.Data;
using GerenciadorProdutos.DTO;
using GerenciadorProdutos.Entities;
using GerenciadorProdutos.Repositories;
using GerenciadorProdutos.Service;

namespace GerenciadorProdutos {
    public class Program {
        public static void Main(string[] args) {

            

            var repo = new JsonRepository("C:\\temp\\GerenciadorDeProdutos\\data.json");
            var AppData = repo.GetAll() ?? new AppData();

            var inv = new InventoryService(AppData) ?? new InventoryService();
            var sale = new SaleService(AppData) ?? new SaleService();

            var menu = new Menu(inv, sale);

            AppDomain.CurrentDomain.ProcessExit += (s, e) => {
                var _data = new AppData(inv, sale);

                repo.SaveAll(_data);
                Console.WriteLine("Data saved to file.");
            };

            try {
                menu.ShowPrincipalMenu();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        static void FillData(InventoryService inv, SaleService sale) {
            inv.CreateCategory("Beverages");
            inv.CreateCategory("Snacks");
            inv.CreateCategory("Dairy");
            inv.AddProductByCategory("Coca-Cola", 10, 5.50, inv.Categories[0]);
            inv.AddProductByCategory("Pepsi", 20, 4.50, inv.Categories[0]);
            inv.AddProductByCategory("Chips", 15, 2.50, inv.Categories[1]);
            inv.AddProductByCategory("Cookies", 30, 3.00, inv.Categories[1]);
            inv.AddProductByCategory("Milk", 25, 1.50, inv.Categories[2]);
            inv.AddProductByCategory("Yogurt", 20, 2.00, inv.Categories[2]);
            inv.AddProductByCategory("Cheese", 10, 4.00, inv.Categories[2]);
            sale.RegisterSale(inv.Products[0], 2);
            sale.RegisterSale(inv.Products[1], 1);
            sale.RegisterSale(inv.Products[2], 3);
        }

    }
}










