using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorProdutos.Service;

namespace GerenciadorProdutos.Data {
    public class AppData {
        public InventoryData InventoryData { get; set; } = new();
        public SaleData SaleData { get; set; } = new();

        public AppData() { }
        public AppData(InventoryService inv, SaleService sale) {
            SaleData.Sale = sale.Sales;

            foreach(var prod in inv.Products) {
                InventoryData.Products.Add(prod.ToDTO());
            }
            
            foreach(var cat in inv.Categories) {
                InventoryData.Categories.Add(cat.ToDTO());
            }


        }
    }
}
