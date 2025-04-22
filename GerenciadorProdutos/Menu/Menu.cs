using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorProdutos.Entities;
using GerenciadorProdutos.Service;

namespace GerenciadorProdutos.Menu {
    class Menu {

        private static InventoryService inventoryService = new InventoryService();
        private static SaleService saleService = new SaleService();

        public static void ShowPrincipalMenu() {
            while (true) {
                Console.WriteLine("<=====================================>");
                Console.WriteLine("Welcome to the Product Manager!");
                Console.WriteLine("1 - Sales options");
                Console.WriteLine("2 - List Product");
                Console.WriteLine("3 - Manage inventory");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("<=====================================>");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();
                Console.Clear();
                switch (option) {
                    case "1":
                        SaleOptions();
                        break;
                    case "2":
                        ListProducts();
                        PressEnterToContinue();
                        break;
                    case "3":
                        ManageInventory();
                        break;
                    case "0":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Invalid option, please try again.");
                        Console.ResetColor();
                        PressEnterToContinue();
                        break;
                }
            }
        }

        public static void SaleOptions() {
            while (true) {
                Console.Clear();
                Console.WriteLine("<=====================================>");
                Console.WriteLine("Sales Options");
                Console.WriteLine("1 - New Sale");
                Console.WriteLine("2 - List Sales");
                Console.WriteLine("3 - Undo sale");
                Console.WriteLine("0 - Back to Main Menu");
                Console.WriteLine("<=====================================>");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();
                Console.Clear();
                switch (option) {
                    case "1":
                        NewSale();
                        PressEnterToContinue();
                        break;
                    case "2":
                        saleService.ListSales();
                        PressEnterToContinue();
                        break;
                    case "3":
                        UndoSale();
                        PressEnterToContinue();
                        break;
                    case "0":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Invalid option, please try again.");
                        Console.ResetColor();
                        PressEnterToContinue();
                        break;
                }
            }

        }
        public static void ListProducts() {
            Console.WriteLine(inventoryService.GetProductsByCategory());
        }
        public static void NewSale() {
            Console.WriteLine("What product do you want to sell?");
            int productId = GetProductId();
            Product product = inventoryService.Products.FirstOrDefault(p => p.Id == productId);
            Console.WriteLine("Product found: " + product.Name);
            Console.WriteLine("Product ID: " + product.Id);
            Console.WriteLine("Product Quantity: " + product.Quantity);
            Console.WriteLine("Product Price: " + product.Price.ToString("N"));

            Console.WriteLine("How many do you want to sell?");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity)) {
                Console.Write("Invalid input. Please enter a valid quantity: ");
            }
            double price = product.Price * quantity;
            Console.WriteLine("You are selling " + quantity + " of " + product.Name + "for te price: " + price.ToString("N"));
            Console.WriteLine("Are you sure? (y/n)");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() != "y") {
                Console.WriteLine("Sale cancelled.");
                return;
            }
            saleService.RegisterSale(product, quantity);
            inventoryService.SellProduct(productId, quantity);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sale registered successfully!");
            Console.ResetColor();


        }
        public static void UndoSale() {
            Console.WriteLine("What sale do you want to undo?");
            int saleId = GetSaleId();

            Sale sale = saleService.Sales.FirstOrDefault(s => s.Id == saleId);

            Console.WriteLine("You are undoing the sale of " + sale.Product.Name + " for the price: " + (sale.Product.Price * sale.Quantity).ToString("N"));
            Console.WriteLine("Are you sure? (y/n)");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() != "y") {
                Console.WriteLine("Sale undo cancelled.");
                return;
            }

            inventoryService.RestockProduct(sale.Product.Id, sale.Quantity);
            saleService.DeleteSale(sale);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sale undone successfully!");
            Console.ResetColor();
        }

        public static void ManageInventory() {
            while (true) {
                Console.Clear();
                Console.WriteLine("<=====================================>");
                Console.WriteLine("Manage Inventory");
                Console.WriteLine("1 - Add Product");
                Console.WriteLine("2 - Update Product");
                Console.WriteLine("3 - Delete Product");
                Console.WriteLine("0 - Back to Main Menu");
                Console.WriteLine("<=====================================>");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();
                Console.Clear();
                switch (option) {
                    case "1":
                        AddProduct();
                        PressEnterToContinue();
                        break;
                    case "2":
                        UpdateProduct();
                        PressEnterToContinue();
                        break;
                    case "3":
                        DeleteProduct();
                        PressEnterToContinue();
                        break;
                    case "0":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Invalid option, please try again.");
                        Console.ResetColor();
                        PressEnterToContinue();
                        break;
                }
            }
        }

        public static void AddProduct() {
            Console.WriteLine("Add Product");
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter product quantity: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity)) {
                Console.Write("Invalid input. Please enter a valid quantity: ");
            }
            Console.Write("Enter product price: ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price)) {
                Console.Write("Invalid input. Please enter a valid price: ");
            }

            int categoryId = GetCategoryId();

            inventoryService.AddProductByCategoryId(name, quantity, price, categoryId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product added successfully!");
            Console.ResetColor();
        }

        public static void UpdateProduct() {
            Console.WriteLine("What product do you want to change?");
            int productId = GetProductId();
            Console.Clear();

            Product product = inventoryService.Products.FirstOrDefault(p => p.Id == productId);

            Console.WriteLine("What do you want to change?");
            Console.WriteLine("1 - Name");
            Console.WriteLine("2 - Quantity");
            Console.WriteLine("3 - Price");
            Console.WriteLine("4 - Category");
            Console.WriteLine("0 - Back to Manage Inventory");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();
            Console.Clear();
            switch (option) {
                case "1":
                    Console.Write("Enter new name: ");
                    string newName = Console.ReadLine();
                    inventoryService.UpdateProduct(productId, newName, product.Quantity, product.Price, product.Category);
                    break;
                case "2":
                    Console.Write("Enter new quantity: ");
                    int newQuantity;
                    while (!int.TryParse(Console.ReadLine(), out newQuantity)) {
                        Console.Write("Invalid input. Please enter a valid quantity: ");
                    }
                    inventoryService.UpdateProduct(productId, product.Name, newQuantity, product.Price, product.Category);
                    break;
                case "3":
                    Console.Write("Enter new price: ");
                    double newPrice;
                    while (!double.TryParse(Console.ReadLine(), out newPrice)) {
                        Console.Write("Invalid input. Please enter a valid price: ");
                    }
                    inventoryService.UpdateProduct(productId, product.Name, product.Quantity, newPrice, product.Category);
                    break;
                case "4":
                    int categoryId = GetCategoryId();
                    Category category = inventoryService.Categories.FirstOrDefault(c => c.Id == categoryId);
                    inventoryService.UpdateProduct(productId, product.Name, product.Quantity, product.Price, category);
                    break;
                case "0":
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Invalid option, please try again.");
                    Console.ResetColor();
                    PressEnterToContinue();
                    break;
            }
        }

        public static void DeleteProduct() {
            Console.WriteLine("What product do you want to delete?");
            int productId = GetProductId();
            Product product = inventoryService.Products.FirstOrDefault(p => p.Id == productId);
            Console.WriteLine("You are deleting the product: " + product.Name);
            Console.WriteLine("Are you sure? (y/n)");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() != "y") {
                Console.WriteLine("Product deletion cancelled.");
                return;
            }
            inventoryService.Products.Remove(product);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product deleted successfully!");
            Console.ResetColor();
        }

        public static int GetCategoryId() {
            int categoryId;
            while (true) {
                Console.Write("Enter category ID (or press Enter to list all categories): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) {
                    Console.WriteLine("Available Categories:");
                    Console.WriteLine(inventoryService.GetCategories());
                }
                else if (int.TryParse(input, out categoryId)) {
                    if (inventoryService.Categories.Any(c => c.Id == categoryId)) {
                        return categoryId;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid category ID. Please try again.");
                        Console.ResetColor();
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid category ID.");
                    Console.ResetColor();
                }
            }
        }
        public static int GetProductId() {
            int productId;
            while (true) {
                Console.Write("Enter product ID (or press Enter to list all products): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) {
                    Console.WriteLine("Available Products:");
                    Console.WriteLine(inventoryService.GetProducts());
                }
                else if (int.TryParse(input, out productId)) {
                    if (inventoryService.Products.Any(p => p.Id == productId)) {
                        return productId;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid product ID. Please try again.");
                        Console.ResetColor();
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid product ID.");
                    Console.ResetColor();
                }
            }
        }
        public static int GetSaleId() {
            int saleId;
            while (true) {
                Console.Write("Enter sale ID (or press Enter to list all sales): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) {
                    Console.WriteLine("Available Sales:");
                    saleService.ListSales();
                }
                else if (int.TryParse(input, out saleId)) {
                    if (saleService.Sales.Any(s => s.Id == saleId)) {
                        return saleId;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid sale ID. Please try again.");
                        Console.ResetColor();
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid sale ID.");
                    Console.ResetColor();
                }
            }
        }
        public static void PressEnterToContinue() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press Enter to continue...");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

    }
}
