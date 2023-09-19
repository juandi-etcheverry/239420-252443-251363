﻿using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ProductRepository : IProductRepository
{
    private readonly DbContext _context;
    
    public ProductRepository(DbContext context)
    {
        _context = context;
    }

    public Product AddProduct(Product product)
    {
        _context.Set<Product>().Add(product);
        _context.SaveChanges();
        return product;
    }

}