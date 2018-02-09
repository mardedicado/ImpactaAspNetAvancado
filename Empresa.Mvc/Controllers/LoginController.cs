using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Mvc.ViewModels;
using Empresa.Repositorios.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class LoginController : Controller
    {

        private EmpresaDbContext _db;// = new EmpresaDbContext(); _db fild todo field começa com underline
        private IDataProtector _protectorProvider;

        public LoginController(EmpresaDbContext db,
            IDataProtectionProvider protectionProvider,
            IConfiguration configuracao) //Aqui o db é um parâmetro    //ctor tab tab. 
        {
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(configuracao.GetSection("ChaveCriptografia").Value);//o nome da sessão 

        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel viewModel)
        {

            var contato = _db.Contatos.Where(c => c.Email == viewModel.Email &&
            _protectorProvider.Unprotect(c.Senha) == viewModel.Senha).SingleOrDefault();

            if (contato == null )
            {
                //validação do modelo
                ModelState.AddModelError("", "Usuário ou Senha incorreto");

                return View(viewModel);//Para o campo e-mail aparecer digitado
            }

            return RedirectToAction("Index","Home");//Action e o Controller
        }
    }
}
