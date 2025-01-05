namespace Talabat.Core.Entities.Identity.Order_Aggregate
{
    public class ProudctItemOrdered
    {
        public ProudctItemOrdered()
        {
            
        }
        public ProudctItemOrdered(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}