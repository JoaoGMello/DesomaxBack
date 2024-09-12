using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesomaxBack.Models
{
    public class Car : ModelBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        [Precision(18, 3)]
        public decimal Price { get; set; }
        public string? Image {  get; set; }
        public string? Color { get; set; }
        public string? Km { get; set; }
        public bool Like { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
