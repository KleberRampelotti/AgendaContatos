using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Controllers
{
    public class AgendaController : Controller
    {        
        IHttpContextAccessor HttpContextAccessor;

        public AgendaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }      

        public IActionResult Index()
        {
            AgendaModel objAgenda = new AgendaModel(HttpContextAccessor); 
            ViewBag.ListaAgenda = objAgenda.ListaAgenda();
            return View();
        }

        [HttpPost]
        public IActionResult CriarContato(AgendaModel formulario)
        {
            if (ModelState.IsValid)
            {
                formulario.HttpContextAccessor = HttpContextAccessor;
                formulario.Insert();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CriarContato(int? id)
        {
            if (id != null)
            {
                AgendaModel objAgenda = new AgendaModel(HttpContextAccessor);
                ViewBag.registro = objAgenda.CarregarRegistro(id);
            }

            return View();
        }

        [HttpGet]
        public IActionResult ExcluirContato(int id)
        {
            AgendaModel objContato = new AgendaModel(HttpContextAccessor);
            objContato.Excluir(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult PesquisarContato(AgendaModel formulario)
        {
            formulario.HttpContextAccessor = HttpContextAccessor;
            ViewBag.ListaAgenda = formulario.ListaAgenda();            
            return View();
        }        
    }
}