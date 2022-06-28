using Bank.Database.Entities;
using Bank.Services.CardServices;

namespace Bank.Desktop
{
    public partial class DeleteCardForm : Form
    {
        private readonly ICardService _cardService;
        private CardEntity _cardEntity;

        public DeleteCardForm(ICardService cardService,
            CardEntity cardEntity)
        {
            _cardService = cardService;
            _cardEntity = cardEntity;

            InitializeComponent();

            label2.Text = _cardEntity.CardNumber.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _cardService.Delete(_cardEntity);
            _cardEntity = null;
            this.Close();
        }
    }
}