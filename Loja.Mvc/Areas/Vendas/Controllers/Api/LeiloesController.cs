using Loja.Mvc.Helpers;
using Loja.Repositorios.SqlServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loja.Mvc.Areas.Vendas.Controllers.Api
{
    public class LeiloesController : ApiController
    {
        private LojaDbContext _db = new LojaDbContext();
        //Será utilizado para
        public IHttpActionResult Get()
        {
            return Ok(Mapeamento.Mapear(_db.Produtos.Where(p => p.EmLeilao).ToList())); //Eu não sei quantos são... por isso uma lista

        }
    }
}
