using Bank.Database.Entities;
using Bank.Services.CardServices;
using Bank.Services.UserServices;

namespace Bank.Desktop
{
    public partial class AddCardForm : Form
    {
        private readonly ICardService _cardService;

        private readonly ClientEntity _clientEntity;
        private readonly CardEntity _cardEntity;

        public AddCardForm(ICardService cardService,
            ClientEntity clientEntity)
        {
            _cardService = cardService;

            _clientEntity = clientEntity;
            _cardEntity = _cardService.Generate();

            InitializeComponent();

            textBox1.Text = _cardEntity.CardNumber.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            short PIN;
            if(!short.TryParse(textBox2.Text, out PIN))
            {
                textBox2.Text = String.Empty;
                return;
            }

            _cardEntity.Password = PIN;
            _cardEntity.Account = _clientEntity.Account;
            _cardService.Create(_cardEntity);

            this.Close();
        }
    }
}