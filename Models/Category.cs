using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaAPI.Models
{
    public class Category : Entity
    {
        [Column(TypeName = "varchar(36)")]
        public Guid ProductId { get; set; }
        
        [Required]
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
