using System.Text;
using GerenciadorProdutos.Exceptions;

namespace GerenciadorProdutos.Entities {
    public class Product {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }

        public Product(string name,int id, int quantity, double price, Category category) {

            Name = name;
            Id = id;
            Quantity = quantity;
            Price = price;
            Category = category;

            if (string.IsNullOrEmpty(name)) {
                throw new ProductException("Name cannot be null or empty");
            }
            if (id < 0) { 
                throw new ProductException("ID cannot be negative");
            }
            if (quantity < 0) {
                throw new ProductException("Quantity cannot be negative");
            }
            if (price <= 0) {
                throw new ProductException("Price cannot be negative");
            }
            if (category == null) {
                throw new ProductException("Category cannot be null");
            }
            if (category.Products == null) {
                throw new ProductException("Category cannot be null");
            }
            if (category.Products.Contains(this)) {
                throw new ProductException("Product already exists in category");
            }
            if (category.Products.Count > 0) {
                foreach (Product product in category.Products) {
                    if (product.Id == Id) {
                        throw new ProductException("ID already exists in category");
                    }
                }
            }

            

        }

        public override string ToString() {
            return $"Product Name: {Name}\nProduct ID: {Id}\nProduct Quantity: {Quantity}\nProduct Price: {Price.ToString("N")}\nProduct Category: {Category.Name}\n";
        }

    }
}
