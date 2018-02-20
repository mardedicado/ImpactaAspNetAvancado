using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Loja.Mvc.Helpers
{
    public class CulturaHelper
    {
        private const string LinguagemPadrao = "pt-BR";
        private string _linguagemSelecionada = LinguagemPadrao; //Sempre que estiver vazia, a liguagem padrão será Br

        public CulturaHelper()
        {
            DefinirCulturaPadrao();
            ObterRegionInfo();
        }

        private void ObterRegionInfo()
        {
            var cultura = CultureInfo.CreateSpecificCulture(_linguagemSelecionada);
            var regiao = new RegionInfo(cultura.LCID);

            Abreviacao = regiao.TwoLetterISORegionName.ToLower();
            NomeNativo = regiao.NativeName;
        }

        //Get significa apenas leitura
        private List<string> LinguagensSuportadas { get;} = new List<string> {"pt-BR", "en-US", "es"};

        public static CultureInfo ObterCultureInfo()
        {
            var linguagemSelecionada = HttpContext.Current.Request.Cookies["LinguagemSelecionada"];
            var linguagem = linguagemSelecionada?.Value ?? LinguagemPadrao;
            //Pergunta se não está na máquina é nulo
            //??Pergunta se o primerio parametro é nulo. Como if null

            return CultureInfo.CreateSpecificCulture(linguagem);
        }
                
        public string Abreviacao { get; set; }
        public string NomeNativo { get; set; }

        private void DefinirCulturaPadrao()
        {
            var request = HttpContext.Current.Request;

            //Se já um cookie, retorna com a liguagem
            if (request.Cookies["LinguagemSelecionada"]!=null)
            {
                _linguagemSelecionada = request.Cookies["LinguagemSelecionada"].Value;
                return;
            }
            if (request.UserLanguages !=null &&
                LinguagensSuportadas.Contains(request.UserLanguages[0]))
            {
                _linguagemSelecionada = request.UserLanguages[0];
            }

            var cookie = new HttpCookie("LinguagemSelecionada", _linguagemSelecionada);
            cookie.Expires = DateTime.MaxValue;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}