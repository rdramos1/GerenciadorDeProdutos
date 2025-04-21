using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorProdutos.Entities;

namespace GerenciadorProdutos.Service {
    class SaleRecorder {
        public List<Sale> Sales { get; private set; }

        public SaleRecorder() {
            Sales = new List<Sale>();
        }

        public void RegisterSale(Product product, int Quantity) {
            Sales.Add(new Sale(Sales.Count, product, Quantity, DateTime.Now));
            
        }

        public void ListSales() {
            Console.WriteLine("Sales Records:");
            if(Sales.Count == 0) {
                Console.WriteLine("No sales registered.");
                return;
            }
            foreach (var sale in Sales) {
                double price = sale.Product.Price * sale.Quantity;
                Console.WriteLine($"Sale ID: {sale.Id}, Product: {sale.Product.Name}, Quantity: {sale.Quantity},Product price {sale.Product.Price}, All price {price}, Date: {sale.SaleDate}");
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
    }
}
