using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class Restaurant
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}