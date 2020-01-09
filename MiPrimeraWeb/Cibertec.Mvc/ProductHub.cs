using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Cibertec.Mvc
{
    public class ProductHub:Hub
    {
        static List<string> productIds = new List<string>();

        public void AddProductId(string id) //SE AGREGA CUANDO SE CONECTA UN USUARIO
        {
            if (!productIds.Contains(id)) productIds.Add(id);
            Clients.All.productStatus(productIds);
        }

        public void RemoveProductId(string id) //LIBERA LA TRANSACCION CUANDO EL USUARIO SE DESCONECTA
        {
            if (productIds.Contains(id)) productIds.Remove(id);
            Clients.All.productStatus(productIds);
        }

        public override Task OnConnected()//VEMOS CUALES IDS ESTAN CONECTADOS
        {
            return Clients.All.productStatus(productIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message); //DESDE EL SERVIDOR SE COMUNICA AL CLIENTE HACE LA FUNCION DEL PROXY
        }
    }
}