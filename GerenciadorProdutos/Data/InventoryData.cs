using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorProdutos.DTO;
using GerenciadorProdutos.Entities;
using GerenciadorProdutos.Service;

namespace GerenciadorProdutos.Data {
    public class InventoryData {
        public List<ProductDTO> Products { get; set; } = new();
        public List<CategoryDTO> Categories { get; set; } = new();

        public InventoryData() { }
        public InventoryData(InventoryService inv) {
            foreach (var prod in inv.Products) {
                Products.Add(prod.ToDTO());
            }

            foreach (var cat in inv.Categories) {
                Categories.Add(cat.ToDTO());
            }
        }

    }
}
