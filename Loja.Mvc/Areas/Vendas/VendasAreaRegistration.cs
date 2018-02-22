using System.Web.Http;
using System.Web.Mvc;

namespace Loja.Mvc.Areas.Vendas
{
    public class VendasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Vendas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //Já está registrado, mas está como...
            //Agora será como api
            //Id não é obrigatório
            //Se fossem 10 areas, todas constariam aqui
            context.Routes.MapHttpRoute(
                name: "VendasDefaultApi",
                routeTemplate: "api/Vendas/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            context.MapRoute(
                "Vendas_default",
                "Vendas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            //Será criada uma controller com action, mas retornará informação em vez de tela.
        }
    }
}