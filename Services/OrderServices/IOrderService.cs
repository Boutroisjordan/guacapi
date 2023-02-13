using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IOrderService
{
    Task<List<Order>> GetAllOrders();
    Task<Order> GetOne(int id);
    Task<Order> Add(OrderRegistryDTO request);
    Task<Order> Update(int id, OrderUpdateDTO request);
    Task<Order> Delete(int id);

}
