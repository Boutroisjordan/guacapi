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
    public async Task<List<OrderStatus>> GetAllStatus()
    {
        var result = await _context.OrderStatus.ToListAsync();
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
        int total = 0;
        if (request.OrderOfferRegistryDTOs.Count > 0)
        {
            Console.Write("ici");

            List<OrderOffer> listOffer = new List<OrderOffer>();
            request.OrderOfferRegistryDTOs.ForEach(item =>
            {
                OrderOffer offer = _mapper.Map<OrderOffer>(item);
                Offer objOffer = _context.Offers.Find(offer.OfferId);
                var calc = offer.Quantity * objOffer.Price;
                total += calc;
                listOffer.Add(offer);
            });

            order.OrderOffers = listOffer;
            Console.Write("heyhoAVANT");

            order.Total = total;
            Console.Write("heyho");
            order.OrderStatusId = 1;
        }

        var savedOrder = _context.Orders.Add(order).Entity;
        await _context.SaveChangesAsync();

        return savedOrder;
    }

    public async Task<Order> Update(int id, OrderUpdateDTO request)
    {

        var entityOrder = await _context.Orders.Where(x => x.OrderId == id).FirstOrDefaultAsync();
        Order order = _mapper.Map<Order>(request);
        order.OrderId = id;
        int total = 0;
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
                var calc = offer.Quantity * offer.offer.Price;
                total += calc;

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
        entityOrder.Total = total;
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
    public async Task<Order> Commander(int id)
    {
        var order = await _context.Orders.Include(x => x.OrderOffers).Where(x => x.OrderId == id).FirstOrDefaultAsync();
        List<InvoiceFurnisher> factures = new List<InvoiceFurnisher>();
        order.OrderStatusId = 3;

        //Check si le stock est bon

        order.OrderOffers.ForEach( productOffer => {
            Offer offer =  _context.Offers.Include(x => x.ProductOffers).ThenInclude(x => x.Product).FirstOrDefault(x => x.OfferId == productOffer.OfferId);
            // Offer offer =  _context.Offers.Find(productOffer.OfferId);

            var orderQuantity = productOffer.Quantity;

            if (offer.ProductOffers != null) {


            offer.ProductOffers.ForEach(productItem => {


                var totalQueryProduct = productItem.QuantityProduct * orderQuantity;

                var product = _context.Products.Find(productItem.ProductId);
                product.Stock -= totalQueryProduct;

                // var result = productItem.Product.Stock - totalQueryProduct;
                var result = product.Stock - totalQueryProduct;


                if(result < 0) {
                        InvoiceFurnisher facture = new InvoiceFurnisher() {
                            FurnisherId = productItem.Product.FurnisherId,
                            InvoicesFurnisherProduct = new List<InvoiceFurnisherProduct>() {
                                new InvoiceFurnisherProduct() {ProductId = productItem.ProductId, QuantityProduct = product.Stock < 0 ? totalQueryProduct: Math.Abs(result)}
                            },
                            Date = DateTime.Now,
                            InvoiceNumber = "your invoice number"
                            };

                        _context.InvoicesFurnisher.Add(facture);
                }


            });
        
            }
        });
            
        if (order is null)
            return null;

        await _context.SaveChangesAsync();

        return order;
    }
    public async Task<Order> UpdateStatus(int id, int statusId) {
        var order =  await _context.Orders.Include(x => x.OrderStatus).Where(x => x.OrderId == id).FirstOrDefaultAsync();

        if (order is null)
            return null;

        order.OrderStatusId = statusId;
        await _context.SaveChangesAsync();

        return order;
    }

}