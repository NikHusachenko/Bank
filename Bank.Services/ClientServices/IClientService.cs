using Bank.Database.Entities;

namespace Bank.Services.UserServices
{
    public interface IClientService
    {
        public void Create(ClientEntity clientEntity);

        public void Update(ClientEntity clientEntity);

        public void Delete(ClientEntity clientEntity);

        public List<ClientEntity> GetAll();

        public ClientEntity GetBy(int id);

        public ClientEntity GetBy(string name);

        public ClientEntity GetBy(BankAccountEntity bankAccount);

        public ClientEntity GetBy(CardEntity cardEntity);
    }
}