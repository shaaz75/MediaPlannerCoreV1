using MediaPlannerCore.Data.Repositories;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Service.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository ClientRepository;
        public ClientService(IClientRepository ClientRepository)
        {
            this.ClientRepository = ClientRepository;
        }

        public Client GetClientById(int? id)
        {
            return this.ClientRepository.GetByID(id);
        }

        public IEnumerable<Client> GetClients(string filter, string includeProperties)
        {
            IEnumerable<Client> clients = null;
            if (filter != null)
            {
                clients = this.ClientRepository.Get(s => s.ClientName.Contains(filter), includeProperties).AsEnumerable<Client>();
            }
            else
            {
                clients = this.ClientRepository.Get(null, includeProperties).AsEnumerable<Client>();

            }
            return clients;
        }
    }
    public interface IClientService
    {
        IEnumerable<Client> GetClients(string filter, string includeProperties);
        Client GetClientById(int? id);
    }
}
