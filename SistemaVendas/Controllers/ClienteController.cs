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
    public class ClienteController : Controller
    {
        protected ApplicationDbContext _applicationDbContext;
        public ClienteController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Cliente> lista = _applicationDbContext.Cliente.ToList();
            //dispose é para liberar memória
            _applicationDbContext.Dispose();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ClienteViewModel viewModel = new ClienteViewModel();

            if (id != null)
            {
                var entidade = _applicationDbContext.Cliente.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Nome = entidade.Nome;
                viewModel.CNPJ_CPF = entidade.CNPJ_CPF;
                viewModel.Email = entidade.Email;
                viewModel.Celular = entidade.Celular;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                Cliente objCliente = new Cliente()
                {
                    Codigo = clienteViewModel.Codigo,
                    Nome = clienteViewModel.Nome,
                    CNPJ_CPF = clienteViewModel.CNPJ_CPF,
                    Email = clienteViewModel.Email,
                    Celular = clienteViewModel.Celular,
                };
                if (clienteViewModel.Codigo == null)
                {
                    _applicationDbContext.Cliente.Add(objCliente);
                }
                else
                {
                    _applicationDbContext.Entry(objCliente).State = EntityState.Modified;
                }
                _applicationDbContext.SaveChanges();
            }

            else
            {
                return View(clienteViewModel);
            }
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var ent = new Cliente()
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
