using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;

namespace GuacAPI.Services;

public class OrderService : IOrderService
{
    #region Fields
    private readonly DataContext _context;
    private readonly IOrderOfferService _orderOfferService;
    private readonly IMapper _mapper;
    #endregion

    // #region Constructors
    public OrderService(DataContext context, IMapper mapper, IOrderOfferService orderOfferService)
    {
        this._context = context;
        this._mapper = mapper;
        this._orderOfferService = orderOfferService;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        var result = await _context.Orders.ToListAsync();
        return result;
    }
    public async Task<Order> GetOne(int id)
    {
        var result = await _context.Orders.Include(i => i.OrderStatus)
        .Include(i => i.OrderOffers)
        .ThenInclude(x => x.offer)
        .Where(i => i.OrderId == id)
        .FirstOrDefaultAsync();
        return result;
    }

    public async Task<Order> Add(OrderRegistryDTO request)
    {
        Order order = _mapper.Map<Order>(request);

        if (request.OrderOfferRegistryDTOs.Count > 0)
        {

            List<OrderOffer> listOffer = new List<OrderOffer>();
            request.OrderOfferRegistryDTOs.ForEach(item =>
            {
                OrderOffer offer = _mapper.Map<OrderOffer>(item);
                listOffer.Add(offer);
            });

            order.OrderOffers = listOffer;
        }

        var savedOrder = _context.Orders.Add(order).Entity;
        await _context.SaveChangesAsync();

        return savedOrder;
    }

    public void SaveChanges()
    {
        this._context.SaveChanges();
    }

    public async Task<Order> Update(int id, OrderUpdateDTO request)
    {

        var entityOrder = await _context.Orders.Where(x => x.OrderId == id).FirstOrDefaultAsync();
        Order order = _mapper.Map<Order>(request);
        order.OrderId = id;
        order.OrderOffers.ForEach(offer =>
        {

            var orderOffer = _context.OrderOffers.FirstOrDefault(x => x.OfferId == offer.OfferId && x.OrderId == offer.OrderId);

            if (orderOffer != null)
            {
                orderOffer.Quantity = offer.Quantity;
            }
            else
            {
                if (_context.Offers.Any(x => x.OfferId == offer.OfferId) == true)
                {
                    _context.OrderOffers.Add(offer);
                }
            }
        });


        var exceptItems = entityOrder.OrderOffers.Except(order.OrderOffers);

        if (exceptItems != null)
        {
            foreach (var item in exceptItems)
            {
                _context.OrderOffers.Remove(item);
            }
        }

        // Order newOrder = _mapper.Map(entityOrder, order);

        entityOrder.OrderStatus = order.OrderStatus;
        await _context.SaveChangesAsync();



        // return newOrder;
        return order;
    }

    public async Task<Order> Delete(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
            return null;

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();

        return order;
    }

}