using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Repositorios.SqlServer;
using Empresa.Dominio;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class ContatosController : Controller
    {
        private EmpresaDbContext _db;// = new EmpresaDbContext(); _db fild todo field começa com underline
        private IDataProtector _protectorProvider;

        public ContatosController(EmpresaDbContext db, 
            IDataProtectionProvider protectionProvider, 
            IConfiguration configuracao) //Aqui o db é um parâmetro    //ctor tab tab. 
        {
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(configuracao.GetSection("ChaveCriptografia").Value);//o nome da sessão 

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_db.Contatos.OrderBy(c => c.Nome).ToList());// Isto é uma lista. Tanto faz o nome c para contatos ou qualquer outra.
        }   //A view é vazia para que se possa digitar

       

        [Authorize(Roles ="Admin,Corretor")] //A listagem é Action. Nem olhar a tela de cadastro nem cadastrar. Aqui é ou
        public IActionResult Create()//para entrar na tela, um em branco //Outro create é no botão gravar
        {
            return View();
        }

        [HttpPost]//Como navegou o botão salvar vai fazer um post. 
        [Authorize(Roles = "Admin,Corretor")]

        /*
        [Authorize(Roles = "Admin")] Usando dessa forma, significa que sou as duas coisas
        [Authorize(Roles = "Corretor")]
        */

        public IActionResult Create(Contato contato)//para entrar na tela, um em branco //Outro create é no botão gravar
        {
            //Polimorfismo    

            var podeCriar = User.HasClaim("Contatos", "Criar");

            if (!podeCriar)
            {
                return RedirectToAction("AcessoNegado", "Login");
            }

            contato.Senha = _protectorProvider.Protect(contato.Senha);

            _db.Contatos.Add(contato);//é uma lista
            _db.SaveChanges();

            //return View(); //Não retorna para a tela
            return RedirectToAction("Index");
        }

    }
}
