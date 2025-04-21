using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorProdutos.Entities {
    class Sale {
        public int Id { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Sale(int id, Product product, int quantity, DateTime saleDate) {
            Id = id;
            Product = product;
            Quantity = quantity;
            SaleDate = saleDate;
        }
    }
}
