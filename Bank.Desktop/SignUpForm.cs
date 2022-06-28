using Bank.Database.Entities;
using Bank.Services.BankAccountService;
using Bank.Services.CardServices;
using Bank.Services.UserServices;

namespace Bank.Desktop
{
    public partial class SignUpForm : Form
    {
        private readonly IClientService _clientService;
        private readonly ICardService _cardService;
        private readonly IBankAccountService _bankAccountService;

        private readonly CardEntity _card;

        public SignUpForm(IClientService clientService,
            ICardService cardService,
            IBankAccountService bankAccountService)
        {
            _clientService = clientService;

            _cardService = cardService;
            _card = _cardService.Generate();

            _bankAccountService = bankAccountService;

            InitializeComponent();

            textBox1.Text = _card.CardNumber.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length != 4)
            {
                textBox2.Text = String.Empty;
                return;
            }

            short PIN;
            if (!short.TryParse(textBox2.Text, out PIN))
            {
                textBox2.Text = String.Empty;
                return;
            }

            var checkClient = _clientService.GetBy(textBox3.Text);
            if (checkClient != null)
            {
                textBox3.Text = string.Empty;
                return;
            }

            string userName = textBox3.Text;

            BankAccountEntity newAccount = _bankAccountService.GenerateAccount();
            _bankAccountService.Create(newAccount);

            _card.Account = newAccount;
            _card.AccountFK = newAccount.ID;
            _card.Password = PIN;
            _cardService.Create(_card);

            newAccount.Cards.Add(_card);
            _bankAccountService.Update(newAccount);

            ClientEntity clientEntity = new ClientEntity()
            {
                Account = newAccount,
                Name = userName,
            };
            _clientService.Create(clientEntity);

            this.Close();
        }
    }
}