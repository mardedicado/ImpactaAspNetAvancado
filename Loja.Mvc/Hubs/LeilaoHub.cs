using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Loja.Mvc.Hubs
{
    public class LeilaoHub : Hub
    {
        public async Task Participar(string nomeParticipante, string produtoId) //O Async aparece porque foi utilizado o await abaixo 
        {   
            //ConnectionId é um guid, o qual foi gerado 
            await Groups.Add(Context.ConnectionId,produtoId);//O grupo foi criado e tem o nome de produto "1"
            //Ctrl K D para identar
            //Se é para escrever na tela é JavaScript
            Clients.Group(produtoId).adicionarMensagem(nomeParticipante, Context.ConnectionId,"Bom leilão a todos!");

        }
    }
}