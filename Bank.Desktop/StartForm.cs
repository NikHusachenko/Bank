using Bank.Database.Entities;
using Bank.Services.BankAccountService;
using Bank.Services.CardServices;
using Bank.Services.OrderServices;
using Bank.Services.UserServices;

namespace Bank.Desktop
{
    public partial class StartForm : Form
    {
        private readonly IClientService _clientService;
        private readonly ICardService _cardService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IOrderService _orderService;

        public StartForm(IClientService clientService,
            ICardService cardService,
            IBankAccountService bankAccountService,
            IOrderService orderService)
        {
            _clientService = clientService;
            _cardService = cardService;
            _bankAccountService = bankAccountService;
            _orderService = orderService;

            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ClientEntity clientEntity = _clientService.GetBy(textBox2.Text);
            if(clientEntity == null)
            {
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                return;
            }

            short PIN;
            if(!short.TryParse(textBox1.Text, out PIN))
            {
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                return;
            }

            CardEntity cardEntity = _cardService.GetBy(clientEntity, PIN);
            if(cardEntity == null)
            {
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                return;
            }

            AccountForm form = new AccountForm(_bankAccountService,
                _cardService,
                _clientService,
                _orderService,
                clientEntity,
                cardEntity);

            form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            SignUpForm form = new SignUpForm(_clientService,
                _cardService,
                _bankAccountService);
            form.ShowDialog();
        }
    }
}