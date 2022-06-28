using Microsoft.EntityFrameworkCore;
using Bank.Database.Entities;
using Bank.Database.GenericRepository;
using Bank.Services.CardServices;

namespace Bank.Services.BankAccountService
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IGenericRepository<BankAccountEntity> _bankAccountRepository;

        public BankAccountService(IGenericRepository<BankAccountEntity> genericRepository)
        {
            _bankAccountRepository = genericRepository;
        }

        public void Create(BankAccountEntity bankAccountEntity)
        {
            _bankAccountRepository.Create(bankAccountEntity);
            _bankAccountRepository.SaveChanges();
        }

        public void Delete(BankAccountEntity bankAccountEntity)
        {
            _bankAccountRepository.Remove(bankAccountEntity);
            _bankAccountRepository.SaveChanges();
        }

        public BankAccountEntity GenerateAccount()
        {
            long number = GenerateNumber();
            while (GetBy(number) != null)
            {
                number = GenerateNumber();
            }

            BankAccountEntity bankAccountEntity = new BankAccountEntity()
            {
                AccountNumber = number,
                Cards = new List<CardEntity>(),
            };

            return bankAccountEntity;
        }

        public long GenerateNumber()
        {
            byte[] digits = new byte[16];
            for (int i = 0; i < digits.Length; i++)
                digits[i] = (byte)(new Random().Next(0, 10));

            return long.Parse(String.Concat(digits));
        }

        public List<BankAccountEntity> GetAll()
        {
            var accounts = _bankAccountRepository.Table
                .Include(account => account.Client)
                .Include(account => account.Cards)
                .ToList();

            return accounts;
        }

        public BankAccountEntity GetBy(int id)
        {
            var account = _bankAccountRepository.Table
                .Where(account => account.ID == id)
                .Include(account => account.Client)
                .Include(account => account.Cards)
                .FirstOrDefault();

            return account;
        }

        public BankAccountEntity GetBy(long number)
        {
            var account = _bankAccountRepository.Table
                .Where(account => account.AccountNumber == number)
                .Include(account => account.Client)
                .Include(account => account.Cards)
                .FirstOrDefault();

            return account;
        }

        public BankAccountEntity GetBy(ClientEntity clientEntity)
        {
            var account = _bankAccountRepository.Table
                .Where(bank => bank.Client.ID == clientEntity.ID)
                .Include(bank => bank.Client)
                .Include(bank => bank.Cards)
                .FirstOrDefault();
            
            return account;
        }

        public void Update(BankAccountEntity bankAccountEntity)
        {
            _bankAccountRepository.Update(bankAccountEntity);
            _bankAccountRepository.SaveChanges();
        }
    }
}