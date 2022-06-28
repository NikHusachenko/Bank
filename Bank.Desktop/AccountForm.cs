using Bank.Database.Entities;
using Bank.Services.BankAccountService;
using Bank.Services.CardServices;
using Bank.Services.OrderServices;
using Bank.Services.UserServices;

namespace Bank.Desktop
{
    public partial class AccountForm : Form
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ICardService _cardService;
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;

        private readonly ClientEntity _currentUser;
        private CardEntity _currentCard;

        public AccountForm(IBankAccountService bankAccountService,
            ICardService cardService,
            IClientService clientService,
            IOrderService orderService,
            ClientEntity clientEntity,
            CardEntity cardEntity)
        {
            _bankAccountService = bankAccountService;
            _cardService = cardService;
            _clientService = clientService;
            _orderService = orderService;

            _currentUser = clientEntity;
            _currentCard = cardEntity;

            InitializeComponent();

            label1.Text = _currentUser.Name;
            label2.Text = _currentCard.Balance.ToString();

            foreach (var card in _currentUser.Account.Cards)
                comboBox1.Items.Add(card.CardNumber);

            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(_currentCard.CardNumber);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            long cardNumber = long.Parse(comboBox1.SelectedItem.ToString());
            CardEntity card = _cardService.GetBy(cardNumber);

            if(card == null)
            {
                comboBox1.SelectedItem = _currentCard.CardNumber;
                return;
            }

            _currentCard = card;
            label2.Text = _currentCard.Balance.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PayOrderForm form = new PayOrderForm(_cardService,
                _orderService,
                _currentCard);
            
            if(_currentCard.IsBlock)
            {
                MessageBox.Show("This card is blocked");
                return;
            }
            if(_currentCard == null)
            {
                MessageBox.Show("No one card selected");
                return;
            }

            form.ShowDialog();
            label2.Text = _currentCard.Balance.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMoneyForm form = new SendMoneyForm(_cardService,
                _currentUser,
                _currentCard);
            
            if(_currentCard.IsBlock)
            {
                MessageBox.Show("This card is blocked");
                return;
            }
            if (_currentCard == null)
            {
                MessageBox.Show("No one card selected");
                return;
            }

            form.ShowDialog();
            label2.Text = _currentCard.Balance.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_currentCard == null)
            {
                MessageBox.Show("No one card selected");
                return;
            }

            BlockCardForm form = new BlockCardForm(_cardService, _currentCard);
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteCardForm form = new DeleteCardForm(_cardService, _currentCard);
            form.ShowDialog();

            comboBox1.Items.Clear();
            foreach (var card in _currentUser.Account.Cards)
                comboBox1.Items.Add(card.CardNumber);

            label2.Text = String.Empty;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddCardForm form = new AddCardForm(_cardService,
                _currentUser);
            form.ShowDialog();

            comboBox1.Items.Clear();
            foreach (var card in _currentUser.Account.Cards)
                comboBox1.Items.Add(card.CardNumber);

            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(_currentCard.CardNumber);
        }
    }
}