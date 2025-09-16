using System.ComponentModel.DataAnnotations;

namespace RestaurantService.Models;

public class Restaurant
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Site { get; set; }

}