namespace GerenciadorProdutos.Entities {
    public class Category {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public List<Product> Products { get; private set; }
        public Category(string name, int id) {
            Name = name;
            Id = id;
            Products = new List<Product>();
        }

    }
}