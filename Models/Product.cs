﻿using System;
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
        
        [Column(TypeName = "mediumblob")]
        public string Image { get; set; }
        public Category Category { get; set; }
    }
}
