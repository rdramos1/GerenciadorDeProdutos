using System.Data.Common;
using System.Text;
using GerenciadorProdutos.Data;
using GerenciadorProdutos.Entities;
using GerenciadorProdutos.Exceptions;
using GerenciadorProdutos.Repositories;

namespace GerenciadorProdutos.Service {
    public class InventoryService {
        public List<Product> Products { get; private set; } = new();
        public List<Category> Categories { get; private set; } = new();

        public InventoryService() {
            Products = new List<Product>();
            Categories = new List<Category>();
        }

        public InventoryService(AppData data) {

            foreach (var catDTO in data.InventoryData.Categories) {
                Categories.Add(new Category(catDTO.Name, catDTO.Id));
            }

            foreach (var ProdDTO in data.InventoryData.Products) {
                var category = Categories.FirstOrDefault(c => c.Id == ProdDTO.CategoryId);
                if (category == null) {
                    throw new InventoryException("Category not found");
                }
                Products.Add(new Product(ProdDTO.Name, ProdDTO.Id, ProdDTO.Quantity, ProdDTO.Price, category));
                category.Products.Add(Products.Last());
            }

        }
        public InventoryService(InventoryData data) {
            foreach (var catDTO in data.Categories) {
                Categories.Add(new Category(catDTO.Name, catDTO.Id));
            }

            foreach (var ProdDTO in data.Products) {
                var category = Categories.FirstOrDefault(c => c.Id == ProdDTO.CategoryId);
                if (category == null) {
                    throw new InventoryException("Category not found");
                }
                Products.Add(new Product(ProdDTO.Name, ProdDTO.Id, ProdDTO.Quantity, ProdDTO.Price, category));

            }
        }

        public void AddProductByCategory(string name, int quantity, double price, Category category) {
            Product product = new Product(name, Products.Count, quantity, price, category);
            Products.Add(product);
            if (category == null) {
                throw new InventoryException("Category not found");
            }
            if (!category.Products.Contains(product)) {
                category.Products.Add(product);
            }
        }
        public void AddProductByCategoryId(string name, int quantity, double price, int categoryId) {
            Category category = Categories.FirstOrDefault(c => c.Id == categoryId);

            AddProductByCategory(name, quantity, price, category);
        }
        public void CreateCategory(string name) {
            if (string.IsNullOrEmpty(name)) {
                throw new InventoryException("Category name cannot be null or empty");
            }
            if (Categories.Any(c => c.Name == name)) {
                throw new InventoryException("Category already exists");
            }
            Categories.Add(new Category(name, Categories.Count));
        }


        public void UpdateProduct(int productId, string name, int quantity, double price, Category category) {
            Product product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) {
                throw new InventoryException("Product not found");
            }
            product.Name = name;
            product.Quantity = quantity;
            product.Price = price;
            product.Category = category;
        }

        public void SellProduct(int productId, int quantity) {
            Product product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) {
                throw new InventoryException("Product not found");
            }
            if (product.Quantity < quantity) {
                throw new InventoryException("Not enough stock");
            }
            product.Quantity -= quantity;
        }
        public void RestockProduct(int productId, int quantity) {
            Product product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) {
                throw new InventoryException("Product not found");
            }
            product.Quantity += quantity;
        }



        public string GetProducts() {
            return string.Join("\n", Products.Select(p => p.ToString()));
        }

        public string GetCategories() {
            return string.Join("\n", Categories.Select(c => c.ToString()));
        }
        public string GetProductsByCategory() {
            return string.Join("\n", Categories.Select(c => $"{c.Name}:\n{string.Join("\n", Products.Where(p => p.Category == c).Select(p => p.ToString()))}"));
        }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Inventory:");
            sb.AppendLine("Products:");
            foreach (Product product in Products) {
                sb.AppendLine("" + product.ToString());
            }
            sb.AppendLine("Categories:");
            foreach (Category category in Categories) {
                sb.AppendLine("" + category.ToString());
            }

            return sb.ToString();
        }

    }
}

