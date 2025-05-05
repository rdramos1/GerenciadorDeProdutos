using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorProdutos.DTO;

namespace GerenciadorProdutos.Entities {
    public class Sale {
        public int Id { get; private set; }
        public ProductDTO Product { get; private set; }
        public int Quantity { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Sale(int id, ProductDTO product, int quantity, DateTime saleDate) {
            Id = id;
            Product = product;
            Quantity = quantity;
            SaleDate = saleDate;
        }
    }
}
