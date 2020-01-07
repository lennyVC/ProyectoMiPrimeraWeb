using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cibertec.Mvc
{
    public class CustomerHub:Hub
    {
        static List<string> customerIds = new List<string>();

        public void AddCustomerId(string id) //SE AGREGA CUANDO SE CONECTA UN USUARIO
        {
            if (!customerIds.Contains(id)) customerIds.Add(id);
            Clients.All.customerStatus(customerIds);
        }

        public void RemoveCustomerId(string id) //LIBERA LA TRANSACCION CUANDO EL USUARIO SE DESCONECTA
        {
            if (customerIds.Contains(id)) customerIds.Remove(id);
            Clients.All.customerStatus(customerIds);
        }

        public override Task OnConnected()//VEMOS CUALES IDS ESTAN CONECTADOS
        {
            return Clients.All.customerStatus(customerIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message); //DESDE EL SERVIDOR SE COMUNICA AL CLIENTE HACE LA FUNCION DEL PROXY
        }
    }
}