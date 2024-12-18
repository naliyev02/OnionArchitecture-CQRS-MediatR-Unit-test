﻿using OnionArchitectureApp.Domain.Common;

namespace OnionArchitectureApp.Domain.Entities;

public class ProductType : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Product>? Products { get; set; }
}
