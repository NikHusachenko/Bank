using Bank.Database;
using Bank.Database.Entities;
using Bank.Database.GenericRepository;
using Bank.Services.BankAccountService;
using Bank.Services.CardServices;
using Bank.Services.OrderServices;
using Bank.Services.UserServices;

namespace Bank.Desktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            IGenericRepository<ClientEntity> clientRepository = new GenericRepository<ClientEntity>(applicationDbContext);
            IGenericRepository<CardEntity> cardRepository = new GenericRepository<CardEntity>(applicationDbContext);
            IGenericRepository<BankAccountEntity> accountRepository = new GenericRepository<BankAccountEntity>(applicationDbContext);

            ICardService cardService = new CardService(cardRepository);
            IBankAccountService bankAccountService = new BankAccountService(accountRepository);
            IClientService clientService = new ClientService(clientRepository);
            IOrderService orderService = new OrderService(cardService);

            ApplicationConfiguration.Initialize();
            Application.Run(new StartForm(clientService,
                cardService,
                bankAccountService,
                orderService));
        }
    }
}