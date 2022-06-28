using Bank.Database.Entities;

namespace Bank.Services.OrderServices
{
    public interface IOrderService
    {
        public OrderEntity GenerateOrder();
    }
}