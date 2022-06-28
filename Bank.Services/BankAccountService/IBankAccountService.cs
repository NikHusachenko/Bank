using Bank.Database.Entities;

namespace Bank.Services.BankAccountService
{
    public interface IBankAccountService
    {
        public void Create(BankAccountEntity bankAccountEntity);

        public void Update(BankAccountEntity bankAccountEntity);

        public void Delete(BankAccountEntity bankAccountEntity);

        public long GenerateNumber();

        public BankAccountEntity GenerateAccount();

        public List<BankAccountEntity> GetAll();

        public BankAccountEntity GetBy(int id);

        public BankAccountEntity GetBy(long number);

        public BankAccountEntity GetBy(ClientEntity clientEntity);
    }
}