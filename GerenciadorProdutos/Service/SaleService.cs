using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorProdutos.Data;
using GerenciadorProdutos.Entities;
using GerenciadorProdutos.Repositories;

namespace GerenciadorProdutos.Service {
    public class SaleService {
        public List<Sale> Sales { get; private set; } = new();

        public SaleService() {
            Sales = new List<Sale>();
        }
        public SaleService(AppData data) {
            Sales = data.SaleData.Sale;
        }
        public SaleService(SaleData data) {
            Sales = data.Sale;
        }

        public void RegisterSale(Product product, int Quantity) {
            Sales.Add(new Sale(Sales.Count, product.ToDTO(), Quantity, DateTime.Now));
            
        }

        public void ListSales() {
            Console.WriteLine("Sales Records:");
            if(Sales.Count == 0) {
                Console.WriteLine("No sales registered.");
                return;
            }
            foreach (var sale in Sales) {
                double price = sale.Product.Price * sale.Quantity;
                Console.WriteLine($"Sale ID: {sale.Id}, Product: {sale}, Quantity: {sale.Quantity} ,Product price ${sale.Product.Price.ToString("N")}, All price ${price.ToString("N")}, Date: {sale.SaleDate}");
            }
        }

        public void DeleteSale(Sale Sale) {
            Sales.Remove(Sale);

        }

        public Sale GetLastSale() {
            if (Sales.Count == 0) {
                return null;
            }
            return Sales.Last();
        }

        public override string? ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sales Records:");
            if (Sales.Count == 0) {
                sb.AppendLine("No sales registered.");
                return sb.ToString();
            }
            foreach (var sale in Sales) {
                double price = sale.Product.Price * sale.Quantity;
                sb.AppendLine($"Sale ID: {sale.Id}, Product: {sale}, Quantity: {sale.Quantity},Product price {sale.Product.Price.ToString("N")} , All price {price.ToString("N")} , Date: {sale.SaleDate}");
            }
            return sb.ToString();
        }
    }
}
