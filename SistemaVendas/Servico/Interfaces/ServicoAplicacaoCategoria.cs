using Dominio.Interfaces;
using SistemaVendas.Dominio.Entidades;
using SistemaVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico.Interfaces
{
    public class ServicoAplicacaoCategoria : IServicoAplicacaoCategoria
    {
        private readonly IServicoCategoria ServicoCategoria;

        public ServicoAplicacaoCategoria(IServicoCategoria servicoCategoria)
        {
            ServicoCategoria = servicoCategoria;
        }

        public void Cadastrar(CategoriaViewModel categoria)
        {
            Categoria item = new Categoria()
            {
                Codigo = categoria.Codigo,
                Descricao = categoria.Descricao
            };
        }

        public CategoriaViewModel CarregarRegistro(int codigoCategoria)
        {
            var registro = ServicoCategoria.CarregarRegistro(codigoCategoria);

            CategoriaViewModel categoria = new CategoriaViewModel()
            {
                Codigo = registro.Codigo,
                Descricao = registro.Descricao
            };
            return categoria;
        }

        public void Excluir(int id)
        {
            ServicoCategoria.Excluir(id);
        }

        public IEnumerable<CategoriaViewModel> Listagem()
        {
            var lista = ServicoCategoria.Listagem();
            List<CategoriaViewModel> listaCategoria = new List<CategoriaViewModel> ();

            foreach(var item in lista)
            {
                CategoriaViewModel categoria = new CategoriaViewModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                };
                listaCategoria.Add(categoria);
            }
            return listaCategoria;
        }
    }
}
