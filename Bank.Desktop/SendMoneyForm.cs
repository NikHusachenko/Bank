using Microsoft.EntityFrameworkCore;
using System.Linq;
using Bank.Database.Entities;
using Bank.Services.CardServices;

namespace Bank.Desktop
{
    public partial class SendMoneyForm : Form
    {
        private readonly ICardService _cardService;
        
        private readonly ClientEntity _clientEntity;
        private CardEntity _currentCard;

        public SendMoneyForm(ICardService cardService,
            ClientEntity clientEntity,
            CardEntity cardEntity)
        {
            _cardService = cardService;

            _clientEntity = clientEntity;
            _currentCard = cardEntity;

            InitializeComponent();

            foreach (var card in _clientEntity.Account.Cards)
                comboBox1.Items.Add(card.CardNumber);

            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(_currentCard.CardNumber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double sendingAmount;
            if (!double.TryParse(textBox2.Text, out sendingAmount))
            {
                textBox2.Text = String.Empty;
                return;
            }

            if (checkBox1.Checked)
            {
                _cardService.SendMoney(_currentCard, sendingAmount);
                this.Close();
                return;
            }
            else
            {
                long recipientCardNumber = long.Parse(textBox1.Text);
                CardEntity recipientCard = _cardService.GetBy(recipientCardNumber);

                if (recipientCard == null)
                {
                    textBox1.Text = String.Empty;
                    return;
                }

                _cardService.SendMoney(_currentCard, recipientCard.CardNumber, sendingAmount);
                this.Close();
                return;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            long cardNumber = long.Parse(comboBox1.SelectedItem.ToString());
            CardEntity newCard = _cardService.GetBy(cardNumber);
            _currentCard = newCard;
        }
    }
}