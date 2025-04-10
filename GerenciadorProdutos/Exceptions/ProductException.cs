namespace GerenciadorProdutos.Exceptions {
    public class ProductException : Exception {
        public ProductException(string message) : base(message) {

        }
        public override string ToString() {
            return $"ProductException: {Message}";
        }
    }
}
