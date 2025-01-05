using Stripe;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork
            )
        {
          _configuration = configuration;
           _basketRepository = basketRepository;
           _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"]; 
            var Basket = await _basketRepository.GetBasketAsync( BasketId );
            if (Basket is null) return null;
            var ShippingPrice = 0m;
            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync( Basket.DeliveryMethodId.Value);
                Basket.ShippingCost = DeliveryMethod.Cost;
                ShippingPrice = DeliveryMethod.Cost;
            } 
            else
            {
                Basket.DeliveryMethodId = 1;
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(1);
                ShippingPrice = DeliveryMethod.Cost;


            }

            if (Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id); 
                    item.Price = product.Price;
                }


            }
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
             {
                var options = new PaymentIntentCreateOptions()
                {
                     Amount = (long) Basket.Items.Sum(item => item.Price * item.Qunatity * 100) + (long) ShippingPrice * 100,
                     Currency = "usd",
                     PaymentMethodTypes = new List<string> {"card"}
                }; 
               paymentIntent =  await service.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;

            } 
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Basket.Items.Sum(item => item.Price * item.Qunatity * 100) + (long)ShippingPrice * 100,

                };
                await service.UpdateAsync(Basket.PaymentIntentId, options);

            } 
            await _basketRepository.UpdateBasketAsync(Basket);
            return Basket;
        }
    }
}
