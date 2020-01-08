using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;


namespace Cibertec.Mvc
{
    public class SupplierHub:Hub
    {
        static List<string> supplierIds = new List<string>();

        public void AddSupplierId(string id) //SE AGREGA CUANDO SE CONECTA UN USUARIO
        {
            if (!supplierIds.Contains(id)) supplierIds.Add(id);
            Clients.All.supplierStatus(supplierIds);
        }

        public void RemoveSupplierId(string id) //LIBERA LA TRANSACCION CUANDO EL USUARIO SE DESCONECTA
        {
            if (supplierIds.Contains(id)) supplierIds.Remove(id);
            Clients.All.supplierStatus(supplierIds);
        }

        public override Task OnConnected()//VEMOS CUALES IDS ESTAN CONECTADOS
        {
            return Clients.All.supplierStatus(supplierIds);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message); //DESDE EL SERVIDOR SE COMUNICA AL CLIENTE HACE LA FUNCION DEL PROXY
        }
    }

}