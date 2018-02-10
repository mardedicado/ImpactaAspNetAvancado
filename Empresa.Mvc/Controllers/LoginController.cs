using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Mvc.ViewModels;
using Empresa.Repositorios.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class LoginController : Controller
    {

        private EmpresaDbContext _db;// = new EmpresaDbContext(); _db fild todo field começa com underline
        private IDataProtector _protectorProvider;
        private string _tipoAutenticacao; //É um field

        public LoginController(EmpresaDbContext db,
            IDataProtectionProvider protectionProvider,
            IConfiguration configuracao) //Aqui o db é um parâmetro    //ctor tab tab. 
        {
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(configuracao.GetSection("ChaveCriptografia").Value);//o nome da sessão 
            _tipoAutenticacao = configuracao.GetSection("TipoAutenticacao").Value;//Agora estamos expondo o texto "Value"
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)//O Model é LoginViewModel. Por causa dos atributos o Java replicou lá.
            {
                return View(viewModel);
            }


            var contato = _db.Contatos.Where(c => c.Email == viewModel.Email &&
            _protectorProvider.Unprotect(c.Senha) == viewModel.Senha).SingleOrDefault();

            if (contato == null)
            {
                //validação do modelo
                ModelState.AddModelError("", "Usuário ou Senha incorreto");

                return View(viewModel);//Para o campo e-mail aparecer digitado
            }
            //Claims é um objeto. Tudo isso para cookie
            var claims = new List<Claim> //uma lista
            {      //ctrl c v
                new Claim(ClaimTypes.Name,contato.Nome),
                new Claim(ClaimTypes.Email,contato.Email),

                //new Claim(ClaimTypes.Role, "Admin"),//Perfil apenas para teste depoiis volart
                new Claim(ClaimTypes.Role, "Vendedor"),//Role - Perfil
                new Claim("Contato", "Criar")
            };  //Depois que está logado essas informações aparecem

            var identidade = new ClaimsIdentity(claims, _tipoAutenticacao);//As permissões são as claims
            var principal = new ClaimsPrincipal(identidade);//qUAL DEles é o principal. Tudo o que fizemos é para chegar na 3ª linha
            //Http context o que necessário estar disponível para eu fazer a autenticação

            HttpContext.Authentication.SignInAsync(_tipoAutenticacao, principal);

            return RedirectToAction("Index", "Home");
            //Action e o Controller
            }
            
            public IActionResult Logout()
            {
                 HttpContext.Authentication.SignOutAsync(_tipoAutenticacao);
                 return RedirectToAction("Index", "Home");
            }

        public IActionResult AcessoNegado()
        {
            return View();
        }
        
    }
}
