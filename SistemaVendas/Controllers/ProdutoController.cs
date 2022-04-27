using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.DAL;
using SistemaVendas.Entidades;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVendas.Controllers
{
    public class ProdutoController : Controller
    {
        protected ApplicationDbContext _applicationDbContext;
        public ProdutoController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Produto> lista = _applicationDbContext.Produto.Include(x=>x.Categoria).ToList();
            //dispose é para liberar memória
            _applicationDbContext.Dispose();
            return View(lista);
        }

        private IEnumerable<SelectListItem> ListaCategoria()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty,
            });

            foreach (var item in _applicationDbContext.Categoria.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }

            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel();
            viewModel.ListaCategorias = ListaCategoria();

            if (id != null)
            {
                var entidade = _applicationDbContext.Produto.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;
                viewModel.Quantidade = entidade.Quantidade;
                viewModel.Valor = entidade.Valor;
                viewModel.CodigoCategoria = entidade.CodigoCategoria;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                Produto objProduto = new Produto()
                {
                    Codigo = produtoViewModel.Codigo,
                    Descricao = produtoViewModel.Descricao,
                    Quantidade = produtoViewModel.Quantidade,
                    Valor = (decimal)produtoViewModel.Valor,
                    CodigoCategoria = (int)produtoViewModel.CodigoCategoria
                };
                if (produtoViewModel.Codigo == null)
                {
                    _applicationDbContext.Produto.Add(objProduto);
                }
                else
                {
                    _applicationDbContext.Entry(objProduto).State = EntityState.Modified;
                }
                _applicationDbContext.SaveChanges();
            }

            else
            {
                produtoViewModel.ListaCategorias = ListaCategoria();
                return View(produtoViewModel);
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
    }
}
