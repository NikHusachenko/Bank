using Bank.Database.Entities;
using Bank.Services.CardServices;

namespace Bank.Desktop
{
    public partial class BlockCardForm : Form
    {
        private readonly ICardService _cardService;

        private readonly CardEntity _cardEntity;
        private readonly bool _isBlocked;

        public BlockCardForm(ICardService cardService,
            CardEntity cardEntity)
        {
            _cardService = cardService;

            _cardEntity = cardEntity;
            _isBlocked = _cardEntity.IsBlock;

            InitializeComponent();

            if(_isBlocked)
            {
                CardIsBlocked();
            }
            else
            {
                CardNotBlocked();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isBlocked)
            {
                _cardEntity.IsBlock = false;
                _cardService.Update(_cardEntity);
                MessageBox.Show("Unblocked");
                this.Close();
            }
            else
            {
                _cardEntity.IsBlock = true;
                _cardService.Update(_cardEntity);
                MessageBox.Show("Blocked");
                this.Close();
            }
        }

        public void CardIsBlocked()
        {
            label1.Text = "Do you want to unblock the card?";
            button1.Text = "Yes, unblock";
            button2.Text = "No, don't unblock";
        }

        public void CardNotBlocked()
        {
            label1.Text = "Do you want to block the card?";
            button1.Text = "Yes, block";
            button2.Text = "No, don't block";
        }
    }
}