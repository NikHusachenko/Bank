using Bank.Database.Entities;
using Bank.Services.CardServices;
using Bank.Services.OrderServices;

namespace Bank.Desktop
{
    public partial class PayOrderForm : Form
    {
        private readonly ICardService _cardService;
        private readonly IOrderService _orderService;

        private readonly CardEntity _cardEntity;
        private readonly OrderEntity _orderEntity;

        public PayOrderForm(ICardService cardService,
            IOrderService orderService,
            CardEntity cardEntity)
        {
            _cardService = cardService;
            _orderService = orderService;
            _cardEntity = cardEntity;

            _orderEntity = _orderService.GenerateOrder();
            _orderEntity.PayerNumber = _cardEntity.CardNumber;

            InitializeComponent();

            richTextBox1.Text = _orderEntity.Appoinment;
            label3.Text = _orderEntity.AuthorNumber.ToString();
            label5.Text = _orderEntity.Cost.ToString();
            label7.Text = _cardEntity.Balance.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _cardService.SendMoney(_cardEntity, _orderEntity.AuthorNumber, _orderEntity.Cost);
            this.Close();
        }
    }
}