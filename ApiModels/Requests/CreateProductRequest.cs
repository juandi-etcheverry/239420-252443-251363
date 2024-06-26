﻿using Domain;

namespace ApiModels.Requests;

public class CreateProductRequest
{
    public string Name { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public Brand Brand { get; set; }
    public Category Category { get; set; }
    public List<Color> Colors { get; set; }
    public int Stock { get; set; }
    public bool PromotionsApply { get; set; } = true;

    public Product ToEntity()
    {
        return new Product
        {
            Name = Name,
            Price = Price,
            Description = Description,
            Brand = Brand,
            Category = Category,
            Colors = Colors,
            Stock = Stock,
            PromotionsApply = PromotionsApply
        };
    }
}