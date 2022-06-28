using Bank.Database.Entities;

namespace Bank.Services.CardServices
{
    public interface ICardService
    {
        public void Create(CardEntity cardEntity);

        public void Update(CardEntity cardEntity);

        public void Delete(CardEntity cardEntity);

        public List<CardEntity> GetAll();

        public CardEntity GetBy(int id);

        public CardEntity GetBy(long number);

        public List<CardEntity> GetBy(ClientEntity client);

        public CardEntity GetBy(ClientEntity client, short password);

        public List<CardEntity> GetBy(BankAccountEntity bankAccount);

        public CardEntity Generate();

        public long GenerateCardNumber();

        public double SendMoney(CardEntity sender, long numberOfRecipient, double money);

        public double SendMoney(CardEntity sender, double money);

        public bool IsCardNumber(long number);
    }
}