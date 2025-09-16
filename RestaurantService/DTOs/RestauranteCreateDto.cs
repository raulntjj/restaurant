using System.ComponentModel.DataAnnotations;

namespace RestaurantService.DTOs
{
    public class RestaurantCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Site { get; set; }
    }
}