﻿namespace Domain;

public class Color
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}