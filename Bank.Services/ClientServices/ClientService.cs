using Microsoft.EntityFrameworkCore;
using Bank.Database.Entities;
using Bank.Database.GenericRepository;
using Bank.Services.BankAccountService;

namespace Bank.Services.UserServices
{
    public class ClientService : IClientService
    {
        private readonly IGenericRepository<ClientEntity> _clientRepository;

        public ClientService(IGenericRepository<ClientEntity> genericRepository)
        {
            _clientRepository = genericRepository;
        }

        public void Create(ClientEntity clientEntity)
        {
            _clientRepository.Create(clientEntity);
            _clientRepository.SaveChanges();
        }

        public void Delete(ClientEntity clientEntity)
        {
            _clientRepository.Remove(clientEntity);
            _clientRepository.SaveChanges();
        }

        public List<ClientEntity> GetAll()
        {
            var clients = _clientRepository.Table
                .Include(client => client.Account)
                .Include(client => client.Account.Cards)
                .ToList();

            return clients;
        }

        public ClientEntity GetBy(int id)
        {
            var client = _clientRepository.Table
                .Where(client => client.ID == id)
                .Include(client => client.Account)
                .Include(client => client.Account.Cards)
                .FirstOrDefault();

            return client;
        }

        public ClientEntity GetBy(string name)
        {
            var client = _clientRepository.Table
                .Where(client => client.Name == name)
                .Include(client => client.Account)
                .Include(client => client.Account.Cards)
                .FirstOrDefault();

            return client;
        }

        public ClientEntity GetBy(BankAccountEntity bankAccount)
        {
            var clients = _clientRepository.Table
                .Include(client => client.Account);

            var client = clients.Where(client => client.Account.ID == bankAccount.ID).FirstOrDefault();
            return client;
        }

        public ClientEntity GetBy(CardEntity cardEntity)
        {
            var clients = _clientRepository.Table
                .Include(client => client.Account)
                .Include(client => client.Account.Cards)
                .ToList();

            var client = clients.Where(client => client.Account.Cards.Contains(cardEntity)).FirstOrDefault();
            return client;
        }

        public void Update(ClientEntity clientEntity)
        {
            _clientRepository.Update(clientEntity);
            _clientRepository.SaveChanges();
        }
    }
}