using Microsoft.AspNetCore.Mvc;
using SistemaVendas.DAL;
using SistemaVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class RelatorioController : Controller
    {
        protected ApplicationDbContext _applicationDbContext;
        public RelatorioController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Grafico()
        {
            var lista = _applicationDbContext.VendaProdutos
                  .GroupBy(x => x.CodigoProduto)
                  .Select(y => new GraficoViewModal
                  {
                      CodigoProduto = y.First().CodigoProduto,
                      Descricao = y.First().Produto.Descricao,
                      TotalVendido = y.Sum(z => z.Quantidade)
                  }).ToList();

            string valores = string.Empty;
            string labels = string.Empty;
            string cores = string.Empty;

            var random = new Random();
            for (int i = 0;  i < lista.Count; i++)
            {
                valores += lista[i].TotalVendido.ToString() + ",";
                labels += "'" + lista[i].Descricao.ToString() + "',";
                cores += "'" + string.Format("#{0:x6}", random.Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;

            return View();
        }
    }
}
