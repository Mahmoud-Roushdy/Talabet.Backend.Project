using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Order_Specification;
using Talabat.Repository;
using Talabat.Service.DTOs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IPaymentService _paymentService;

        ///private readonly IGenericRepository<Product> _productRepository;
        ///private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        ///private readonly IGenericRepository<Order> _orderRepository;

        public OrderService(IBasketRepository basketRepository, 
            IUnitOfWork unitOfWork,
            IPaymentService paymentService
            ///IGenericRepository<Product> ProductRepository,
            ///IGenericRepository<DeliveryMethod> DeliveryMethodRepo,
            ///IGenericRepository<Order> OrderRepository
            )
        {
            _basketRepository = basketRepository;
            _UnitOfWork = unitOfWork;
            _paymentService = paymentService;
            ///_productRepository = ProductRepository;
            ///_deliveryMethodRepo = DeliveryMethodRepo;
            ///_orderRepository = OrderRepository;
        }

       

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodId, Address ShippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            var OrderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var Product = await _UnitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrdered = new ProudctItemOrdered(item.Id, Product.Name, Product.PictureUrl);

                    var OrderItem = new OrderItem(ProductItemOrdered, Product.Price, item.Qunatity);
                     OrderItems.Add(OrderItem);

                }  
            }
            var SubTotal = OrderItems.Sum(item => item.Price * item.Quantity);
            var DeliveryMethod = await _UnitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            var spec = new OrderPaymentSpec(basket.PaymentIntentId);
             var existingOrder = await _UnitOfWork.Repository<Order>().GetByIdWithSpec(spec);
            if (existingOrder != null)
            {
                 _UnitOfWork.Repository<Order>().Delete( existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(BasketId);
            }

            var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, SubTotal, basket.PaymentIntentId );
           await _UnitOfWork.Repository<Order>().Add(Order); 
             
             var result = await _UnitOfWork.Complete();
            if (result <= 0) return null;
            return Order;
          
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethods()
        {
            var deliverymethods =  _UnitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliverymethods;
        }

        public Task<Order> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail)
        {
            var sepc = new OrderSpecification(OrderId,  BuyerEmail);
            var order = _UnitOfWork.Repository<Order>().GetByIdWithSpec(sepc);
            return order;

        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        { 
            var spec = new OrderSpecification(BuyerEmail);
            var orders = _UnitOfWork.Repository<Order>().GetAllAsyncWithSpec(spec);
            return orders;
        } 

    }
}
