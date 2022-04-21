using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Entidades
{
    public class VendaProdutos
    {
        [Key]
        public int CodigoVenda { get; set; }
        public int CodigoProduto { get; set; }
        public double Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }

    }
}
