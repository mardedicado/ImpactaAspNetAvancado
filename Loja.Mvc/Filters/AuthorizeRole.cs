using Loja.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Filters
{
    //As permissões estão no numerador. Qual é a profissão? Leiloeiro
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params Perfil[] perfis)    //Params associado com vetor. Coleção de parâmetros, ou seja, uma lista.
        {                                               //Para vetor, é necessário instanciar. "Params" desobriga que seja instanciado.
            foreach (var perfil in perfis)
            {
                Roles += perfil + ",";
            }

            //Roles = string.Join(",", perfis.Select(p => Enum.GetName(p.GetType(), p)));
        }
    }
}