using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaVendas.DAL;
using SistemaVendas.Entidades;
using SistemaVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class VendaController : Controller
    {
        protected ApplicationDbContext _applicationDbContext;
        public VendaController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Venda> lista = _applicationDbContext.Venda.ToList();
            //dispose é para liberar memória
            _applicationDbContext.Dispose();
            return View(lista);
        }

        private IEnumerable<SelectListItem> ListaProdutos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty,
            });

            foreach (var item in _applicationDbContext.Produto.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }

            return lista;
        }

        private IEnumerable<SelectListItem> ListaClientes()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty,
            });

            foreach (var item in _applicationDbContext.Cliente.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome.ToString()
                });
            }

            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            VendaViewModel viewModel = new VendaViewModel();
            viewModel.ListaClientes = ListaClientes();
            viewModel.ListaProdutos = ListaProdutos();

            if (id != null)
            {
                var entidade = _applicationDbContext.Venda.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Data = entidade.Data;
                viewModel.CodigoCliente = entidade.CodigoCliente;
                viewModel.Total = entidade.Total;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                Venda objVenda = new Venda()
                {
                    Codigo = entidade.Codigo,
                    Data = (DateTime)entidade.Data,
                    CodigoCliente = (int)entidade.CodigoCliente,
                    Total = entidade.Total,
                    Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(entidade.JsonProdutos)
                };
                if (entidade.Codigo == null)
                {
                    _applicationDbContext.Venda.Add(objVenda);
                }
                else
                {
                    _applicationDbContext.Entry(objVenda).State = EntityState.Modified;
                }
                _applicationDbContext.SaveChanges();
            }

            else
            {
                entidade.ListaClientes = ListaClientes();
                entidade.ListaProdutos = ListaProdutos();
                return View(entidade);
            }
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var ent = new Produto()
            {
                Codigo = id
            };
            _applicationDbContext.Attach(ent);
            _applicationDbContext.Remove(ent);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal LerValorProduto(int codigoProduto)
        {
            return _applicationDbContext.Produto.Where(x => x.Codigo == codigoProduto).Select(x => x.Valor).FirstOrDefault();
        }
    }
}
