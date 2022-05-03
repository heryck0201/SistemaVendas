using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.DAL;
using SistemaVendas.Helpers;
using SistemaVendas.Models;
using System;
using System.Linq;

namespace SistemaVendas.Controllers
{
    public class LoginController : Controller
    {
        protected ApplicationDbContext _applicationDbContext;
        protected IHttpContextAccessor _ihttpContextAccessor;
        public LoginController(ApplicationDbContext applicationDbContext, IHttpContextAccessor ihttpContextAccessor)
        {
            _ihttpContextAccessor = ihttpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                if (id == 0)
                {
                    _ihttpContextAccessor.HttpContext.Session.Clear();
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            ViewData["ErroLogin"] = string.Empty;
            if (ModelState.IsValid)
            {
                var usuario = _applicationDbContext.Usuario.Where(x => x.Email == model.Email && x.Senha == model.Senha).FirstOrDefault();
                
                if (usuario == null)
                {
                    ViewData["ErroLogin"] = "O Email ou senha informado não esta no sistema";
                    return View(model);
                }
                else
                {
                    _ihttpContextAccessor.HttpContext.Session.SetString(Sessao.NOME_USUARIO, usuario.Nome);
                    _ihttpContextAccessor.HttpContext.Session.SetString(Sessao.EMAIL_USUARIO, usuario.Email);
                    _ihttpContextAccessor.HttpContext.Session.SetInt32(Sessao.CODIGO_USUARIO, (int)usuario.Codigo);
                    _ihttpContextAccessor.HttpContext.Session.SetInt32(Sessao.LOGADO, 1);

                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                return View(model);
            }
            
        }
    }
}
