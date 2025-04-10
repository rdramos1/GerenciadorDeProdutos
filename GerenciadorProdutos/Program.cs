using GerenciadorProdutos.Entities;

namespace GerenciadorProdutos {
    public class Program {
        public static void Main(string[] args) { 
            List<Category> categories = new List<Category>();
            List<Product> products = new List<Product>();

            Category category = new Category("Electronics", categories.Count);
            categories.Add(category);

            Product product = new Product("Smartphone", products.Count, 5, 800.00, categories[0]);
            products.Add(product);

            Console.WriteLine(product.ToString());

            try {
                product.ChangeQuantity(5);
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            
            Console.WriteLine("Say Hi");
            Console.ReadLine();
        }
    }
}








