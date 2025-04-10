using System.Text;
using GerenciadorProdutos.Exceptions;

namespace GerenciadorProdutos.Entities {
    public class Product {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public Category Category { get; private set; }

        public Product(string name, int id, int quantity, double price, Category category) {

            if (string.IsNullOrEmpty(name)) {
                throw new ProductException("Name cannot be null or empty");
            }
            if (id <= 0) { /*Resolver o B.O do Id*/
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
                    if (product.Id == id) {
                        throw new ProductException("ID already exists in category");
                    }
                }
            }

            Name = name;
            Id = id;
            Quantity = quantity;
            Price = price;
            Category = category;

            Category.Products.Add(this);

        }

        public void ChangeName(string name) {
            Name = name;
        }
        public void ChangeQuantity(int quantity) {
            if (quantity <= 0) {
                throw new ProductException("Quantity cannot be negative");
            }
            else if (quantity < Quantity) {
                throw new ProductException("Quantity cannot be less than current quantity");
            }
            Quantity = quantity;
        }
        public void ChangePrice(double price) {
            Price = price;
        }
        public void ChangeCategory(Category category) {
            Category = category;
        }

        public override string? ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Product Name: {Name}");
            sb.AppendLine($"Product ID: {Id}");
            sb.AppendLine($"Product Quantity: {Quantity}");
            sb.AppendLine($"Product Price: {Price}");
            sb.AppendLine($"Product Category: {Category.Name}");

            return sb.ToString();
        }
    }
}
