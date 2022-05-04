﻿using Dominio.Interfaces;
using Dominio.Repositorio;
using SistemaVendas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servivos.Categorias
{
    public class ServicoCategoria : IServicoCategoria
    {
        IRepositorioCategoria RepositorioCategoria;
        public ServicoCategoria(IRepositorioCategoria repositorioCategoria)
        {
            RepositorioCategoria = repositorioCategoria;
        }
        public void Cadastrar(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Categoria CarregarRegistro(int id)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> Listagem()
        {
            return RepositorioCategoria.Read();
        }
    }
}