using Microsoft.EntityFrameworkCore;
using Bank.Database.Entities;
using Bank.Database.GenericRepository;

namespace Bank.Services.CardServices
{
    public class CardService : ICardService
    {
        private readonly IGenericRepository<CardEntity> _cardRepository;

        public CardService(IGenericRepository<CardEntity> cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void Create(CardEntity cardEntity)
        {
            _cardRepository.Create(cardEntity);
            _cardRepository.SaveChanges();
        }

        public void Delete(CardEntity cardEntity)
        {
            _cardRepository.Remove(cardEntity);
            _cardRepository.SaveChanges();
        }

        public CardEntity Generate()
        {
            CardEntity cardEntity = new CardEntity();

            long cardNumber = GenerateCardNumber();
            while(GetBy(cardNumber) != null) 
            {
                cardNumber = GenerateCardNumber();
            }
            cardEntity.CardNumber = cardNumber;

            return cardEntity;
        }

        public long GenerateCardNumber()
        {
            byte firstDigit = (byte)(4 + new Random().Next(0, 2));

            byte[] cardNumber = new byte[16];
            for (byte i = 0; i < cardNumber.Length; i++)
                cardNumber[i] = (byte)(new Random().Next(0, 10));

            return long.Parse(firstDigit + string.Concat(cardNumber));
        }

        public List<CardEntity> GetAll()
        {
            var cards = _cardRepository.Table
                .Include(card => card.Account)
                .Include(card => card.Account.Client)
                .ToList();

            return cards;
        }

        public CardEntity GetBy(int id)
        {
            var cards = GetAll();
            var card = cards.Where(card => card.ID == id).FirstOrDefault();
            return card;
        }

        public CardEntity GetBy(long number)
        {
            var cards =  GetAll();
            var card = cards.Where(card => card.CardNumber == number).FirstOrDefault();
            return card;
        }

        public List<CardEntity> GetBy(ClientEntity client)
        {
            var allCards = GetAll();
            var cards = allCards.Where(card => card.Account.Client.ID == client.ID).ToList();
            return cards;
        }

        public List<CardEntity> GetBy(BankAccountEntity bankAccount)
        {
            var allCards = GetAll();
            var cards = allCards.Where(card => card.Account.ID == bankAccount.ID).ToList();
            return cards;
        }

        public CardEntity GetBy(ClientEntity client, short password)
        {
            var cards = GetBy(client);
            var card = cards.Where(card => card.Password == password).FirstOrDefault();
            return card;
        }

        public void Update(CardEntity cardEntity)
        {
            _cardRepository.Update(cardEntity);
            _cardRepository.SaveChanges();
        }

        public double SendMoney(CardEntity sender, long numberOfRecipient, double money)
        {
            sender.Balance -= money;

            CardEntity recipientCard = GetBy(numberOfRecipient);
            if(recipientCard != null)
            {
                recipientCard.Balance += money;
            }

            return sender.Balance;
        }

        public bool IsCardNumber(long number)
        {
            if (GetBy(number) != null)
                return true;

            if (number.ToString().Length == 16)
                return true;

            return false;
        }

        public double SendMoney(CardEntity sender, double money)
        {
            sender.Balance -= money;
            return sender.Balance;
        }
    }
}