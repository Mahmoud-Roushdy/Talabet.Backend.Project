using System.ComponentModel.DataAnnotations;

namespace Talabat.Service.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage =" Qunatity must be grater than 1 item")]
        public int Qunatity { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = " price must be grater than 0.1")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Brand { get; set; }
    }
}