using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorProdutos.Exceptions {
    public class SystemException : Exception{
        public SystemException(string message) : base(message) {

        }
        public override string ToString() {
            return $"ProductException: {Message}";
        }
    }
}
