using Loja.Mvc.Filters;
using Loja.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Controllers
{
    [Authorize]//O cara que não está logado não conseguirá acessar
    public class HomeController : Controller
    {
        [AllowAnonymous]//Liberar a tela abaixo para alguns
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]//Liberar a tela abaixo para alguns
        public ActionResult DefinirLinguagem(string linguagem)
        {
            Response.Cookies["LinguagemSelecionada"].Value = linguagem;

            return Redirect(Request.UrlReferrer.ToString());
        }

        [AuthorizeRole(Perfil.Administrador,Perfil.Comprador)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            /* Descobrir se tem o perfil
            if (!User.IsInRole(Perfil.Leiloeiro.ToString()))//Role são os perfis
            {
                return RedirectToAction("Login", "Acount", new { area = "" });  //Caminho completo na area de redirecionar
            }
            */

            if (!((ClaimsIdentity)User.Identity).HasClaim("Leilao","Cadastrar"))//Uma determinada claim
            {
                return RedirectToAction("Login", "Acount", new { area = "" });  //Caminho completo na area de redirecionar
            }



            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}