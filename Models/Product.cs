using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaAPI.Models
{
    public class Product : Entity
    {
        [Column(TypeName = "varchar(36)")]
        public Guid CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Model { get; set; }
        
        [Required]
        public string Brand { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public int Amount { get; set; }
        
        [Required]
        public string Image { get; set; }

        [Required]
        [Column(TypeName = "mediumblob")]
        public string ImageUpload { get; set; }
        public Category Category { get; set; }
    }
}
