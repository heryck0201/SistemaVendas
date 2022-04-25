using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.DAL;
using SistemaVendas.Entidades;
using SistemaVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class CategoriaController : Controller
    {
        protected ApplicationDbContext _applicationDbContext;
        public CategoriaController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Categoria> lista = _applicationDbContext.Categoria.ToList();
            //dispose é para liberar memória
            _applicationDbContext.Dispose();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            CategoriaViewModel viewModel = new CategoriaViewModel();

            if (id != null)
            {
                var entidade = _applicationDbContext.Categoria.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel categoriaViewModel)
        {
            if (ModelState.IsValid)
            {
                Categoria objCategoria = new Categoria()
                {
                    Codigo = categoriaViewModel.Codigo ,
                    Descricao = categoriaViewModel.Descricao
                };
                if (categoriaViewModel.Descricao == null)
                {
                    _applicationDbContext.Categoria.Add(objCategoria);
                }
                else
                {
                    _applicationDbContext.Entry(objCategoria).State = EntityState.Modified;
                }
                _applicationDbContext.SaveChanges();
            }

            else
            {
                return View(categoriaViewModel);
            }
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var ent = new Categoria()
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
