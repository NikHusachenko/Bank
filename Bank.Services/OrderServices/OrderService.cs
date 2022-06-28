using Bank.Database.Entities;
using Bank.Services.CardServices;

namespace Bank.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly ICardService _cardService;

        private string[] _orderAppoinment = new string[]
        {
            "Wrong parking ticket", // Оплата неправильной парковки
            "Payment for food delivery", // Оплата доставки еды
            "Rent", // Квартплата
            "Purchase in the online store", // Покупка в интернет магазине
            "Payment for registration on the Violity.com", // Оплата регистрации на сайте
            "Transaction in the game", // Транзакция в игре (Донат)
            "Buying a game in the store", // Покупка игры в магазине
            "Monthly card maintenance", // Ежемесячное обслуживание карты
            "Payment for services" // Оплата услуг
        };

        public OrderService(ICardService cardService)
        {
            _cardService = cardService;
        }

        private string this[int index]
        {
            get 
            {
                if (index >= 0 && index < _orderAppoinment.Length)
                    return _orderAppoinment[index];
                else
                    return string.Empty;
            }
        }

        public OrderEntity GenerateOrder()
        {
            OrderEntity orderEntity = new OrderEntity()
            {
                Appoinment = this[new Random().Next(0, _orderAppoinment.Length)],
                AuthorNumber = _cardService.GenerateCardNumber(),
                Cost = new Random().Next(0, 10) * 100,
                CreatedDate = DateTime.Now.AddDays(-7),
            };

            return orderEntity;
        }
    }
}