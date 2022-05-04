﻿using SistemaVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico.Interfaces
{
   public interface IServicoAplicacaoCategoria
    {
        IEnumerable<CategoriaViewModel> Listagem();
        CategoriaViewModel CarregarRegistro(int codigoCategoria);
        void Cadastrar(CategoriaViewModel categoria);
        void Excluir(int id);
    }
}
