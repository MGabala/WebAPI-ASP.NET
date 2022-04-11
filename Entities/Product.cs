﻿namespace WebAPI_ASP.NET6.Entities;

    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; } = string.Empty; 
        [MaxLength(200)]
        public string? Desc { get; set; }
        public int Quantity { get; set; }
        public ICollection<TypeOfProduct> TypeOfProduct { get; set; } = new List<TypeOfProduct>();

    }
