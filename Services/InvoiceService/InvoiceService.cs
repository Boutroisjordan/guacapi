using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;

namespace GuacAPI.Services;

public class InvoiceService : IInvoiceService
{
    #region Fields
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IInvoiceServiceProduct _invoiceServiceProduct;
    #endregion


    public InvoiceService(DataContext context, IMapper mapper, IInvoiceServiceProduct invoiceServiceProduct)
    {
        _context = context;
        this._mapper = mapper;
        this._invoiceServiceProduct = invoiceServiceProduct;
    }

    public async Task<InvoiceFurnisher> GeneratePDF(int invoiceId)
    {

        var invoice = await _context.InvoicesFurnisher
        .Include(x => x.InvoicesFurnisherProduct)
            .ThenInclude(y => y.Product)
        .Include(x => x.Furnisher)
        .Where(x => x.InvoiceFurnisherId == invoiceId)
        .FirstOrDefaultAsync();
        return invoice;
    }

    public async Task<InvoiceFurnisher> AddInvoice(InvoiceFurnisherRegister request)
    {
        InvoiceFurnisher invoice = _mapper.Map<InvoiceFurnisher>(request);
        // Ajouter l'offre à la base de données
        var addedInvoice = await _context.InvoicesFurnisher.AddAsync(invoice);

        await _context.SaveChangesAsync();

        return addedInvoice.Entity;
    }

    public async Task<List<InvoiceFurnisher>> GetAllInvoicesFurnisher()
    {
        var invoices = await _context.InvoicesFurnisher.ToListAsync();

        return invoices;
    }

    public async Task<InvoiceFurnisher> GetInvoiceFurnisher(int id)
    {
        // var invoice = await _context.InvoicesFurnisher.Include(x => x.InvoicesFurnisherProduct).FirstAsync;
        var invoice = await _context.InvoicesFurnisher
        .Include(x => x.InvoicesFurnisherProduct)
            .ThenInclude(y => y.Product)
        .Include(x => x.Furnisher)
        .Where(x => x.InvoiceFurnisherId == id)
        .FirstOrDefaultAsync();

        if (invoice is null)
        {
            return null;
        }

        return invoice;
    }


    public async Task<InvoiceFurnisher> UpdateInvoiceFurnisher(InvoiceFurnisherUpdate request, int id)
    {
        // var invoiceFurnisher = await _context.InvoicesFurnisher.FirstOrDefaultAsync(u => u.InvoiceFurnisherId == id);
        // if (invoiceFurnisher == null) return null;

        // InvoiceFurnisher addedInvoice = _mapper.Map(request, invoiceFurnisher);


        // // user.Username = request.Username;
        // await _context.SaveChangesAsync();
        // return addedInvoice;

        //Récupérer la facture
        var entityInvoice = await _context.InvoicesFurnisher.Where(x => x.InvoiceFurnisherId == id).FirstOrDefaultAsync();

        //Map OBJ Update dans une facture
        InvoiceFurnisher invoiceFurnisher = _mapper.Map<InvoiceFurnisher>(request);
        invoiceFurnisher.InvoiceFurnisherId = id;

        //Dans chaque Produit dans la facture
        invoiceFurnisher.InvoicesFurnisherProduct.ForEach(product =>
        {
            //récupérer l'id de la facture et l'id du produit
            var invoiceProduct = _context.InvoicesFurnisherProduct.FirstOrDefault(x => x.InvoiceFurnisherId == product.InvoiceFurnisherId && x.ProductId == product.ProductId);

            //Si il existe alors lui afecter la valeur de la requête
            if (invoiceProduct != null)
            {
                invoiceProduct.QuantityProduct = product.QuantityProduct;
            }
            else
            {
                //SI non, Vérifier que l'id facture existe et que l'id du produit existe et ajouter ce produit à la list de la facture
                if (_context.InvoicesFurnisher.Any(x => x.InvoiceFurnisherId == product.InvoiceFurnisherId) == true && _context.Products.Any(x => x.ProductId == product.ProductId) == true)
                {
                    _context.InvoicesFurnisherProduct.Add(product);
                }
            }
        });
            //Vérifier si il manque un produit
        var exceptItems = entityInvoice.InvoicesFurnisherProduct.Except(invoiceFurnisher.InvoicesFurnisherProduct);
    //Si il en manque un alors il faut le supprimer
        if (exceptItems != null)
        {
            foreach (var item in exceptItems)
            {
                _context.InvoicesFurnisherProduct.Remove(item);
            }
        }

        //Mapper les deux listes
        InvoiceFurnisher newOrder = _mapper.Map(entityInvoice, invoiceFurnisher);

        entityInvoice.Date = invoiceFurnisher.Date;
        entityInvoice.InvoiceNumber = invoiceFurnisher.InvoiceNumber;

    //sauvegarder
        await _context.SaveChangesAsync();

        return invoiceFurnisher;
    }

    // public async Task<Order> Update(int id, OrderUpdateDTO request)
    // {

    //     var entityOrder = await _context.Orders.FindAsync(id);
    //     Order order = _mapper.Map<Order>(request);

    //     order.OrderOffers.ForEach(offer =>
    //     {

    //         var orderOffer = _context.OrderOffers.FirstOrDefault(x => x.OfferId == offer.OfferId && x.OrderId == offer.OrderId);

    //         if (orderOffer != null)
    //         {
    //             orderOffer.Quantity = offer.Quantity;
    //         }
    //         else
    //         {
    //             if (_context.Offers.Any(x => x.OfferId == offer.OfferId) == true)
    //             {
    //                 _context.OrderOffers.Add(offer);
    //             }
    //         }
    //     });

    //     var exceptItems = entityOrder.OrderOffers.Except(order.OrderOffers);

    //     if (exceptItems != null)
    //     {
    //         foreach (var item in exceptItems)
    //         {
    //             _context.OrderOffers.Remove(item);
    //         }
    //     }

    //     Order newOrder = _mapper.Map(entityOrder, order);


    //     await _context.SaveChangesAsync();

    //     return order;
    // }

}